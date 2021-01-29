using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float climbSpeed = 5f;

    //state

    //cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    BoxCollider2D myFeetCollider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        Run();
        Jump();
        Climb();
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
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                myRigidbody.velocity += new Vector2(0f, jumpForce);
            }
        }
    }

    private void Climb()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) && Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Epsilon)
        {

            myAnimator.SetBool("climbing", true);

            float controlThrow = Input.GetAxis("Vertical") * climbSpeed;
            Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, controlThrow);
            myRigidbody.velocity = playerVelocity;

            runSpeed = 3f; //zeby nie spadać z drabiny
        }
        else
        {
            myAnimator.SetBool("climbing", false);

            runSpeed = 5f;
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
