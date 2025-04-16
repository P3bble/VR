using UnityEngine;

public class PuzzelGate1 : MonoBehaviour
{
    public Animator gateAnimator;

  
    public void OpenGate()
    {
        if (gateAnimator != null)
        {
            gateAnimator.SetTrigger("Open");
            Debug.Log("Gate Opened!");
        }
        else
        {
            Debug.LogError("Gate Animator is not assigned!");
        }
    }
}
