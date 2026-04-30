using UnityEngine;
using LootLocker.Requests;

public class LootLockerConnect : MonoBehaviour
{
    private string gameKey = "dev_6538957de6204e3aa214455d91f420b3";

    void Start()
    {
        Debug.Log("Starting LootLocker session...");

        LootLockerSDKManager.StartGuestSession(gameKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("LootLocker Connected. Player ID: " + response.player_id);
            }
            else
            {
                Debug.LogError("LootLocker Failed (no detailed error in this SDK version)");
            }
        });
    }
}