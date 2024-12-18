using UnityEngine;

public class VehicleFollowCamera : MonoBehaviour
{
    public Transform target; // 追跡する乗り物（ターゲット）
    public Vector3 offset = new Vector3(0, 5, -10); // カメラの相対的な位置
    public float smoothSpeed = 0.125f; // カメラ追従のスムーズさ

    public float pitch = 10f; // 上下角度
    public float yaw = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        // 進行方向を考慮したカメラ位置の計算
        Vector3 targetPosition = target.position + target.TransformDirection(offset);

        // スムーズに追従する
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // カメラの位置を更新
        transform.position = smoothedPosition;

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        Quaternion customRotation = Quaternion.Euler(pitch, targetRotation.eulerAngles.y + yaw, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, customRotation, smoothSpeed);
    }
}
