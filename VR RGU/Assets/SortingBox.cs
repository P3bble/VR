using UnityEngine;

public class SortingBox : MonoBehaviour
{
    private int corruptedCount = 0;
    private int uncorruptedCount = 0;
    public int maxItems = 6;

    // play sound
    public AudioSource correctPlacementSound;

    public int CorruptedCount => corruptedCount;
    public int UncorruptedCount => uncorruptedCount;

    private void OnTriggerEnter(Collider other)
    {
        // which object entered the trigger zone
        Debug.Log("Object entered: " + other.name);

        //puzzle isn't complete
        if (corruptedCount + uncorruptedCount >= maxItems * 2) // both boxes are full
        {
            Debug.Log("Puzzle completed! No more items allowed.");
            return;
        }

        // "Corrupted" or "Uncorrupted"
        if (other.CompareTag("Corrupted") || other.CompareTag("Uncorrupted"))
        {
            // object belongs to the correct box
            if (gameObject.CompareTag("CorruptedBox") && other.CompareTag("Corrupted") && corruptedCount < maxItems)
            {
                corruptedCount++;
                FreezeObject(other);  // Freeze the object
                Debug.Log("Added to Corrupted Box. Current count: " + corruptedCount);
                PlayCorrectSound(); // Play sound
            }
            else if (gameObject.CompareTag("UncorruptedBox") && other.CompareTag("Uncorrupted") && uncorruptedCount < maxItems)
            {
                uncorruptedCount++;
                FreezeObject(other);
                Debug.Log("Added to Uncorrupted Box. Current count: " + uncorruptedCount);
                PlayCorrectSound();
            }
            else
            {
                
                Debug.LogWarning("Incorrect box: " + other.name);
            }
        }
    }

    // freeze it in place
    private void FreezeObject(Collider objectCollider)
    {
        Rigidbody rb = objectCollider.GetComponent<Rigidbody>();
        if (rb != null)
        {
          
            objectCollider.transform.position = transform.position;
            rb.isKinematic = true; 
            objectCollider.transform.SetParent(transform);
            Debug.Log("Object " + objectCollider.name + " is frozen in the box.");
        }
        else
        {
            Debug.LogError("Object " + objectCollider.name + " doesn't have a Rigidbody.");
        }
    }

    
    private void PlayCorrectSound()
    {
        if (correctPlacementSound != null)
        {
            correctPlacementSound.Play();  // Play the sounds
        }
        else
        {
            Debug.LogError("AudioSource for correct placement sound is not assigned!");
        }
    }
}
