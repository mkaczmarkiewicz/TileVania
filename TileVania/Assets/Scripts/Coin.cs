using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameSession>().AddToScore(10);
            AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position);
            //dodaj monetę do wyniku
            Destroy(gameObject);
        }
    }
}
