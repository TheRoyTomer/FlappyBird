using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ScoreDisplay scoreDisplay;
    
    [SerializeField] private ScoreDisplay finalScoreDisplay;
    [SerializeField] private ScoreDisplay bestScoreDisplay;
    
    [SerializeField] private GameOverUIAnimator gameOverUIAnimator;
    
    public static GameManagerScript Instance { get; private set; }
    
    public int Score { get; private set; }
    
    public int BestScore { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }
    
    private void Start()
    {
        scoreDisplay.gameObject.SetActive(true);
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        scoreDisplay.UpdateScore(Score);
        gameOverUIAnimator.Hide();
        
    }

    public void GameOver()
    {
        IsGameOver = true;
        scoreDisplay.gameObject.SetActive(false);

        
        UpdateFinalScore();
        UpdateBestScore();
        gameOverUIAnimator.Show();
        
    }
    
    public void AddScore()
    {
        Score++;
        scoreDisplay.UpdateScore(Score);
    }
    
    
    private void UpdateFinalScore()
    {
        finalScoreDisplay.UpdateScore(Score);
    }
    
    private void UpdateBestScore()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore", BestScore);
            PlayerPrefs.Save();
        }

        bestScoreDisplay.UpdateScore(BestScore);
    }
    
    public void PlayAgain()
    {
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Default"),
            LayerMask.NameToLayer("Pipe"),
            false
        );

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}