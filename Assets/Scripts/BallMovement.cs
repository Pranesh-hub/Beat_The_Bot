using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BallMovement : MonoBehaviour
{
    public float BallSpeed = 8f;
    public Rigidbody2D rb;

    private Vector2 lastFrameVelocity;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;

        rb.velocity = Vector2.right * BallSpeed;
    }

    void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
