using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    float scrollRate = 0.1f;

    void Start()
    {
        
    }

    
    void Update()
    {
        float yMovement = scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0, yMovement));
    }
}
