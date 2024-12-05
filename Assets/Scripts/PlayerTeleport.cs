using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private bool isTeleporting = false;  // Prevents instant re-teleportation by adding a delay

    // Reference to the AudioManager for playing sound effects
    AudioManager audioManager;

    // Initialize the AudioManager reference
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Triggered when the player enters a collider marked as a gate
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is a gate and if teleportation delay is active
        if (collision.CompareTag("Gate") && !isTeleporting)
        {
            // Get the GatePair component attached to the gate object
            GatePair currentGate = collision.GetComponent<GatePair>();

            // Check if the gate has a valid GatePair component
            if (currentGate != null)
            {
                // Find the paired gate using its gateID
                GameObject otherGate = FindOtherGate(currentGate.gateID, collision.gameObject);

                // If a paired gate is found, start the teleportation coroutine
                if (otherGate != null)
                {
                    StartCoroutine(TeleportPlayer(otherGate.transform.position));
                }
            }
        }
    }

    // Coroutine to handle teleportation with a slight delay
    private System.Collections.IEnumerator TeleportPlayer(Vector3 targetPosition)
    {
        isTeleporting = true;  // Activate teleportation delay
        audioManager.PlaySFX(audioManager.teleport);  // Play teleport sound effect

        yield return new WaitForSeconds(0.1f);  // Short delay before teleporting

        transform.position = targetPosition;  // Move the player to the target position

        yield return new WaitForSeconds(0.1f);  // Delay to avoid immediate re-teleportation
        isTeleporting = false;  // Deactivate teleportation delay
    }

    // Finds the other gate in the same pair based on the gateID
    private GameObject FindOtherGate(int gateID, GameObject currentGate)
    {
        // Get all GatePair objects in the scene
        GatePair[] allGates = FindObjectsOfType<GatePair>();

        // Loop through each gate to find the matching gateID, excluding the current gate
        foreach (GatePair gate in allGates)
        {
            if (gate.gateID == gateID && gate.gameObject != currentGate)
            {
                return gate.gameObject;  // Return the matching gate object
            }
        }
        return null;  // Return null if no matching gate is found
    }
}
