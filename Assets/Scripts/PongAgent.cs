using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

[RequireComponent(typeof(Rigidbody2D))]
public class PongAgent : Agent
{
    public Rigidbody2D rb;          // this paddle RB
    public Rigidbody2D ballRb;      // ball RB (assigned from GameManager or inspector)
    public Transform opponent;      // other paddle
    public float moveSpeed = 8f;
    public bool isRightSide;        // tick true for right paddle

    private Vector2 startPos;

    public override void Initialize()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position=startPos;
        // reset paddle; ball reset is handled by GameManager
        rb.velocity = Vector2.zero;
        //rb.position = startPos;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Ball position and velocity: where is the ball, how is it moving?
        sensor.AddObservation(ballRb.position);   // 2 floats
        sensor.AddObservation(ballRb.velocity);   // 2 floats

        // This paddle: where am I and how fast am I moving vertically?
        sensor.AddObservation(rb.position.y);     // 1 float
        sensor.AddObservation(rb.velocity.y);     // 1 float

        // Opponent position: gives context about other paddle
        sensor.AddObservation(opponent.position.y); // 1 float

        // Side indicator: tells network left vs right (symmetry broken)
        sensor.AddObservation(isRightSide ? 1f : 0f); // 1 float
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // One discrete action: 0=stay, 1=up, 2=down
        int a = actions.DiscreteActions[0];
        float vertical = 0f;
        if (a == 1) vertical = 1f;
        else if (a == 2) vertical = -1f;

        rb.velocity = new Vector2(0f, vertical * moveSpeed);

        // Small time penalty to encourage finishing rallies (scoring) sooner
        AddReward(-0.00002f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Optional: manual control for testing
        var da = actionsOut.DiscreteActions;
        da[0] = 0;
        if (!isRightSide)
        {
            if (Input.GetKey(KeyCode.W)) da[0] = 1;
            if (Input.GetKey(KeyCode.S)) da[0] = 2;
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) da[0] = 1;
            if (Input.GetKey(KeyCode.DownArrow)) da[0] = 2;
        }
    }
}
