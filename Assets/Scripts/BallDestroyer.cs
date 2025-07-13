using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    // ボールを削除するY座標の閾値（インスペクターで設定可能）
    public float destroyYThreshold = -1.0f;
    

    void Update()
    {
        // ボールの現在のY座標が閾値を下回ったかチェック
        if (transform.position.y < destroyYThreshold)
        {
            // 条件を満たした場合、このゲームオブジェクト（ボールのクローン）を削除
            Destroy(gameObject);
        }
    }
}