using UnityEngine;
using System;

public class ShotCalculator : MonoBehaviour
{
    private BasketballSimulator inputAction_;
    public Transform Shootobj;
    public BallShooter shooter;
    // シミュレーションのタイムステップとステップ数
    private const float TIME_STEP = 0.01f;
    private const int STEPS = 1000;

    private const float GOAL_X_UNITY = 0.0f;
    private const float GOAL_Y_UNITY = 2.42f - 0.4f;
    private const float GOAL_Z_UNITY = 6.71f;
    private const float BACKBOARD_Z_UNITY = 6.95f;
    private const float GOAL_RADIUS = 0.23f;
    private const float GRAVITY = 9.81f;
    private const float BALL_RADIUS = 24.6f / 2f / 100f;
    private const float BOUNCE_COEFFICIENT = 0.8f;
    private const float MAX_VELOCITY = 15f;
    private const float MAX_PITCH_RAD = 60.0f * Mathf.PI / 180.0f;

    public bool isDone = false;


    public Vector3 myLocation;


    private struct TargetAngles
    {
        public float yaw, pitch, roll;
    }

    private TargetAngles targetAngles;

    void Start()
    {
        inputAction_ = new BasketballSimulator();

        inputAction_.Enable();

        myLocation.y += 1.0f;
       
    }
    
    void Update()
    {
        myLocation = Shootobj.position;
        if (inputAction_.Player.Fire.triggered)
        {
            SimulateBasketballShotDirect();
            isDone = true;
        }

        if (inputAction_.Player.Select.triggered)
        {
            SimulateBasketballShotBounce();
            isDone = true;
        }
    }


    private void SimulateBasketballShotDirect()
    {
        targetAngles.yaw = Mathf.Atan2(GOAL_X_UNITY - myLocation.x, GOAL_Z_UNITY - myLocation.z);
        float horizontalDistanceDirect = Mathf.Sqrt(Mathf.Pow(GOAL_X_UNITY - myLocation.x, 2) + Mathf.Pow(GOAL_Z_UNITY - myLocation.z, 2));
        targetAngles.pitch = Mathf.Atan2(GOAL_Y_UNITY - myLocation.y, horizontalDistanceDirect);

        SimulateShotInternal(targetAngles.yaw, targetAngles.pitch, false);
    }

    private void SimulateBasketballShotBounce()
    {
        
        targetAngles.yaw = Mathf.Atan2(GOAL_X_UNITY - myLocation.x, BACKBOARD_Z_UNITY + 2 * (BACKBOARD_Z_UNITY - GOAL_Z_UNITY - BALL_RADIUS) - myLocation.z);
        float horizontalDistanceBounce = Mathf.Sqrt(Mathf.Pow(GOAL_X_UNITY - myLocation.x, 2) + Mathf.Pow(BACKBOARD_Z_UNITY - myLocation.z, 2));
        targetAngles.pitch = Mathf.Atan2(GOAL_Y_UNITY - myLocation.y, horizontalDistanceBounce);

        SimulateShotInternal(targetAngles.yaw, targetAngles.pitch, true);
        
    }

    private void SimulateShotInternal(float yaw, float initialPitch, bool isBounceShot)
    {
        for (float pitch = initialPitch; pitch <= MAX_PITCH_RAD; pitch += (Mathf.PI / 180.0f) / 10.0f)
        {
            for (float v = 0; v <= MAX_VELOCITY; v += 0.5f)
            {
                float x = myLocation.x;
                float y = myLocation.y;
                float z = myLocation.z;

                float v_horizontal = v * Mathf.Cos(pitch);

                float vx = v_horizontal * Mathf.Sin(yaw);
                float vz = v_horizontal * Mathf.Cos(yaw);
                float vy = v * Mathf.Sin(pitch);

                bool bounced = false;
                for (int k = 0; k < STEPS; k++)
                {
                    x += vx * TIME_STEP;
                    z += vz * TIME_STEP;
                    y += vy * TIME_STEP + 0.5f * -GRAVITY * TIME_STEP * TIME_STEP;
                    vy += -GRAVITY * TIME_STEP;
                    if (isBounceShot && !bounced && z >= BACKBOARD_Z_UNITY - BALL_RADIUS)
                    {
                        z = BACKBOARD_Z_UNITY - BALL_RADIUS;
                        vz *= -BOUNCE_COEFFICIENT;
                        bounced = true;
                    }

                    if (vy <= 0)
                    {
                        float targetZForDistanceCheck = GOAL_Z_UNITY;
                        float distanceSq = Mathf.Pow(x - GOAL_X_UNITY, 2) + Mathf.Pow(z - targetZForDistanceCheck, 2);
                        
                        if (distanceSq <= Mathf.Pow(GOAL_RADIUS - BALL_RADIUS, 2))
                        {
                            if (y >= GOAL_Y_UNITY - 0.03f && y <= GOAL_Y_UNITY + 0.03f)
                            {
                                shooter.pitchAngle = pitch * Mathf.Rad2Deg;
                                shooter.yawAngle = yaw * Mathf.Rad2Deg;
                                
                                UnityEngine.Debug.Log($"success v: {v:F1} m/s, yaw: {shooter.yawAngle:F2} deg, pitch: {shooter.pitchAngle:F3} def");
                                shooter.launchSpeed = v;
                                return;
                            }
                        }
                    }
                }
            }
        }
        UnityEngine.Debug.Log("range error");
    }
}
