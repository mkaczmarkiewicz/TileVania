using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myCollider;
    BoxCollider2D groundDetector;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        groundDetector = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed *= -1;

        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }
}
