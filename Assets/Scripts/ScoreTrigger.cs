using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManagerScript.Instance.AddScore();
            AudioManager.Instance.PlayPoint();
        }
    }
}