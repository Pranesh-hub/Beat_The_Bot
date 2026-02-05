using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
    public float BallSpeed = 8f;
    public Rigidbody2D rb;

    private Vector2 lastFrameVelocity;

<<<<<<< HEAD
    public GameManager gameManager;
=======
>>>>>>> upstream/main
    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
    }

    void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
        if (collision.gameObject.CompareTag("Player"))
        {
            PongAgent agent = collision.collider.GetComponent<PongAgent>();
            if (agent != null)
            {
                agent.AddReward(+0.005f);
            }
        }

=======
>>>>>>> upstream/main
        if (!collision.gameObject.CompareTag("Wall") &&
            !collision.gameObject.CompareTag("Player"))
            return;

        if (lastFrameVelocity.sqrMagnitude < 0.0001f) return;

        Vector2 normal = Vector2.zero;
        for (int i = 0; i < collision.contactCount; i++)
        {
            normal += collision.GetContact(i).normal;
        }

        if (normal == Vector2.zero) return;
        normal.Normalize();

        ContactPoint2D c = collision.GetContact(0);

        float speed = lastFrameVelocity.magnitude;
        Vector2 direction = lastFrameVelocity.normalized;

        Vector2 reflectedDir = Vector2.Reflect(direction, normal).normalized;
        rb.velocity = reflectedDir * speed;
    }
}
