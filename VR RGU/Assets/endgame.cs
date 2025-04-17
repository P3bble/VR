using UnityEngine;
using TMPro;

public class ThanksTrigger : MonoBehaviour
{
    public GameObject thanksMessage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (thanksMessage != null)
            {
                thanksMessage.SetActive(true);
                Debug.Log("Thanks message displayed.");
            }
        }
    }
}
