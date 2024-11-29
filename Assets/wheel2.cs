using UnityEngine;
using System.Collections;

public class RotateCube2 : MonoBehaviour
{

    void Update()
    {
        // transformを取得
        Transform myTransform = this.transform;

        // ローカル座標を基準に、回転を取得
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.z = 5.0f;
    }
}