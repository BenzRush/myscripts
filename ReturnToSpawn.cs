using UnityEngine;

public class ReturnToSpawn : MonoBehaviour
{
    public Transform teleportTarget; // The public GameObject to teleport to

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            // Teleport the player to the position of the teleportTarget
            other.transform.position = teleportTarget.position;
        }
    }
}