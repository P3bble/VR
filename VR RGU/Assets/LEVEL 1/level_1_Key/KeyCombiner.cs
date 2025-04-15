using UnityEngine;

public class KeyCombiner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyCollector collector = other.GetComponent<KeyCollector>();
            if (collector != null)
            {
                collector.TryCombineKey();
            }
        }
    }
}
