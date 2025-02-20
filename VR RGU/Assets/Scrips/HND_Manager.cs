using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



public class HND_Manager : MonoBehaviour
{
    [SerializeField] GameObject xrPlayer;
    [SerializeField] GameObject FPSPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Using Device:" + XRSettings.loadedDeviceName);
        if (XRSettings.isDeviceActive || XRSettings.loadedDeviceName == "OpenXR Display")
        {
            xrPlayer.SetActive(true);
            FPSPlayer.SetActive(false);
        }
        else
        {
            xrPlayer.SetActive(false);
            FPSPlayer.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
