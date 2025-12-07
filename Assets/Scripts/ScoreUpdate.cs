using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI RightScoreText;
    public int Right_score = 0;
    public TextMeshProUGUI LeftScoreText;
    public int Left_score = 0;
    public bool isRight = true;
    void Start()
    {
        RightScoreText.text = "Right Player's Score: " + Right_score.ToString();
        LeftScoreText.text = "Left Player's Score: " + Left_score.ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Goal Scored");
        if (collision.gameObject.CompareTag("Ball") && isRight)
        {
            Debug.Log("Left Player Scored");
            Left_score++;
            LeftScoreText.text = "Left Player's Score: " + Left_score.ToString();
        }
        else if (collision.gameObject.CompareTag("Ball") && !isRight)
        {
            Debug.Log("Right Player Scored");
            Right_score++;
            RightScoreText.text = "Right Player's Score: "+ Right_score.ToString();        
        }       
    }
}
