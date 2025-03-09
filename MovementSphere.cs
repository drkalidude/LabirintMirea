using UnityEngine;

public class Ball3DPhysics : MonoBehaviour
{
    [Header("Forward/Back Settings")]
    [SerializeField] private float forwardSensitivity = 1.5f;
    [SerializeField] private float forwardMaxAngle = 30f;
    [SerializeField] private float backwardMaxAngle = 20f;

    [Header("Left/Right Settings")]
    [SerializeField] private float lateralSensitivity = 1f;
    [SerializeField] private float lateralMaxAngle = 25f;

    [Header("Common Settings")]
    [SerializeField] private float friction = 0.4f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float accelerationSmoothing = 0.15f;

    [Header("Advanced")]
    [SerializeField] private AnimationCurve forwardResponseCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve lateralResponseCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float deadZone = 0.08f;

    private Rigidbody rb;
    private Quaternion calibrationQuaternion;
    private Vector3 targetAcceleration;
    private Vector3 currentAcceleration;

    void Start()
    {
        InitializeSystems();
        CalibrateGyro();
    }

    void InitializeSystems()
    {
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void CalibrateGyro()
    {
        calibrationQuaternion = Quaternion.Inverse(Input.gyro.attitude);
    }

    void FixedUpdate()
    {
        ProcessGyroInput();
        ApplyMovementForces();
        ApplyFriction();
        ClampVelocity();
    }

    void ProcessGyroInput()
    {
        Quaternion deviceRotation = calibrationQuaternion * Input.gyro.attitude;
        Vector3 tilt = deviceRotation.eulerAngles;

        float forwardTilt = NormalizeAngle(tilt.x);
        float forwardInput = 0f;

        if (forwardTilt > 0) 
        {
            forwardInput = Mathf.Clamp(forwardTilt / forwardMaxAngle, 0f, 1f);
            forwardInput = forwardResponseCurve.Evaluate(forwardInput);
        }
        else 
        {
            forwardInput = Mathf.Clamp(-forwardTilt / backwardMaxAngle, 0f, 1f);
            forwardInput = -forwardResponseCurve.Evaluate(forwardInput);
        }

        float lateralTilt = NormalizeAngle(tilt.z);
        float lateralInput = Mathf.Clamp(lateralTilt / lateralMaxAngle, -1f, 1f);
        lateralInput = lateralResponseCurve.Evaluate(Mathf.Abs(lateralInput)) * Mathf.Sign(lateralInput);

        if (Mathf.Abs(forwardInput) < deadZone) forwardInput = 0f;
        if (Mathf.Abs(lateralInput) < deadZone) lateralInput = 0f;

        targetAcceleration = new Vector3(
            lateralInput * lateralSensitivity,
            0f,
            forwardInput * forwardSensitivity
        );
    }

    void ApplyMovementForces()
    {
        currentAcceleration = Vector3.Lerp(
            currentAcceleration,
            targetAcceleration,
            accelerationSmoothing * Time.fixedDeltaTime * 50f
        );

        rb.AddForce(currentAcceleration, ForceMode.Acceleration);
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        return angle > 180f ? angle - 360f : angle;
    }

    void ApplyFriction()
    {
        if (rb.velocity.magnitude > 0.01f)
        {
            Vector3 frictionForce = -rb.velocity.normalized * friction;
            rb.AddForce(frictionForce, ForceMode.Acceleration);
        }
    }

    void ClampVelocity()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        currentAcceleration = Vector3.zero;
        targetAcceleration = Vector3.zero;
        transform.position = new Vector3(0, 0.5f, 0);
    }

    /*

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, currentAcceleration * 2f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, rb.velocity);
    }

    */

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, currentAcceleration * 2f);

        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }

}
