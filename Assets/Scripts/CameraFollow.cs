using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Drag your Player here
    public float offsetX;     // How far to the right/left the camera sits

    void Start()
    {
        // Automatically calculate the distance between camera and player at the start
        offsetX = transform.position.x - player.position.x;
    }

    void Update()
    {
        if (player != null)
        {
            // Move the camera to match the player's X position
            // We keep the Y and Z the same so the camera doesn't bounce up and down
            transform.position = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }
}
