using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AppleSpawner : MonoBehaviour
{
    // Array to hold different apple prefabs for spawning
    public GameObject[] applePrefab;

    // Interval between each spawn in seconds
    public float spawnInterval = 5f;

    // Maximum number of attempts to find a valid spawn position
    public int maxAttempts = 10;

    // Reference to the ground tilemap to check valid spawn positions
    public Tilemap groundTilemap;

    // Area within which apples can be spawned
    public BoxCollider2D spawnArea;

    /* Total game time in seconds (currently commented out) */
    // public float gameTime = 60f;

    // Flag to keep track if spawning should continue
    private bool keepSpawning = true;

    // Called at the start of the game
    void Start()
    {
        /* Start the game timer (currently commented out) */
        // StartCoroutine(GameTimer());

        // Start spawning apples repeatedly
        StartCoroutine(SpawnApples());
    }

    // Coroutine to spawn apples at regular intervals
    IEnumerator SpawnApples()
    {
        // Continue spawning while the keepSpawning flag is true
        while (keepSpawning)
        {
            // Generate a random valid spawn position
            Vector2 spawnPosition = GenerateValidSpawnPosition();

            // If a valid position is found, spawn an apple
            if (spawnPosition != Vector2.zero)
            {
                Instantiate(applePrefab[Random.Range(0, applePrefab.Length)], spawnPosition, Quaternion.identity);
            }

            // Wait for the specified spawn interval before spawning the next apple
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Method to generate a valid spawn position within the bounds of the spawn area
    Vector2 GenerateValidSpawnPosition()
    {
        // Try up to maxAttempts times to find a valid position
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Generate a random point within the spawn area's bounds
            Vector2 randomPosition = GetRandomPointInBounds(spawnArea.bounds);

            // Check if the generated position is on the ground tilemap
            if (!IsOnGround(randomPosition))
            {
                // Return the position if it's valid
                return randomPosition;
            }
        }

        // Return zero vector if no valid position was found
        return Vector2.zero;
    }

    // Helper method to get a random point within a given boundary
    Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(randomX, randomY);
    }

    // Method to check if a given position is on the ground tilemap
    bool IsOnGround(Vector2 position)
    {
        // Convert the world position to a tile position
        Vector3Int tilePosition = groundTilemap.WorldToCell(position);

        // Check if the tilemap has a tile at the specified position
        return groundTilemap.HasTile(tilePosition);
    }
}
