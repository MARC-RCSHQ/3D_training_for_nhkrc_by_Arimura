using UnityEngine;
using UnityEngine.InputSystem;

public class BallShooter : MonoBehaviour
{
    private BasketballSimulator inputAction_;

    public GameObject ballPrefab;
    public Transform robotSpawnPoint;
    public ShotCalculator ShotCal;
    public float launchSpeed = 10f;
    
    public float yawAngle = 0f;
    public float pitchAngle = 30f;

    public float spawnYOffset = 1.0f;

    void Start()
    {
        inputAction_ = new BasketballSimulator();
    }

    void Update(){
        if(ShotCal.isDone){
            ShootBall();
            ShotCal.isDone = false;
        }
    }
    

    public void ShootBall()
    {
        Vector3 adjustedSpawnPosition = robotSpawnPoint.position + new Vector3(0f, spawnYOffset, 0f);
        
        GameObject ball = Instantiate(ballPrefab, adjustedSpawnPosition, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb.isKinematic)
        {
            rb.isKinematic = false;
        }

        Quaternion launchRotation = Quaternion.Euler(-pitchAngle, yawAngle, 0);
        Vector3 launchDirection = launchRotation * Vector3.forward;

        rb.linearVelocity = launchDirection.normalized * launchSpeed;
    }
}
