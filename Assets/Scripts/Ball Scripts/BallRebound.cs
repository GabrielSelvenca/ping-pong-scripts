using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallRebound : MonoBehaviour
{
    public BallMoviment ballMoviment;

    [Header("Correção mínima no vetor")]
    public float minHorizontalForce = 0.2f;
    public float minVerticalForce = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (ballMoviment == null) return;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.isKinematic) return;
        ballMoviment.AddSpeed();

        Vector3 velocity = rb.linearVelocity;

        // Corrige se o eixo X estiver fraco (movimento horizontal)
        if (Mathf.Abs(velocity.x) < minHorizontalForce)
        {
            float directionX = velocity.x >= 0 ? 1f : -1f;
            velocity.x = directionX * minHorizontalForce;
        }

        // Corrige se o eixo Z estiver fraco (movimento vertical)
        if (Mathf.Abs(velocity.z) < minVerticalForce)
        {
            float directionZ = velocity.z >= 0 ? 1f : -1f;
            velocity.z = directionZ * minVerticalForce;
        }

        // Normaliza a direção com a mesma velocidade
        rb.linearVelocity = velocity.normalized * velocity.magnitude;
    }
}
