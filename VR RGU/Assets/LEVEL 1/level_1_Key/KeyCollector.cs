using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI popupText;
    public float popupDuration = 2f;

    [SerializeField] private ParticleSystem sparkEffect; // Reference to particle system

    // Animator
    [SerializeField] private Animator npcAnimator;

    // Audio
    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private AudioClip npcNoKeysSound;
    [SerializeField] private AudioClip npcCraftSuccessSound;
    [SerializeField] private AudioClip npcAlreadyHaveKeySound;
    private AudioSource audioSource;

    private void Start()
    {
        popupText.gameObject.SetActive(false);
        if (keyPartAImage != null) keyPartAImage.enabled = false;
        if (keyPartBImage != null) keyPartBImage.enabled = false;
        if (fullKeyImage != null) fullKeyImage.enabled = false;

        if (keyCompleteEffect != null) keyCompleteEffect.SetActive(false);
        if (popupText != null) popupText.text = "";

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing on player object. Please add one.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyPartA"))
        {
            hasPartA = true;
            Destroy(other.gameObject);
            keyPartAImage.enabled = true;
            ShowPopup("You collected a broken part of a key.");
            PlayPickupSound();
        }
        else if (other.CompareTag("KeyPartB"))
        {
            hasPartB = true;
            Destroy(other.gameObject);
            keyPartBImage.enabled = true;
            ShowPopup("You collected a broken part of a key.");
            PlayPickupSound();
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

            if (sparkEffect != null)
            {
                sparkEffect.gameObject.SetActive(true);
                sparkEffect.Play();
                StartCoroutine(HideParticleEffectAfterDelay(sparkEffect, 2f)); // 👈 auto-hide after 2 sec
            }

            if (keyCompleteEffect != null) keyCompleteEffect.SetActive(false);

            ShowPopup("You've got both parts! I'll fix it for you.");
            PlayNpcSound(npcCraftSuccessSound);

            if (npcAnimator != null)
            {
                npcAnimator.SetTrigger("Point");
            }
        }
        else if (hasFullKey)
        {
            ShowPopup("I already made you the key.. maybe try that door over there.");
            PlayNpcSound(npcAlreadyHaveKeySound);
        }
        else
        {
            ShowPopup("Find the broken keys.");
            if (npcAnimator != null)
            {
                npcAnimator.SetTrigger("ShakeHead");
            }
            PlayNpcSound(npcNoKeysSound);
        }
    }

    private void ShowPopup(string message)
    {
        if (popupText != null)
        {
            StopAllCoroutines();
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

    private void PlayPickupSound()
    {
        if (coinPickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(coinPickupSound);
        }
        else
        {
            Debug.LogError("coinPickupSound or AudioSource is missing!");
        }
    }

    private void PlayNpcSound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("NPC sound or AudioSource is missing!");
        }
    }

    private System.Collections.IEnumerator HideParticleEffectAfterDelay(ParticleSystem ps, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Properly stop and clear the particle effect
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitWhile(() => ps.IsAlive(true)); // Wait until all particles finish

        ps.gameObject.SetActive(false);
    }

}
