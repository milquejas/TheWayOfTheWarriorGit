using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float jumpHeight;
    public float speed;
    private float attackDelay = 10f;
    private bool isAttacking;    
    public float attackRange;
    public float raycastDistance;
    public float patrolRange;
    public Rigidbody2D rb;
    public GameObject player;
    
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public float wallCheckRadius;
    public bool isFacingRight;
    public bool isGrounded = true;


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
            StartCoroutine(AttackCoolDown());
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
    }

    void Patrol()
    {
        //tarkastaa suunnan
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        // cast a ray from the enemy's position in the direction it is facing
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, wallLayer);

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

    IEnumerator AttackCoolDown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StopCoroutine(AttackCoolDown());
    }
   

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}





