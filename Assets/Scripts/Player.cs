using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    
    [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] const int Speed = 10;
    [SerializeField] bool isFacingright = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] Pin_script pin;
    [SerializeField] bool isGrounded = true;
    [SerializeField] float jumpForce = 500.0f;
    [SerializeField] Animator animator;

    const int IDLE = 0;
    const int RUN = 1;
    const int JUMP = 2;

    bool pinInstantiated = false; // flag to keep track of pin instantiation

    // Start is called before the first frame update
    void Start()
    {
        if (rigidbody2D == null)
            rigidbody2D = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
        animator.SetInteger("motion", IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 playerPosition = transform.position;
            pin.instantiatePin(playerPosition);
            pinInstantiated = true; // set flag to true
        }

        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
    }

    private void FixedUpdate()
    {
        //Movemement
        //rigidbody2D.MovePosition(rigidbody2D.position + movement * Speed * Time.fixedDeltaTime);
        rigidbody2D.velocity = new Vector2(Speed * movement, rigidbody2D.velocity.y);
        if (pin.spritePrefab != null && pin.spriteInstances.Count > 0)
        {
            pin.movePins();
        }

        if (movement < 0 && isFacingright || movement > 0 && !isFacingright)
            flip();
        if (jumpPressed && isGrounded)
            Jump();
        else
        {
            jumpPressed = false;
            if (isGrounded)
            {
                if (movement > 0 || movement < 0)
                {
                    animator.SetInteger("motion", RUN);
                }
                else
                {
                    animator.SetInteger("motion", IDLE);
                }
            }

        }



    }

    private void flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingright = !isFacingright;
    }

    private void Jump()
    {
        animator.SetInteger("motion", JUMP);
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
        //Debug.Log("jumped");
        jumpPressed = false;
        isGrounded = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "ground")
            isGrounded = true;
        // else
        //   Debug.Log(collision.gameObject.tag);
        animator.SetInteger("motion", IDLE);
    }

}
