using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 3.5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManagerScript.Instance.IsGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManagerScript.Instance.GameOver();
        rb.linearVelocity = Vector2.zero;
    }
}