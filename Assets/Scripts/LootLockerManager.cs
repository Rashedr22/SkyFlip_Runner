using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class LootLockerManager : MonoBehaviour
{
    public static LootLockerManager instance;
    public GameObject nameEntryPanel;
    public TMP_InputField nameInput;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        nameEntryPanel.SetActive(false);

        // Check if this device already has a player ID
        string savedID = PlayerPrefs.GetString("PlayerID", "");

        if (savedID == "")
        {
            // First time — let LootLocker create a new unique player
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    PlayerPrefs.SetString("PlayerID", response.player_identifier);
                    Debug.Log("New player created: " + response.player_identifier);
                }
            });
        }
        else
        {
            // Returning player — log in with the saved ID
            LootLockerSDKManager.StartGuestSession(savedID, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Returning player: " + savedID);
                }
            });
        }
    }

    public void OnPlayPressed()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            nameEntryPanel.SetActive(true);
        }
    }

    public void ConfirmName()
    {
        string chosenName = nameInput.text.Trim();
        if (chosenName.Length < 2) return;

        PlayerPrefs.SetString("PlayerName", chosenName);

        LootLockerSDKManager.SetPlayerName(chosenName, (response) =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }
}