using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallMoviment : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float speedIncrement = .1f;
    public float maxSpeed = 30f;
    public float delayBeforeStart = 1f;

    private float currentSpeed;
    private Rigidbody rb;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;


        currentSpeed = initialSpeed;
        Invoke(nameof(LaunchBall), delayBeforeStart);
    }

    public void LaunchBall()
    {
        direction = GetRandomDiagonalDirection();
        currentSpeed = initialSpeed;
        rb.isKinematic = false;
        rb.linearVelocity = direction * currentSpeed;
    }

    private Vector3 GetRandomDiagonalDirection()
    {
        float x = Random.Range(0.6f, 1f) * (Random.value < 0.5f ? -1 : 1);
        float z = Random.Range(0f, 0.4f) * (Random.value < 0.5f ? -1 : 1);

        return new Vector3(x, 0f, z).normalized;
    }

    public void AddSpeed()
    {
        if (rb.isKinematic) return;

        currentSpeed = Mathf.Min(currentSpeed + Random.Range(speedIncrement, 1f), maxSpeed);
        rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
    }


    public void StopBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, 1f, position.z);
    }

    public void ResetAndLaunch()
    {
        StopBall();
        SetPosition(Vector3.zero);
        Invoke(nameof(LaunchBall), delayBeforeStart);
    }

    public void ResetSpeed()
    {
        currentSpeed = initialSpeed;
    }


    void FixedUpdate()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
    }
}