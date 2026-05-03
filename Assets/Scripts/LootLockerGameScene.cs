using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LootLockerGameScene : MonoBehaviour
{
    public static LootLockerGameScene instance;

    public GameObject leaderboardPanel;
    public TextMeshProUGUI[] leaderboardTexts;

    private string playerName = "";

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        leaderboardPanel.SetActive(false);
        playerName = PlayerPrefs.GetString("PlayerName", "Guest");

        LootLockerSDKManager.StartGuestSession("104825", (response) =>
        {
            if (response.success)
                Debug.Log("LootLocker connected in game scene.");
        });
    }

    public void SubmitScore(int score)
    {
        string name = PlayerPrefs.GetString("PlayerName", "Guest");

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success) return;

            LootLockerSDKManager.SubmitScore(name, score, "main_leaderboard", (res) =>
            {
                FetchLeaderboard();
            });
        });
    }

    void FetchLeaderboard()
    {
        LootLockerSDKManager.GetScoreList("main_leaderboard", 5, (response) =>
        {
            Debug.Log("Fetch response success: " + response.success);
            if (!response.success) return;
            Debug.Log("Leaderboard fetched, items: " + response.items.Length);

            if (!response.success) return;

            for (int i = 0; i < leaderboardTexts.Length; i++)
            {
                if (i < response.items.Length)
                {
                    var e = response.items[i];
                    leaderboardTexts[i].text = "#" + e.rank + "  " + e.metadata + "  " + e.score;
                }
                else
                {
                    leaderboardTexts[i].text = "";
                }
            }

            leaderboardPanel.SetActive(true);
        });
    }
}