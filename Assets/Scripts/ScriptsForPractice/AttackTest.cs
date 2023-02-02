using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour
{
    public float jumpHeight;
    public float speed;
    public Rigidbody2D rb2D;
    

    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform groundCheck;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public float raycastWallDistance;
    public float raycastPlayerDistance;
    private bool isFacingRight;
    public bool isGrounded = true;
    public bool isWalking = true;
    

    public Transform player;
    public float chaseSpeed = 8f;
    private float minDepth;
    private float maxDepth;

    void Start()
    {
        isFacingRight = true;
        rb2D = GetComponent<Rigidbody2D>();

    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    void Patrol()
    {
        // move the enemy in the direction it is facing
        rb2D.velocity = new Vector2(speed * (isFacingRight ? 1 : -1), rb2D.velocity.y);
    }
    
    public void Chase()
    {
        //alkaa jahtaamaan pelaajaa kun raycast osuu pelaajaan
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position,
        isFacingRight ? Vector2.right : Vector2.left, raycastPlayerDistance, playerLayer);
             
        //Follow the player
        rb2D.velocity = new Vector2(chaseSpeed * (player.transform.position.x - transform.position.x), rb2D.velocity.y);

    }
    //private void FollowPlayer()
    //{
    ////    if(playerOnRange)
    ////    {
    ////        ////tarkastaa suunnan
    //        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

    ////        tarkastaa playerLayerin
    //        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position,
    //           direction, raycastPlayerDistance, playerLayer);

    ////        // Calculate the distance between the enemy and the player
    ////        float distance = Vector2.Distance(transform.position, player.transform.position);

    ////        // Get the horizontal direction towards the player
    ////        float xDirection = (player.transform.position.x > transform.position.x) ? 1 : -1;

    //        //Follow the player
    //        rb2D.velocity = new Vector2(speed * (player.transform.position.x - transform.position.x), rb2D.velocity.y);

    ////    }

    //}

    private void Update()
    {
        ////tarkastaa suunnan
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        //tarkastaa onko grounded
        isWalking = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //tarkastaa reunalla onko grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
       
        //tarkastaa seinät
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            direction, raycastWallDistance, wallLayer);

        //tarkastaa playerLayerin
        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position,
              direction, playerLayer);


        // if the raycast hits a wall
        if (hit.collider != null)
        {
            // make the enemy turn around
            Flip();
        }

        if (isGrounded)
        {
            Patrol();
        }

        if (hitPlayer.collider != null)
        {
            Chase();
        }

        //float distance = Vector2.Distance(transform.position, player.position);
        //if (distance < followRange)
        //{
        //    float xDirection = (player.transform.position.x > transform.position.x) ? 1 : -1;
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime * xDirection);
        //}
        //tarkastaa enemyn ja pelaajan välisen etäisyyden
        //float distance = Vector2.Distance(transform.position, player.position);
        //float xDirection = (player.transform.position.x > transform.position.x) ? 1 : -1;

        //if (distance < followRange)
        //{
        //    transform.position = Vector2.MoveTowards
        //        (transform.position, player.position,xDirection, speed * Time.deltaTime);

        //}
        //if (playerOnRange)
        //{
        //    FollowPlayer();
        //    if (!playerOnRange)
        //    {
        //        Patrol();
        //    }
        //}
    }
    
}
