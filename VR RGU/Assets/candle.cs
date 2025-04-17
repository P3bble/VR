using UnityEngine;

public class Candle : MonoBehaviour
{
    public int candleID; // ID for candle
    public bool isLit = false;

    public GameObject flameEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!isLit && other.CompareTag("Player"))
        {
            LightCandle();
            CandlePuzzleManager.instance.CandleLit(this);
        }
    }

    public void LightCandle()
    {
        isLit = true;
        if (flameEffect != null)
            flameEffect.SetActive(true);
    }

    public void ResetCandle()
    {
        isLit = false;
        if (flameEffect != null)
            flameEffect.SetActive(false);
    }
}
