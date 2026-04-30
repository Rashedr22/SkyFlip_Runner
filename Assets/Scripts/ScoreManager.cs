using UnityEngine;
using TMPro;
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
            scoreText.text = "Distance:  " + Mathf.FloorToInt(score);
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
