using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    public float jumpHeight;
    public float speed;
    // time in seconds to wait before chasing the player again
    
    
    public float attackDelay; 
    private bool isAttacking;
    private float attackTimer;
    public float attackRange;
    public float raycastDistance;
    public float patrolRange;
    public Rigidbody2D rb;
    public GameObject player;
    
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform groundCheck;
    public Transform wallCheck;
    public float groundCheckRadius;
    public float wallCheckRadius;
    public bool isFacingRight;
    public bool isGrounded = true;
    [SerializeField] Transform groundCheckPoint;

    void Start()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Check if the enemy is grounded
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, groundLayer);

        if (!isGrounded)
        {
            Flip();
        }


        // Check the distance between the enemy and the player
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= attackRange && !isAttacking)
        {
            // If the player is within the attack range, attack
            Attack();
        }
        else if (!isAttacking)
        {
            // Follow the player
            rb.velocity = new Vector2(speed * (player.transform.position.x - transform.position.x), rb.velocity.y);
        }

        // If the enemy is not in attack range and not chasing the player, patrol
        if (distance > attackRange && !isAttacking)
        {
            Patrol();
        }
        // if the enemy is attacking
        if (isAttacking)
        {
            // decrease the attack timer
            attackTimer -= Time.deltaTime;

            // if the attack timer is less than 0
            if (attackTimer < 0)
            {
                // set isAttacking to false
                isAttacking = false;
            }
        }
    }

    void Patrol()
    {
        // cast a ray from the enemy's position in the direction it is facing
        RaycastHit2D hit = Physics2D.Raycast(transform.position, isFacingRight ? Vector2.right : Vector2.left, raycastDistance, wallLayer);

        // if the raycast hits a wall
        if (hit.collider != null)
        {
            // make the enemy turn around
            Flip();
        }

        // move the enemy in the direction it is facing
        rb.velocity = new Vector2(speed * (isFacingRight ? 1 : -1), rb.velocity.y);
    }

    void Attack()
    {

        if (isGrounded)
        {
            // Calculate the distance between the enemy and the player
            float distance = Vector2.Distance(transform.position, player.transform.position);
            // Calculate the jump force needed to reach the player
            float jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpHeight);
            // Get the horizontal direction towards the player
            float xDirection = (player.transform.position.x > transform.position.x) ? 1 : -1;
            // Calculate the jump velocity
            Vector2 jumpVelocity = new Vector2(xDirection * jumpForce * distance, jumpForce);
            // Apply the jump velocity to the enemy's rigidbody
            rb.velocity = jumpVelocity;
            Debug.Log("Enemy attacks!");
        }
    }

   

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}





