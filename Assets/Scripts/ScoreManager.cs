using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private float score = 0f;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime * 5; // speed of score
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    public void StopScore()
    {
        isGameOver = true;
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}
