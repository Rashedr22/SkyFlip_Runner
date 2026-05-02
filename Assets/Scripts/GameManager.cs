using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject score;
    public int crystalCount = 0;
    public TextMeshProUGUI crystalText;

    public AudioSource ambience;
    public AudioSource footsteps;
    public GameObject jumpButton;
    public GameObject flipButton;

    void Awake()
    {
        instance = this;
    }

    public void AddCrystal(int amount)
    {
        crystalCount += amount;

        if (crystalText != null)
            crystalText.text = "  " + crystalCount;

        Debug.Log("Crystals: " + crystalCount);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        isGameOver = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Stop audio immediately so the death feels impactful
        if (ambience != null) ambience.Stop();
        if (footsteps != null) footsteps.Stop();

        // Disable input buttons so player can't jump/flip during the death pause
        jumpButton.SetActive(false);
        flipButton.SetActive(false);
    }

    // Called after the 1-second delay
    public void FreezeAndHideUI()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        score.SetActive(false);
    }

    public void ResumeGame()
    {
        if (isGameOver) return;

        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (isGameOver) return;

        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

}