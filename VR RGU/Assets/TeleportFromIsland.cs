using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    public Transform teleportTarget; // Drag a GameObject here
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player entered teleporter.");
            other.transform.position = teleportTarget.position;
        }
    }
}
