using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LootLockerGameScene : MonoBehaviour
{
    public static LootLockerGameScene instance;
    public GameObject leaderboardPanel;
    public TextMeshProUGUI[] leaderboardTexts;

    private const string LeaderboardKey = "main_leaderboard";

    void Awake() { instance = this; }

    void Start()
    {
        leaderboardPanel.SetActive(false);
        // No new session here — LootLockerManager already handles it
    }

    public void SubmitScore(int score)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        string memberId = PlayerPrefs.GetString("LL_PlayerID", "Guest");

        // member ID = unique player, metadata = display name
        LootLockerSDKManager.SubmitScore(memberId, score, LeaderboardKey, playerName, (res) =>
        {
            if (res.success)
            {
                Debug.Log("Score submitted: " + score + " by " + playerName);
                FetchLeaderboard();
            }
            else
            {
                Debug.LogError("Score submission failed: " + res.errorData?.message);
            }
        });
    }

    void FetchLeaderboard()
    {
        LootLockerSDKManager.GetScoreList(LeaderboardKey, 5, (response) =>
        {
            if (!response.success)
            {
                Debug.LogError("Fetch failed: " + response.errorData?.message);
                return;
            }

            Debug.Log("Leaderboard items: " + response.items.Length);

            for (int i = 0; i < leaderboardTexts.Length; i++)
            {
                if (i < response.items.Length)
                {
                    var e = response.items[i];
                    string displayName = string.IsNullOrEmpty(e.metadata) ? "Guest" : e.metadata;
                    leaderboardTexts[i].text = $"#{e.rank}  {displayName}  {e.score}";
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