using UnityEngine;

public class WheelSpeed3 : MonoBehaviour
{

    public float velocity;
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
        
        // targetVelocityを設定
        xDrive.targetVelocity = velocity;

        // 設定した値をArticulationBodyに戻す
        articulationBody.xDrive = xDrive;

    }
    
}
