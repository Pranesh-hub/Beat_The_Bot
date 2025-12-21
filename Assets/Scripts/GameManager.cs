using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public Rigidbody2D ballPrefab;      // prefab from Project
    private Rigidbody2D currentBall;    // active ball instance
    public Transform spawnPoint;

    [Header("Random Spawn Settings")]
    public float maxSpawnYOffset = 2f;   // how far up/down from spawnPoint
    public float maxLaunchAngle = 30f;   // degrees away from pure left/right

    [Header("UI")]
    public TextMeshProUGUI RightScoreText;
    public TextMeshProUGUI LeftScoreText;

    [Header("Scores")]
    public int Right_score = 0;
    public int Left_score = 0;

    [Header("Pong Agents")]
    public PongAgent leftAgent;
    public PongAgent rightAgent;

    void Start()
    {
        UpdateUI();

        Vector2 firstDir = (Random.value < 0.5f) ? Vector2.left : Vector2.right;
        SpawnBall(firstDir);
    }

    public void OnGoalScored(bool scoredOnRightGoal)
    {
        if (currentBall != null)
            Destroy(currentBall.gameObject);

        if (scoredOnRightGoal)
        {
            Left_score++;
            leftAgent.AddReward(+1f);
            rightAgent.AddReward(-1f);
        }
        else
        {
            Right_score++;
            rightAgent.AddReward(+1f);
            leftAgent.AddReward(-1f);
        }

        UpdateUI();

        leftAgent.EndEpisode();
        rightAgent.EndEpisode();

        Vector2 dir = scoredOnRightGoal ? Vector2.left : Vector2.right;
        SpawnBall(dir);
    }

    void SpawnBall(Vector2 baseDirection)
    {
        Vector3 pos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        float yOffset = Random.Range(-maxSpawnYOffset, maxSpawnYOffset);
        pos.y += yOffset;

        currentBall = Instantiate(
            ballPrefab,
            pos,
            Quaternion.identity
        );

        leftAgent.ballRb = currentBall;
        rightAgent.ballRb = currentBall;

        Vector2 dir = baseDirection.normalized;
        float angle = Random.Range(-maxLaunchAngle, maxLaunchAngle);
        Quaternion rot = Quaternion.Euler(0f, 0f, angle);
        Vector2 randomizedDir = rot * dir;

        var bm = currentBall.GetComponent<BallMovement>();
        if (bm != null)
        {
            currentBall.velocity = randomizedDir.normalized * bm.BallSpeed;
        }
        else
        {
            currentBall.velocity = randomizedDir.normalized * 8f;
        }
    }

    void UpdateUI()
    {
        LeftScoreText.text  = "Left Player's Score: "  + Left_score;
        RightScoreText.text = "Right Player's Score: " + Right_score;
    }
}
