using UnityEngine;
using System.Collections;

public class MainCharacter2 : MonoBehaviour {
    void Update () {
        // transformを取得
        Transform myTransform = this.transform;

        // ローカル座標を基準に、回転を取得
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.x = 10.0f; // ローカル座標を基準に、x軸を軸にした回転を10度に変更
        localAngle.y = 10.0f; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
        localAngle.z = 10.0f; // ローカル座標を基準に、z軸を軸にした回転を10度に変更
        myTransform.localEulerAngles = localAngle; // 回転角度を設定

    }
}
