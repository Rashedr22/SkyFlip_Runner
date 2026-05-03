using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LootLockerGameScene : MonoBehaviour
{
    public static LootLockerGameScene instance;
    public GameObject leaderboardPanel;
    public TextMeshProUGUI[] leaderboardTexts;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        leaderboardPanel.SetActive(false);
    }

    // Called when the player finishes a run
    public void SubmitScore(int score)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");

        LootLockerSDKManager.SubmitScore(playerName, score, "main_leaderboard", (response) =>
        {
            if (response.success)
            {
                FetchLeaderboard();
            }
        });
    }

    // Gets the top 5 scores from all players
    void FetchLeaderboard()
    {
        LootLockerSDKManager.GetScoreList("main_leaderboard", 5, (response) =>
        {
            if (!response.success) return;

            for (int i = 0; i < leaderboardTexts.Length; i++)
            {
                if (i < response.items.Length)
                {
                    var entry = response.items[i];
                    string name = entry.player.name;

                    if (string.IsNullOrEmpty(name))
                    {
                        name = "Guest";
                    }

                    leaderboardTexts[i].text = "#" + entry.rank + "  " + name + "  " + entry.score;
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