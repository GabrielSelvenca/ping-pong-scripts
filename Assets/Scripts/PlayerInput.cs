using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour
{
    public float speed = 8.5f;

    [Header("Configuração automática")]
    public bool isRightPlayer;

    [Header("Limites")]
    public float minPosition = -3.5f;
    public float maxPosition = 3.5f;

    private float fixedX;
    private float fixedY;
    private Rigidbody rb;

    private string inputAxis;

    void Start()
    {
        isRightPlayer = transform.CompareTag("Bot");

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        fixedX = transform.position.x;
        fixedY = transform.position.y;

        inputAxis = isRightPlayer ? "Vertical2" : "Vertical";
    }

    void FixedUpdate()
    {
        if (!GameSettings.multiplayer && isRightPlayer)
            return;

        float move = Input.GetAxisRaw(inputAxis);
        float newZ = Mathf.Clamp(transform.position.z + move * speed * Time.fixedDeltaTime, minPosition, maxPosition);
        Vector3 targetPos = new Vector3(fixedX, fixedY, newZ);
        rb.MovePosition(targetPos);
    }
}