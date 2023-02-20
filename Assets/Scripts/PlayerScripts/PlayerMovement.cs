using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public float runSpeed;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;

    public Animator animator;
    public Rigidbody2D rb2D;

    


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        localScale = transform.localScale;
        
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Mathf.Abs(dirX) > 0 && rb2D.velocity.y == 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed * 3;
            animator.SetBool("Run", true);
        }
            



        if (Input.GetButtonDown("Jump") && rb2D.velocity.y == 0)
            rb2D.AddForce(Vector2.up * 700f);

        if (rb2D.velocity.y == 0)
        {
            animator.SetBool("Jump", false);
            //animator.SetBool("Falls", false);

        }

        if (rb2D.velocity.y > 0)
            animator.SetBool("Jump", true);

        if (rb2D.velocity.y < 0 )
        {
            animator.SetBool("Jump", false);
            //animator.SetBool("Falls", true);
        }
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(dirX, rb2D.velocity.y);
    }

    private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }
}
