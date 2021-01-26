using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 10f;

    //state

    //cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;       
        myAnimator.SetBool("running", playerMoving);       
    }

    private void Jump()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                myRigidbody.velocity += new Vector2(0f, jumpForce);
            }
        }
    }

    private void FlipSprite()
    {
        bool playerMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerMoving)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
