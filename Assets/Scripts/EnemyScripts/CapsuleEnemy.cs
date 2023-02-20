using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleEnemy : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;

    public float jumpForce = 10f;
    public float detectionDistance = 5f;
    private bool inAir = false;

    public float moveSpeed = 2f;
    public bool facingRight = true;

    public Transform groundCheck;
    public Transform wallCheck;

    private bool hittingGround;
    private bool hittingWall;

    void Update()
    {
        hittingGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        hittingWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (!hittingGround || hittingWall)
        {
            Flip();
        }

        Move();

        // Check if the player is within detection distance
        if (Vector2.Distance(transform.position, player.transform.position) < detectionDistance)
        {
            // Flip the enemy towards the player
            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            // Jump if not in the air
            if (!inAir)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                inAir = true;
            }
        }

        // Check if the enemy has landed
        if (GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            inAir = false;
        }
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < detectionDistance)
        {
            if (player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
        else
        {
            if (facingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}