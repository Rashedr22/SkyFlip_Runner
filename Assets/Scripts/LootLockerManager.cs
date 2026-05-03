using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class LootLockerManager : MonoBehaviour
{
    public static LootLockerManager instance;
    public GameObject nameEntryPanel;
    public TMP_InputField nameInput;

    private const string GameKey = "104825";

    void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject); // session persists across scenes
    }

    void Start()
    {
        nameEntryPanel.SetActive(false);

        // Reuse stored player ID if we have one — keeps the same identity
        string storedId = PlayerPrefs.GetString("LL_PlayerID", "");

        if (!string.IsNullOrEmpty(storedId))
        {
            LootLockerSDKManager.StartGuestSession(storedId, OnSessionStarted);
        }
        else
        {
            // First time on this device — let LootLocker generate a new ID
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    PlayerPrefs.SetString("LL_PlayerID", response.player_identifier);
                    PlayerPrefs.Save();
                    Debug.Log("New LootLocker player created: " + response.player_identifier);
                }
                else Debug.LogError("LootLocker session failed: " + response.errorData?.message);
            });
        }
    }

    private void OnSessionStarted(LootLockerGuestSessionResponse response)
    {
        if (response.success) Debug.Log("LootLocker session restored.");
        else Debug.LogError("LootLocker session failed.");
    }

    public void OnPlayPressed()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
            SceneManager.LoadScene("GameScene");
        else
            nameEntryPanel.SetActive(true);
    }

    public void ConfirmName()
    {
        if (nameInput.text.Trim().Length < 2) return;
        PlayerPrefs.SetString("PlayerName", nameInput.text.Trim());
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}