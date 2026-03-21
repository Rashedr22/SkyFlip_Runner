using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levelChunks; // Drag your prefab here
    public float chunkWidth = 20f;
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
        GameObject chunkToSpawn;

        // If it's the very first chunk (spawn point is still at start)
        if (nextSpawnPoint == Vector3.zero)
        {
            chunkToSpawn = levelChunks[0];
        }
        else
        {
            chunkToSpawn = levelChunks[Random.Range(0, levelChunks.Length)];
        }

        Instantiate(chunkToSpawn, nextSpawnPoint, Quaternion.identity);

        nextSpawnPoint += new Vector3(chunkWidth, 0, 0);
    }
}

