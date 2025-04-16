using UnityEngine;

public class BottleLore : MonoBehaviour
{
    [TextArea(3, 10)]
    public string loreText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoreDisplay.Instance.ShowLore(loreText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoreDisplay.Instance.HideLore();
        }
    }
}
