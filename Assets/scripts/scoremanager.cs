using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static int finalScore = 0;

    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

           
            if (rb.linearVelocity.y <= 0f)
            {
                ContactPoint contact = collision.contacts[0];

                
                if (contact.point.y < transform.position.y)
                {
                    AddScore(1);
                }
            }
        }
    }

    public void AddScore(int value)
    {
        score += value;
        finalScore = score;

        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void ResetScore()
    {
        score = 0;
        finalScore = 0;
    }

    public int GetScore()
    {
        return score;
    }
}
