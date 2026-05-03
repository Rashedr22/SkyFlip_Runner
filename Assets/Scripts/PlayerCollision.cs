using TMPro;
using UnityEngine;
using System.Collections;

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
            FreezePlayer();
            StartCoroutine(DelayedGameOver());
        }
    }

    void FreezePlayer()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic; // Stops gravity + movement

        // Also stop the PlayerController from setting velocity in Update
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null) pc.enabled = false;
    }

    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSecondsRealtime(1f);
        ShowGameOverUI();
    }

    void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + scoreManager.GetScore();
        scoreManager.StopScore();
    }
}
