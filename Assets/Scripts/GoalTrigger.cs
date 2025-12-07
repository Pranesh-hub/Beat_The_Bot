using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public bool isRightGoal;        // tick TRUE on Right_Goal, FALSE on Left_Goal
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        gameManager.OnGoalScored(isRightGoal);
    }
}
