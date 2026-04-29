using TMPro;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();

            ShowGameOverUI();
        }
    }

    void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + scoreManager.GetScore();

        scoreManager.StopScore();
    }
}
