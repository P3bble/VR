using UnityEngine;

public class GateTriggerZone : MonoBehaviour
{
    [SerializeField] private PuzzelGate1 gateToOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gateToOpen.OpenGate();
        }
    }
}
