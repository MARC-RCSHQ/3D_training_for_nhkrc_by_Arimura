using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public float jumpForce = 5f; // ジャンプの強さ
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // スペースバーを押した時にジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 地面に接触したらisGroundedをtrueにする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
