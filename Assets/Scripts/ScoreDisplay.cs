using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] digitSprites;
    [SerializeField] private GameObject digitPrefab;

    public void UpdateScore(int score)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        string scoreText = score.ToString();

        foreach (char digit in scoreText)
        {
            int digitIndex = digit - '0';

            GameObject newDigit = Instantiate(digitPrefab, transform);

            Image image = newDigit.GetComponent<Image>();
            image.sprite = digitSprites[digitIndex];
        }
    }
}