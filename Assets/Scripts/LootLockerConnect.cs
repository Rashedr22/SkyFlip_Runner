using UnityEngine;
using LootLocker.Requests;

public class LootLockerConnect : MonoBehaviour
{
    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("LootLocker Connected");
            }
            else
            {
                Debug.Log("LootLocker Failed");
            }
        });
    }
}