using UnityEngine;
using UnityEngine.InputSystem; 

public class ThirdPersonControlledOrbitCamera : MonoBehaviour
{
    public Transform target; 
    public float orbitRadius = 1f; 

    public float mouseSensitivity = 0.1f; 
    public float gamepadSensitivity = 200f; 

    public float positionSmoothSpeed = 0.125f; 

    public float clampAngleX = 80f; 
    public float initialVerticalAngle = 15f; 
    
    // --- 追加: 初期水平角度のオフセットを設定 ---
    [Tooltip("ターゲットの進行方向に対する初期カメラ水平角度のオフセット（Y軸）")]
    public float initialHorizontalAngleOffset = 0f; 

    public float lookInputThreshold = 0.05f; 
    public float minCameraHeight = 0.5f; // 前回の修正分

    private BasketballSimulator inputAction_; 
    private Vector2 lookInput; 

    private float currentOrbitAngleY; 
    private float currentOrbitAngleX; 

    void Awake()
    {
        inputAction_ = new BasketballSimulator();
        inputAction_.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputAction_.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnEnable()
    {
        inputAction_.Enable();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        if (target != null)
        {
            // カメラをターゲットの真後ろ（target.forwardの逆方向）に配置するための回転を計算
            Quaternion initialLookRotation = Quaternion.LookRotation(-target.forward);
            
            // ターゲットの真後ろの角度に、設定したオフセット角度を加算して初期角度を決定
            currentOrbitAngleY = initialLookRotation.eulerAngles.y + initialHorizontalAngleOffset;
            currentOrbitAngleX = initialVerticalAngle; 
        }
        else
        {
            currentOrbitAngleY = transform.eulerAngles.y;
            currentOrbitAngleX = transform.eulerAngles.x; 
        }
    }

    void OnDisable()
    {
        inputAction_.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // --- (入力処理と感度設定は省略) ---
        float currentSensitivity;
        if (inputAction_.Player.Look.activeControl != null)
        {
            currentSensitivity = (inputAction_.Player.Look.activeControl.device is Gamepad) ? gamepadSensitivity * Time.deltaTime : mouseSensitivity;
        }
        else
        {
            currentSensitivity = 0f;
        }

        bool isLooking = lookInput.magnitude > lookInputThreshold;

        if (isLooking)
        {
            currentOrbitAngleY += lookInput.x * currentSensitivity;
            currentOrbitAngleX -= lookInput.y * currentSensitivity; 
        }

        // 垂直方向の回転角度を制限
        currentOrbitAngleX = Mathf.Clamp(currentOrbitAngleX, -clampAngleX, clampAngleX);

        // カメラの目標位置を計算
        Quaternion orbitRotation = Quaternion.Euler(currentOrbitAngleX, currentOrbitAngleY, 0f);
        Vector3 desiredPosition = target.position + orbitRotation * (Vector3.forward * -orbitRadius); 

        // カメラのY座標に最低高さを適用
        desiredPosition.y = Mathf.Max(desiredPosition.y, minCameraHeight);

        // カメラの位置を滑らかに補間
        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed);

        // 常にターゲットを中央に捉えるようにLookAtする
        transform.LookAt(target.position);
    }
}