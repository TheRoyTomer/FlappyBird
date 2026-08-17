using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private ScoreDisplay scoreDisplay;
    
    public static GameManagerScript Instance { get; private set; }
    
    public int Score { get; private set; }
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
        scoreDisplay.UpdateScore(Score);
    }

    public void GameOver()
    {
        IsGameOver = true;
    }
    
    public void AddScore()
    {
        Score++;
        scoreDisplay.UpdateScore(Score);
    }
}