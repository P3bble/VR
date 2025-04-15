using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this for TextMeshPro

public class KeyCollector : MonoBehaviour
{
    public bool hasPartA = false;
    public bool hasPartB = false;
    public bool hasFullKey = false;

    public GameObject keyCompleteEffect;

    // UI
    public Image keyPartAImage;
    public Image keyPartBImage;
    public Image fullKeyImage;

    public TextMeshProUGUI popupText; // 👉 Assign this in Inspector
    public float popupDuration = 2f;  // Duration before the message disappears

    private void Start()

    {
        popupText.gameObject.SetActive(false);
        if (keyPartAImage != null) keyPartAImage.enabled = false;
        if (keyPartBImage != null) keyPartBImage.enabled = false;
        if (fullKeyImage != null) fullKeyImage.enabled = false;

        if (keyCompleteEffect != null) keyCompleteEffect.SetActive(false);
        if (popupText != null) popupText.text = ""; // Hide message at start
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyPartA"))
        {
            hasPartA = true;
            Destroy(other.gameObject);
            keyPartAImage.enabled = true;
            ShowPopup("You collected Key Part A!");
        }
        else if (other.CompareTag("KeyPartB"))
        {
            hasPartB = true;
            Destroy(other.gameObject);
            keyPartBImage.enabled = true;
            ShowPopup("You collected Key Part B!");
        }

        if (hasPartA && hasPartB && !hasFullKey)
        {
            if (keyCompleteEffect != null && !keyCompleteEffect.activeInHierarchy)
            {
                keyCompleteEffect.SetActive(true);
            }
        }
    }

    public void TryCombineKey()
    {
        if (hasPartA && hasPartB && !hasFullKey)
        {
            hasFullKey = true;

            fullKeyImage.enabled = true;
            keyPartAImage.enabled = false;
            keyPartBImage.enabled = false;

            if (keyCompleteEffect != null) keyCompleteEffect.SetActive(false);

            ShowPopup("You combined the key parts into a FULL KEY!");
        }
        else if (hasFullKey)
        {
            ShowPopup("You already have the full key.");
        }
        else
        {
            ShowPopup("You don't have both key parts yet.");
        }
    }

    private void ShowPopup(string message)
    {
        if (popupText != null)
        {
            StopAllCoroutines(); // Stop previous messages if any
            StartCoroutine(PopupRoutine(message));
        }
    }

    private System.Collections.IEnumerator PopupRoutine(string message)
    {
        popupText.text = message;
        popupText.gameObject.SetActive(true);
        yield return new WaitForSeconds(popupDuration);
        popupText.text = "";
        popupText.gameObject.SetActive(false);
    }
}
