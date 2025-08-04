// CoinManager.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public static CoinManager instance;
    public int currentCoins = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
    void Update()
    {
        if (CoinManager.instance != null)
        {
            coinText.text = "" + CoinManager.instance.currentCoins;
        }
    }

    public void AddCoin(int value)
    {
        currentCoins += value;
    }

    public void ResetCoins()
    {
        currentCoins = 0;
    }
}
