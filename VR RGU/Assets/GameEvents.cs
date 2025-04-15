using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action eventCradleActive;
    public event Action eventCradleDeactive;

    public void setCradleActive()
    {
        Debug.Log("event function called");
        if (eventCradleActive != null)
        {
            eventCradleActive();
            Debug.Log("cradle active event");
        }

    }
    public void setCradleDeactive()
    {

        if (eventCradleDeactive != null)
        {
            eventCradleDeactive();

        }
    }
}