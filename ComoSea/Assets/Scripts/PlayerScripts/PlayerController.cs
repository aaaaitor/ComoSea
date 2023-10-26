using UnityEngine;

/// <summary>
/// This class controls the player movement and the way the sprites interact with inputs.
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D playerRB;

    PlayerStats playerStats;

    private Vector3 direction;
    private float moveY;
    private float moveX;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    /*
     * 
     *  This method is called every frame and is used to move the player around the screen
     *  it does this by getting the input from the keyboard and then moving the player in the direction of the arrow keys
     *  This method HAS TO BE CHANGED!!
     */

    void Update()
    {
        Movement();
        Jump();
    }

    /// <summary>
    /// Method to make the player jump when the SPACE keys is pressed. Also checks the vertical velocity of 
    /// the player's rigidbody and only allows the player to jump when it's 0 (when the player is in contact with
    /// something) so the player isn't able to fly by jumping continuously.
    /// </summary>
    void Jump()
    {
        if (playerRB.velocity.y != 0){ return; }

        if (Input.GetButtonDown("Jump"))
        {
            playerRB.AddForce(new Vector2(playerRB.velocity.x, playerStats.actualJumpForce));
        }
    }

    /// <summary>
    /// Method to control player movement and set the correct sprite animation.
    /// </summary>
    void Movement()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0)
        {
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
            animator.SetFloat("Speed", 1);

            // Vertical movement set to 0 so the player cant move in the Y axis
            direction = (Vector3.up * 0 + Vector3.right * moveX).normalized;
            transform.Translate(direction * playerStats.actualSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
