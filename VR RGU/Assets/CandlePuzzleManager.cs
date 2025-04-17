using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzleManager : MonoBehaviour
{
    public static CandlePuzzleManager instance;

    public List<int> correctOrder = new List<int> { 1, 3, 2, 0 };
    private List<int> currentOrder = new List<int>();

    public List<Candle> allCandles = new List<Candle>();
    public GameObject rewardObject;

    [Header("Door")]
    public GameObject doorObject;
    private Animator doorAnimator;

    [Header("Audio")]
    public AudioClip lightCandleSound;
    public AudioClip wrongOrderSound;
    public AudioClip puzzleCompleteSound;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        if (doorObject != null)
        {
            doorAnimator = doorObject.GetComponent<Animator>();
            doorAnimator.SetBool("Open", false); // Keep the door closed at start
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void CandleLit(Candle candle)
    {
        currentOrder.Add(candle.candleID);
        Debug.Log($"[PUZZLE] Candle {candle.candleID} added to order.");

        // 🔥 Play candle lit sound
        if (lightCandleSound != null)
            audioSource.PlayOneShot(lightCandleSound);

        if (currentOrder.Count == correctOrder.Count)
        {
            if (IsOrderCorrect())
            {
                Debug.Log("✅ Puzzle solved.");

                // ✅ Play success sound
                if (puzzleCompleteSound != null)
                    audioSource.PlayOneShot(puzzleCompleteSound);

                PuzzleComplete();
            }
            else
            {
                Debug.LogWarning("❌ Wrong order. Resetting candles...");

                // ❌ Play wrong order sound
                if (wrongOrderSound != null)
                    audioSource.PlayOneShot(wrongOrderSound);

                ResetAllCandles();
            }
        }
    }

    private bool IsOrderCorrect()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
                return false;
        }
        return true;
    }

    private void ResetAllCandles()
    {
        foreach (Candle candle in allCandles)
        {
            candle.ResetCandle();
        }

        currentOrder.Clear();
        Debug.Log("[PUZZLE] Candles have been reset.");
    }

    private void PuzzleComplete()
    {
        if (rewardObject != null)
        {
            rewardObject.SetActive(true);
            Debug.Log("[PUZZLE] Reward activated!");
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("Open", true);
            Debug.Log("[PUZZLE] Door triggered!");
        }
        else
        {
            Debug.LogWarning("[PUZZLE] No Animator found on door.");
        }
    }
}
