using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerCollision : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI finalScoreText;
    public ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");

        // Stop time
        Time.timeScale = 0f;

        // Stop score
        scoreManager.StopScore();

        // Show Game Over UI
        gameOverPanel.SetActive(true);

        // Display final score
        finalScoreText.text = "Score: " + scoreManager.GetScore();
    }
}
