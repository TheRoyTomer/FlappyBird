using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float destroyX = -5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameManagerScript.Instance.IsGameOver)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = Vector2.left * moveSpeed;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}