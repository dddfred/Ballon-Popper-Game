using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Script : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] float amp;
    //[SerializeField] float freq;
    [SerializeField] Vector2 startForce;
    [SerializeField] float speed = 10f;
    public AudioSource audioSource;
    Vector2 initPos;
    Vector2 screenPos;
    Vector3 lastVel;
    [SerializeField] float growthRate = 0.1f; //how much to scale the ball object 

    Score scoreScript;
    
    public Vector3 originalBallSize;
    public float minSize = 0.1f; // The minimum size of the ball to give score
    public float maxSize = 10.0f; // The maximum size of the ball to give score


    public Animator explosionAnim;
    
    LevelManager sceneManger;
    AudioController audioController;
    // Start is called before the first frame update

    private void Awake()
    {
        sceneManger = GameObject.FindObjectOfType<LevelManager>();
        audioController = GameObject.FindObjectOfType<AudioController>();
    }

    void Start()
    {
        initPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, speed);
        originalBallSize = this.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        lastVel = rb.velocity;
        InvokeRepeating("ballExpansion", 2.0f, 1.0f);//calls ballExpansion() after 2sec and then every second
        CheckSize();
        Debug.Log("update getting called..");
    }

    private void FixedUpdate()
    {
        screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Reverse the ball's velocity when it hits the top edge of the screen
        if (screenPos.y >= Screen.height)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        // Reverse the ball's velocity when it hits the bottom edge of the screen
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
    }

    public void playAudio()
    {
        //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        audioController.playSFX(audioController.ballDestroyed);
    }



    //makes the ball grow bigger as time goes on
    public void ballExpansion()
    {
        transform.localScale += new Vector3(growthRate, growthRate, 0) * Time.deltaTime;
    }



    /*
    //calculate the ball size and Points
    public void calculatePoints()
    {
        float size = transform.localScale.x / originalBallSize.x;
        if(size >= minSize && size <= maxSize)
        {
            //int baseScore = FindObjectOfType<Score>().baseScore;
            float sizeFactor = 1.0f / size;
            
             scoreScript.score = Mathf.RoundToInt(scoreScript.baseScore * sizeFactor);
        }
    }*/


    //checks the size of the ball to determine when to destroy
    void CheckSize()
    {
        //if ball gets bigger than max size, its destroyed
        if(transform.localScale.x >= maxSize)
        {
           
            GetComponent<Animator>().SetTrigger("Destroyed");

            // Disable the collider to prevent any more collisions
            GetComponent<Collider2D>().enabled = false;

            // Disable the Rigidbody2D to prevent any more physics calculations
            rb.simulated = false;

            //play the ball destroyed audio
            playAudio();
            // Destroy the game object after the animation has finished playing
            float animationLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationLength);
            Debug.Log("Maximun size -> Ball Destroyed");
            sceneManger.restartLevel();
            
        }
    }


   


}
