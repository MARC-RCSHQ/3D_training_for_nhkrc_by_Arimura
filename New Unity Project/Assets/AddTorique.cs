using UnityEngine;

public class RollingBallWithTorque : MonoBehaviour
{
    public float torqueAmount = 10f; // 回転エネルギーの大きさ

    private Rigidbody rb;

    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // ユーザー入力を取得
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 入力に基づいてトルクを加える
        Vector3 torque = new Vector3(moveVertical, 0, -moveHorizontal); // 水平方向の入力を回転軸に変換
        rb.AddTorque(torque * torqueAmount);
    }
}
