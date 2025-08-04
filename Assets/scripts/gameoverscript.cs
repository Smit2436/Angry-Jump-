using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameoverscript : MonoBehaviour
{
    public static gameoverscript instance;
    public GameObject gameoverUI;
    public TextMeshProUGUI finalScoreText;
    public AudioClip gameOverSound;
    public TextMeshProUGUI finalCoinText;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        finalScoreText.text = "" + ScoreManager.finalScore.ToString();

        finalCoinText.text = "" + CoinManager.instance.currentCoins;
        
        Time.timeScale = 0f;
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    public void restart()
    {
        CoinManager.instance.ResetCoins();
        Time.timeScale = 1f;
        SceneManager.LoadScene("maingame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
