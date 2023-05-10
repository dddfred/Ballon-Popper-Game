using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_script : MonoBehaviour
{
    /*
    public GameObject spritePrefab;
    public GameObject spriteInstance;
    public float speed = 5f;

    public void instatiatePin()
    {
        spriteInstance = Instantiate(spritePrefab, transform.position, Quaternion.identity);
    }

    public void movePin()
    {
        spriteInstance.transform.Translate(Vector2.right * speed * Time.deltaTime);

    }*/

    //[SerializeField] AudioSource audio;
    public GameObject spritePrefab;
    public List<GameObject> spriteInstances = new List<GameObject>();
    public float speed = 10f;
    Ball_Script ball;
    Score scoreScript;
   




    private void Start()
    {
        //initialize
        ball = GameObject.FindObjectOfType<Ball_Script>();
        scoreScript = GameObject.FindObjectOfType<Score>();
        
    }

    public void instantiatePin(Vector2 position)
    {
        if (!Pause.isPaused) //to prevent user input when paused
        {
            Debug.Log("Instantiating pin...");
            float yOffset = 0.5f; // adjust this value as needed to position the pin at the center of the player
            Vector2 spawnPosition = new Vector2(position.x, position.y + yOffset);
            GameObject newSprite = Instantiate(spritePrefab, spawnPosition, Quaternion.Euler(0f, 0f, 90f));
            spriteInstances.Add(newSprite);
        }
      
        
    }

    public void movePins()
    {

        for (int i = spriteInstances.Count - 1; i >= 0; i--)
        {
            if (spriteInstances[i] == null || !spriteInstances[i].activeSelf)
            {
                // Remove pin instance from the list if it has been destroyed or deactivated
                spriteInstances.RemoveAt(i);
            }
            else
            {
                // Move pin instance down
                spriteInstances[i].transform.Translate(Vector2.down * speed * Time.deltaTime);
                //Debug.Log("Moving pin...");
            }
        }
    }

    //Makes the ball disapear once the pin collides with it
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        if (collider2D.gameObject.CompareTag("Ball"))
        {
            Debug.Log("HERE");
            
            //calculate Points
            scoreScript.calPoints();

            //play ballDestroy audion
            ball.playAudio();

            //Destroy ball
            ball.GetComponent<Animator>().SetTrigger("Destroyed");
            //Stop the ball from moving
            ball.GetComponent<Collider2D>().enabled = false;
            ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody2D>().gravityScale = 0;
            //wait for one second before destroying the ball to allow the animation to finish playing
            Destroy(collider2D.gameObject, 1.0f);

        
            

        }   
    }

    // Makes the pins disapear once they collide with the screen edge
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ScreenEdge") || collision.gameObject.CompareTag("insect") || collision.gameObject.CompareTag("ground"))
        {
            
            foreach (GameObject spriteInstance in spriteInstances)
            {
                Destroy(spriteInstance);
            }
            spriteInstances.Clear();
        }


    }



}
 