using UnityEngine;
using System.Collections;

public class GameOverUIAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform gameOverText;
    [SerializeField] private RectTransform scoreSection;
    [SerializeField] private RectTransform playButton;

    private Vector2 gameOverTextOriginalPosition;
    private Vector2 scoreSectionOriginalPosition;
    private Vector3 playButtonOriginalScale;

    private void Awake()
    {
        gameOverTextOriginalPosition = gameOverText.anchoredPosition;
        scoreSectionOriginalPosition = scoreSection.anchoredPosition;
        playButtonOriginalScale = playButton.localScale;
    }

    public void Hide()
    {
        gameOverText.gameObject.SetActive(false);
        scoreSection.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
    }

    public void Show()
    {
        StartCoroutine(ShowGameOverSequence());
    }

    private IEnumerator ShowGameOverSequence()
    {
        // Game Over text - slides from above
        gameOverText.gameObject.SetActive(true);

        Vector2 gameOverStartPosition =
            gameOverTextOriginalPosition + new Vector2(0f, 300f);

        yield return StartCoroutine(
            SlideIn(
                gameOverText,
                gameOverStartPosition,
                gameOverTextOriginalPosition,
                0.3f
            )
        );

        yield return new WaitForSeconds(0.1f);

        // Score section - slides from below
        scoreSection.gameObject.SetActive(true);

        Vector2 scoreStartPosition =
            scoreSectionOriginalPosition + new Vector2(0f, -300f);

        yield return StartCoroutine(
            SlideIn(
                scoreSection,
                scoreStartPosition,
                scoreSectionOriginalPosition,
                0.3f
            )
        );

        yield return new WaitForSeconds(0.1f);

        // Play button - scales in
        playButton.gameObject.SetActive(true);

        yield return StartCoroutine(
            ScaleIn(
                playButton,
                0.25f
            )
        );
    }

    private IEnumerator SlideIn(
        RectTransform rectTransform,
        Vector2 startPosition,
        Vector2 endPosition,
        float duration)
    {
        float time = 0f;

        rectTransform.anchoredPosition = startPosition;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            rectTransform.anchoredPosition =
                Vector2.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
    }

    private IEnumerator ScaleIn(
        RectTransform rectTransform,
        float duration)
    {
        float time = 0f;

        Vector3 startScale = Vector3.zero;
        Vector3 endScale = playButtonOriginalScale;

        rectTransform.localScale = startScale;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            rectTransform.localScale =
                Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        rectTransform.localScale = endScale;
    }
}