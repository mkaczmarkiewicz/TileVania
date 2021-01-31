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
    bool isAlive;

    //cached
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    BoxCollider2D myFeetCollider;

    void Start()
    {
        isAlive = true;

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            Jump();
            Climb();
            FlipSprite();
            Die();
        }
        else
        {
            Respawn();
        }
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

    private void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            isAlive = false; //zabiera graczowi kontrolę
            myAnimator.SetBool("dying", true); //zaczyna animację umierania

            myRigidbody.velocity = new Vector2(0f, jumpForce); //wystrzel gracza w górę

            this.gameObject.layer = 13; //zmieniamy layer na "Dead Player" żeby Enemy nie wchodził z nim w interakcje          
        }
    }

    private void Respawn()
    {
        if(Input.anyKeyDown)
        {
            isAlive = true;
            myAnimator.SetBool("dying", false);

            this.gameObject.layer = 10;

            FindObjectOfType<GameSession>().ProcessPlayerDeath(); //wywołaj metodę z GameSession która przywraca gracza do życia i restartuje level
        }
    }
}

/*if (myRigidbody.velocity == new Vector2(0f, 0f)) //po tym jak się zatrzyma
            {
                myRigidbody.bodyType = RigidbodyType2D.Static; //statyczne rigidbody żeby ciało nie spadło przez mapę
                myCollider.enabled = false; // wyłączamy oba collidery...
                myFeetCollider.enabled = false; //...żeby przeciwnik się nie odbijał o ciało
            }*/

/* myFeetCollider.enabled = false; //...żeby przeciwnik się nie odbijał o ciało

 if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) //po tym jak się zatrzyma
 {
     myRigidbody.bodyType = RigidbodyType2D.Static; //statyczne rigidbody żeby ciało nie spadło przez mapę
     myCollider.enabled = false; // wyłączamy oba collidery...

     Debug.Log("a");
 }*/

/*
myCollider.enabled = true;
myFeetCollider.enabled = true;
myRigidbody.bodyType = RigidbodyType2D.Dynamic;
            */