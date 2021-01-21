using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 5f;

    //state

    //cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
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

    private void FlipSprite()
    {
        bool playerMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerMoving)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
