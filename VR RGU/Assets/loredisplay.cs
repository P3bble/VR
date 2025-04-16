using UnityEngine;
using TMPro;

public class LoreDisplay : MonoBehaviour
{
    public static LoreDisplay Instance;

    [SerializeField] private GameObject lorePanel;           // 👈 The panel that includes the paper image
    [SerializeField] private TextMeshProUGUI loreTextUI;     // 👈 The actual text element

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (lorePanel != null)
            lorePanel.SetActive(false);
    }

    public void ShowLore(string message)
    {
        if (lorePanel != null)
            lorePanel.SetActive(true);

        if (loreTextUI != null)
            loreTextUI.text = message;
    }

    public void HideLore()
    {
        if (lorePanel != null)
            lorePanel.SetActive(false);
    }
}
