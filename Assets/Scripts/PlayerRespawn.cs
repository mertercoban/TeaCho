using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Reference to the point where the player will respawn
    public Transform spawnPoint;

    // Threshold below which the player is considered to have fallen off
    public float fallThreshold = -10f;

    // Update is called once per frame
    void Update()
    {
        // Check if the player’s y-position is below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            RespawnPlayer();  // Call the respawn function if the player has fallen
        }
    }

    // Respawn the player at the designated spawn point
    void RespawnPlayer()
    {
        transform.position = spawnPoint.position;  // Set player position to the spawn point
    }
}
