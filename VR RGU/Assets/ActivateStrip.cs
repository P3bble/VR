using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStrip : MonoBehaviour
{
    [SerializeField] private GameObject stripObject; // working now

    private void Start()
    {
        if (stripObject != null)
            stripObject.SetActive(false);

        if (GameEvents.current != null)
            GameEvents.current.eventCradleActive += Activate;
        GameEvents.current.eventCradleDeactive += Deactivate;
    }

    private void Activate()
    {
        if (stripObject != null)
            stripObject.SetActive(true);
    }
    private void Deactivate()
    {
        if (stripObject != null)
            stripObject.SetActive(false);
    }
}
