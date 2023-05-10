using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{

    Rigidbody2D rb;
    Vector3 lastVel;
    Vector2 screenPos;
    [SerializeField] float speed = 15;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, speed);
    }

    // Update is called once per frame
    void Update()
    {
        lastVel = rb.velocity;
        
    }

    private void FixedUpdate()
    {
        screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log(rb.velocity);
        // Reverse the insect's velocity when it hits the top edge of the screen
        if (screenPos.y >= Screen.height)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        // Reverse the insect's velocity when it hits the bottom edge of the screen
        else if (screenPos.y <= 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "ScreenEdge")
        {

            var speed = lastVel.magnitude;
            var direction = Vector2.Reflect(lastVel.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed;
        }

        if(collision.gameObject.tag == "Pin")
        {
            rb.velocity = lastVel;
        }

        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Player")
        {

            var speed = lastVel.magnitude;
            var direction = Vector2.Reflect(lastVel.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed;
        }
    }

   
}
