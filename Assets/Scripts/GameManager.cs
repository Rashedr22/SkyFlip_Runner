using LootLocker.Requests;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject score;

    public AudioSource ambience;
    public AudioSource footsteps;

    void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        Time.timeScale = 0f;

        if (ambience != null) ambience.Stop();
        if (footsteps != null) footsteps.Stop();

        // UI control
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

    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
    }

}