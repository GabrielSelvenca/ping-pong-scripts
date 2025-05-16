using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotIA : MonoBehaviour
{
    public Transform ball;
    public float maxSpeed = 8f;
    public float movementLimit = 3.5f;

    private float fixedX;
    private float fixedY;
    private Rigidbody rb;

    void Start()
    {
        fixedX = transform.position.x;
        fixedY = transform.position.y;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (GameSettings.multiplayer || ball == null) return;

        float botZ = transform.position.z;
        float ballZ = ball.position.z;
        float distance = ballZ - botZ;

        float moveZ = Mathf.Clamp(distance, -maxSpeed * Time.fixedDeltaTime, maxSpeed * Time.fixedDeltaTime);
        float newZ = Mathf.Clamp(botZ + moveZ, -movementLimit, movementLimit);

        Vector3 targetPos = new Vector3(fixedX, fixedY, newZ);
        rb.MovePosition(targetPos);
    }
}