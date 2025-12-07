using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 8f;
    public bool isRight = true;
    public Rigidbody2D rb;

    float moveInput;

    void Update()
    {
        if (isRight)
        {
            if (Input.GetKey(KeyCode.UpArrow))       moveInput = 1f;
            else if (Input.GetKey(KeyCode.DownArrow)) moveInput = -1f;
            else                                      moveInput = 0f;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))             moveInput = 1f;
            else if (Input.GetKey(KeyCode.S))        moveInput = -1f;
            else                                      moveInput = 0f;
        }
    }

    void FixedUpdate()
    {
        // Force X velocity to 0, only move in Y
        rb.velocity = new Vector2(0f, moveInput * PlayerSpeed);
    }
}
