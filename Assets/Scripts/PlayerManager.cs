using System.Collections;
using UnityEngine;
using TMPro;  

public class PlayerManager : MonoBehaviour
{
    // References to Rigidbody and Animator components
    Rigidbody2D player;
    Animator animator;

    // Movement speed and direction
    public float moveSpeed = 1f;
    public bool facingRight = true;

    // Jump properties
    public float jumpSpeed = 1f;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;  // Radius for the ground check
    public LayerMask groundLayer;

    private int jumpCount = 0;  // Track how many jumps have been used
    public int maxJumps = 1;    // Allow double jump (2 jumps)

    // Slide control
    public bool slide = false;

    // Score variables
    public int score = 0;  // Current score
    private int highScore = 0;  // High score
    public TextMeshProUGUI scoreText;  // TextMeshPro UI for displaying the score
    public TextMeshProUGUI highScoreText;  // TextMeshPro UI for displaying the high score

    // Reference to AudioManager for sound effects
    AudioManager audioManager;

    // Find and reference the AudioManager in the scene
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D and Animator components
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Load the high score from storage
        LoadHighScore();

        // Initialize the score display
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle horizontal movement
        HorizontalMove();

        // Flip character direction based on movement
        if (player.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (player.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }

        // Check for jump input and ensure jumps are within allowed limits
        if (Input.GetButtonDown("Vertical") && jumpCount < maxJumps)
        {
            VerticalMove();
        }

        // Check for slide input
        if (Input.GetButtonDown("Jump"))
        {
            slide = true;
            animator.SetBool("Slide", true);
            moveSpeed *= 2;  // Increase movement speed while sliding
        }

        // Stop sliding when button is released
        if (Input.GetButtonUp("Jump"))
        {
            slide = false;
            animator.SetBool("Slide", false);
            moveSpeed /= 2;  // Reset movement speed
        }
    }

    // FixedUpdate is called at fixed intervals
    void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jump count when on the ground
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    // Handle horizontal movement based on input
    void HorizontalMove()
    {
        player.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, player.velocity.y);

        // Update animation speed based on movement
        animator.SetFloat("MoveSpeed", Mathf.Abs(player.velocity.x));

        // Play walking sound effect if moving horizontally
        if (player.velocity.x > 0)
        {
            audioManager.PlaySFX(audioManager.walk);
        }
    }

    // Apply jump velocity and update jump count
    void VerticalMove()
    {
        player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        jumpCount++;
    }

    // Flip the player's facing direction
    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }

    // Called when the player collides with "Food" objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            score += 1;  // Increase score
            UpdateScoreUI();  // Update score display

            // Update high score if the current score exceeds it
            if (score >= highScore)
            {
                highScore = score;
                SaveHighScore();  // Save the new high score
                UpdateScoreUI();
            }

            Destroy(other.gameObject);  // Destroy the food object
        }
    }

    // Update the score and high score on the UI
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
            PlayerPrefs.SetInt("Score", score);
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    // Save the high score to PlayerPrefs
    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);  // Save the high score
        PlayerPrefs.Save();  // Make data persistent
    }

    // Load the high score from PlayerPrefs
    void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
        }
    }
}
