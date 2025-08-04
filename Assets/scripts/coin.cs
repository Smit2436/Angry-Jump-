
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 0;
    public AudioClip collectSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin(coinValue);
            SoundManager.instance.PlaySound(collectSound);
            Destroy(gameObject);
        }
    }
}
