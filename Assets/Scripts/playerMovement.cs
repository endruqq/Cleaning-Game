using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public Rigidbody2D rb;
    public Animator animator;
    private GrabItem grabItem;

    Vector2 movement;
    Vector2 lastMovement; 


    private void Start()
    {

        grabItem = gameObject.GetComponent<GrabItem>();
        grabItem.Direction = new Vector2 (0, -1);
    }
    void Update()
    {
       
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(movement.sqrMagnitude > .1f)
        {
            grabItem.Direction = movement.normalized;
        }

        
        if (movement.sqrMagnitude > 0)
        {
            lastMovement = movement.normalized;
        }

        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

      
        if (movement.sqrMagnitude == 0)
        {
            animator.SetFloat("LastMoveX", lastMovement.x);
            animator.SetFloat("LastMoveY", lastMovement.y);
        }
    }

    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
