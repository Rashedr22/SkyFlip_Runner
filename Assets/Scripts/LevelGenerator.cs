using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject levelChunkPrefab; // Drag your prefab here
    public Transform player;            // Drag your Player here
    public float spawnDistance = 18f;   // How far ahead to spawn
    private Vector3 nextSpawnPoint;

    void Start()
    {
        // Spawn a few chunks to start with
        for (int i = 0; i < 5; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        // If player gets close to the end of the current world, spawn more
        if (Vector3.Distance(player.position, nextSpawnPoint) < spawnDistance)
        {
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        // Create the new ground
        Instantiate(levelChunkPrefab, nextSpawnPoint, Quaternion.identity);

        // Move the next spawn point forward by the width of your chunk
        // (Change '20' to match the actual width of your ground sprite)
        nextSpawnPoint += new Vector3(20, 0, 0);
    }
}

