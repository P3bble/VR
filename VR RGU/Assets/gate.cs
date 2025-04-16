using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ActiveStrip;
    [SerializeField] private KeyCollector playerKeyCollector;

    private bool ballInCradle = false;

    void Start()
    {
        if (ActiveStrip != null)
            ActiveStrip.SetActive(false);

        CloseGate();

        GameEvents.current.eventCradleActive += OnCradleActivated;
        GameEvents.current.eventCradleDeactive += OnCradleDeactivated;
    }

    private void OnCradleActivated()
    {
        ballInCradle = true;

        if (playerKeyCollector != null && playerKeyCollector.hasFullKey)
        {
            OpenGate();
            playerKeyCollector.SendMessage("ShowPopup", "The gate is opening!"); 
        }
        else
        {
            Debug.Log("Ball is in cradle, but player does NOT have the full key yet.");
            playerKeyCollector.SendMessage("ShowPopup", "You need the full key!"); 
        }
    }

    private void OnCradleDeactivated()
    {
        ballInCradle = false;
        CloseGate();
    }

    private void OpenGate()
    {
        if (ActiveStrip != null)
            ActiveStrip.SetActive(true);

        animator.SetBool("Open", true);
        Debug.Log("Gate is opening.");
    }

    private void CloseGate()
    {
        if (ActiveStrip != null)
            ActiveStrip.SetActive(false);

        animator.SetBool("Open", false);
        Debug.Log("Gate is closing.");
    }

    // if the player gets key after ball is placed in cradle
    public void CheckGateConditions()
    {
        if (ballInCradle && playerKeyCollector != null && playerKeyCollector.hasFullKey)
        {
            OpenGate();
            playerKeyCollector.SendMessage("ShowPopup", "The gate is opening!");
        }
    }
}
