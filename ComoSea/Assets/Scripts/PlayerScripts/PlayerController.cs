using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D playerRB;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    private Vector3 direction;
    private double jumpBlockTime;
    float moveY;
    float moveX;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        jumpBlockTime = 1f;
    }

    /*
     * 
     *  This method is called every frame and is used to move the player around the screen
     *  it does this by getting the input from the keyboard and then moving the player in the direction of the arrow keys
     *  This method HAS TO BE CHANGED!!
     */

    void Update()
    {
        
    }

    void FixedUpdate()
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
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (jumpBlockTime <= 0f && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            Jump();
            jumpBlockTime = 1f;
        }
        else
        {
            jumpBlockTime -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Method to make the player jump when the W or the SPACE keys are pressed.
    /// </summary>
    void Jump()
    {
        Debug.Log("jumping!");
        playerRB.velocity = new Vector2(direction.x, moveY * jumpForce);
    }
}
