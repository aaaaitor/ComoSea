using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*
     * 
     *  This method is called every frame and is used to move the player around the screen
     *  it does this by getting the input from the keyboard and then moving the player in the direction of the arrow keys
     *  This method HAS TO BE CHANGED!!
     */

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
            animator.SetFloat("Speed", 1);

            Vector3 direction = (Vector3.up * moveY + Vector3.right * moveX).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
  
    }
}
