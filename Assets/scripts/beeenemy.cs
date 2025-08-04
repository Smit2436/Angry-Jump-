using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTriggerKillPlayer : MonoBehaviour
{
    public string playerTag = "Player";              
    public GameObject deathEffectPrefab;             
    public string gameOverSceneName = "game over";    
    public float delayBeforeLoad = 0.5f;           

    private bool playerDead = false;

    void OnTriggerEnter(Collider other)
    {
        if (playerDead) return;

        if (other.CompareTag(playerTag))
        {
            playerDead = true;

            
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, other.transform.position, Quaternion.identity);
            }

          
            other.gameObject.SetActive(false);

            Invoke("LoadGameOverScene", delayBeforeLoad);
        }
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}
