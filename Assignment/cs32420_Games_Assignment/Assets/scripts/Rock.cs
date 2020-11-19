using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool rockTriggered;
    public int rockSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        rockTriggered = false;
        rockSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Rock OnTriggerEnter triggered");
            rockTriggered = true; 
        }
        
    }

    public void rockMovement()
    {
        Debug.Log("Rock rockMovement");
        GetComponent<Rigidbody2D>().velocity = new Vector2(-rockSpeed, GetComponent<Rigidbody2D>().velocity.x);
    }
}
