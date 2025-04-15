using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cradleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CradleBall")
        {
            GameEvents.current.setCradleActive();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CradleBall")
        {
            GameEvents.current.setCradleDeactive();
        }
    }
}
