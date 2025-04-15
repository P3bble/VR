using UnityEngine;

public class TeleportOnKey : MonoBehaviour
{
    public Transform teleportTarget;         // Where the player will be sent
    public KeyCollector playerKeyCollector;  // Reference to the player's KeyCollector script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerKeyCollector != null && playerKeyCollector.hasFullKey)
            {
                other.transform.position = teleportTarget.position;
                Debug.Log("Teleported to new area!");
            }
            else
            {
                Debug.Log("You need the full key to enter.");
            }
        }
    }
}
