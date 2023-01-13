using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public Animator animator;
    public Rigidbody2D rb2D;

    public float moveSpeed;
    public float jumpForce;
    public float runSpeed;


    private bool isJumping;

    private float moveHorizontal;
    private float moveVertical;


    //public Transform groundCheckPosition;
    //public float groundCheckRadius;
    //public LayerMask groundCheckLayer;
    //public bool grounded;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 3f;
        jumpForce = 60f;
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f|| moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2 (moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);

        }

        if (!isJumping && moveVertical > 0.1f) 
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);

        }


    }

    private void OnTriggerEnter2D(Collider collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }
}
