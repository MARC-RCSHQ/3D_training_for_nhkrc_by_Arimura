using UnityEngine;
using System.Collections;

public class RotateCube3 : MonoBehaviour
{

    void Update()
    {
        // transform���擾
        Transform myTransform = this.transform;

        // ���[�J�����W����ɁA��]���擾
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.x = -5.0f;
    }
}