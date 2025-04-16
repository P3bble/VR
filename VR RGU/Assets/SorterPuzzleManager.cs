using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // sorting Boxes
    public SortingBox corruptedBox;
    public SortingBox uncorruptedBox;

    // puzzle gate to open
    public PuzzelGate1 puzzleGate;

    // puzzle is complete
    public bool puzzleCompleted = false;

    // Update is called once per frame
    void Update()
    {
        CheckPuzzleCompletion();
    }

    // both boxes are filled correctly
    private void CheckPuzzleCompletion()
    {
        // Make sure both boxes are filled correctly with maxItems
        if (corruptedBox.CorruptedCount == corruptedBox.maxItems && uncorruptedBox.UncorruptedCount == uncorruptedBox.maxItems)
        {
            if (!puzzleCompleted)
            {
                puzzleCompleted = true;
                Debug.Log("Puzzle Complete! All items sorted correctly.");

                // open the gate
                if (puzzleGate != null)
                {
                    puzzleGate.OpenGate();  // gate to open
                }
            }
        }
        else
        {
            Debug.Log("Puzzle In Progress...");
        }
    }
}
