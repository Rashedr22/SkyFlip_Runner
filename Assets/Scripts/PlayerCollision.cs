using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
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

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
