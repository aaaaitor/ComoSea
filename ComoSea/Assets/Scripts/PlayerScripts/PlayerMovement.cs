using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Variables used for player movement
    Rigidbody2D playerRigidBody;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 movementDirection;
    [HideInInspector]
    public Vector2 lastVectorialDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        // Sets a base value to lastVectorialDirection
        lastVectorialDirection = new Vector2 (1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    // Update method that doesnt depend on frames
    void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// Function to control player input.
    /// </summary>
    void InputManagement()
    {
        // Sets horizontal and vertical axles
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Sets movement direction
        movementDirection = new Vector2(moveX, moveY).normalized;

        // Asigns last vectors of movement
        if (movementDirection.x != 0)
        {
            lastHorizontalVector = movementDirection.x;
            // Last x axis movement direction
            lastVectorialDirection = new Vector2(lastHorizontalVector, 0f);
        }
        if (movementDirection.y != 0)
        {
            lastVerticalVector = movementDirection.y;
            // Last y axis movement direction
            lastVectorialDirection = new Vector2(0f, lastVerticalVector);
        }
        if (movementDirection.x != 0 && movementDirection.y != 0)
        {
            // Last diagonal direction
            lastVectorialDirection = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    /// <summary>
    /// Controls player movement.
    /// </summary>
    void Movement()
    {
        playerRigidBody.velocity = new Vector2(movementDirection.x * /*player.currentSpeed*/2f, movementDirection.y * /*player.currentSpeed*/0f);
    }
}
