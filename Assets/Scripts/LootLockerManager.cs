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

        LootLockerSDKManager.StartGuestSession("104825", (response) =>
        {
            if (response.success)
                Debug.Log("LootLocker connected.");
        });
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
        if (nameInput.text.Trim().Length < 2) return;

        PlayerPrefs.SetString("PlayerName", nameInput.text.Trim());
        SceneManager.LoadScene("GameScene");
    }
}