using UnityEngine;

public class RollingBall : MonoBehaviour
{
    public float speed = 10f; // 球体を転がす力の強さ

    private Rigidbody rb;

    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // ユーザー入力を取得
        float moveHorizontal = 2f * Input.GetAxis("Horizontal");
        float moveVertical = 2f * Input.GetAxis("Vertical");

        // 入力に基づいて力を加える
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
}
