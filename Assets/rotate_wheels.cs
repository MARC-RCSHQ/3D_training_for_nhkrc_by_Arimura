using UnityEngine;

public class ControlXDriveTargetVelocity : MonoBehaviour
{
    public float velocity = 100f; // 目標速度
    private ArticulationBody articulationBody;

    void Start()
    {
        // ArticulationBodyコンポーネントを取得
        articulationBody = GetComponent<ArticulationBody>();
    }

    void Update()
    {
        
            // X Driveの現在の値を取得
            ArticulationDrive xDrive = articulationBody.xDrive;
if (Input.GetKey(KeyCode.Space))
        {
            // targetVelocityを設定
            xDrive.targetVelocity = velocity;
        }
        else {
            xDrive.targetVelocity = 0;
        }

            // 設定した値をArticulationBodyに戻す
            articulationBody.xDrive = xDrive;
        

        DebugDrive();

    }


    void DebugDrive()
    {
    ArticulationDrive xDrive = articulationBody.xDrive;
    Debug.Log("Current targetVelocity: " + xDrive.targetVelocity);
    }
}
