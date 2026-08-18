using UnityEngine;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private Sprite[] birdSprites;
    [SerializeField] private float animationInterval = 0.12f;

    [Header("Angles")]
    [SerializeField] private float maxUpAngle = 30f;
    [SerializeField] private float maxDownAngle = -30f;
    [SerializeField] private float rotationSpeed = 7f;
    
    [SerializeField] private float deathAngle = -80f;
    [SerializeField] private float deathRotationSpeed = 10f;
    
    private Rigidbody2D rb;
    private InputAction jump;

    private SpriteRenderer spriteRenderer;
    private float animationTimer;
    private int currentSpriteIndex;
    
    private int pipeLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        var player = InputSystem.actions.FindActionMap("Player", throwIfNotFound: true);
        jump = player.FindAction("Jump", throwIfNotFound: true);
        
        pipeLayer = LayerMask.NameToLayer("Pipe");
    }

    private void Update()
    {
        if (GameManagerScript.Instance.IsGameOver)
        {
            RotateDownAfterGameOver();
            return;
        }
        
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationInterval)
        {
            currentSpriteIndex++;

            if (currentSpriteIndex >= birdSprites.Length)
            {
                currentSpriteIndex = 0;
            }

            spriteRenderer.sprite = birdSprites[currentSpriteIndex];
            animationTimer = 0f;
        }

        if (jump.WasPressedThisFrame())
        {
            rb.linearVelocity = Vector2.up * jumpForce;
            AudioManager.Instance.PlayWing();
        }
        
        UpdateRotation();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManagerScript.Instance.IsGameOver)
        {
            return;
        }
        
        AudioManager.Instance.PlayDeathSequence();
        GameManagerScript.Instance.GameOver();

        Physics2D.IgnoreLayerCollision(
            gameObject.layer,
            pipeLayer,
            true
        );
    }
    
    private void UpdateRotation()
    {
        float targetAngle;

        if (rb.linearVelocity.y > 0)
        {
            targetAngle = maxUpAngle;
        }
        else
        {
            targetAngle = maxDownAngle;
        }

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
    
    private void RotateDownAfterGameOver()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, deathAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            deathRotationSpeed * Time.deltaTime
        );
    }
    
}