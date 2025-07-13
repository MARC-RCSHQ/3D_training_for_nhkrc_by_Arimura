using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private BasketballSimulator inputAction_;
    public WheelSpeed1 wheelspeed1;
    public WheelSpeed2 wheelspeed2;
    public WheelSpeed3 wheelspeed3;
    public WheelSpeed4 wheelspeed4;

    public float Maxvelocity = 500f;
    public float MaxRotateSpeed = 100f;

    private readonly float cos45 = Mathf.Cos(Mathf.PI / 4);
    private readonly float sin45 = Mathf.Sin(Mathf.PI / 4);
    void Start()
    {
        inputAction_ = new BasketballSimulator();
        inputAction_.Enable();
        
    }

    void Update()
    {

        Vector2 moveInput = inputAction_.Player.Move.ReadValue<Vector2>();
        Vector2 rotatedMoveInput = Vector2.zero;
        rotatedMoveInput.x = moveInput.x * cos45 - moveInput.y * sin45;
        rotatedMoveInput.y = moveInput.x * sin45 + moveInput.y * cos45;

        wheelspeed1.velocity = rotatedMoveInput.x * Maxvelocity;
        wheelspeed2.velocity = rotatedMoveInput.y * Maxvelocity;
        wheelspeed3.velocity = -rotatedMoveInput.x * Maxvelocity;
        wheelspeed4.velocity = -rotatedMoveInput.y * Maxvelocity;

        float cwrotateInput = inputAction_.Player.CWRotate.ReadValue<float>();
        if (cwrotateInput != 0)
        {
            wheelspeed1.velocity = cwrotateInput * MaxRotateSpeed;
            wheelspeed2.velocity = cwrotateInput * MaxRotateSpeed;
            wheelspeed3.velocity = cwrotateInput * MaxRotateSpeed;
            wheelspeed4.velocity = cwrotateInput * MaxRotateSpeed;
        }
        
        float ccwrotateInput = inputAction_.Player.CCWRotate.ReadValue<float>();
        if (ccwrotateInput != 0) {
            wheelspeed1.velocity = -ccwrotateInput * MaxRotateSpeed;
            wheelspeed2.velocity = -ccwrotateInput * MaxRotateSpeed;
            wheelspeed3.velocity = -ccwrotateInput * MaxRotateSpeed;
            wheelspeed4.velocity = -ccwrotateInput * MaxRotateSpeed;
        }
    }
}
