using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateEnemyAI : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] float moveSpeed;
    [SerializeField] float chaseSpeed;

    [SerializeField] GameObject player;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform eyeCast;

    [SerializeField] float groundCheckRadius;
    [SerializeField] float raycastWallDistance;
    [SerializeField] float chaseRange;

    private bool isChase = false;
    private bool isSearching = false;
    private bool isFacingRight = true;
    public bool isGrounded = true;
    public bool isWalking = true;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Patrol()
    {
        isChase = false;
        isSearching = false;
        

        ////tarkastaa suunnan
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        //tarkastaa onko grounded
        isWalking = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //tarkastaa reunalla onko grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        //tarkastaa wallLayerin
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            direction, raycastWallDistance, wallLayer);



        //jos pelaaja on grounded tapahtuu t‰m‰
        if (isWalking)
        {
            // move the enemy in the direction it is facing
            rb2D.velocity = new Vector2(moveSpeed * (isFacingRight ? 1 : -1), rb2D.velocity.y);

            // jos enemy raycast osuu sein‰‰n tai tulee reunalle
            if (hit.collider != null || !isGrounded)
            {
                // make the enemy turn around
                Flip();
            }

        }

        //Animator.Play("")
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        //jos katsoo oikealle
        if (isFacingRight)
        {

            castDist = -distance;
        }

        //tarkastaa playerLayerin
        //RaycastHit2D hitPlayer = Physics2D.Raycast(eyeCast.transform.position, player.transform.position, playerLayer);

        Vector2 endPos = eyeCast.transform.position + Vector3.right * castDist;
        
        RaycastHit2D hitPlayer = Physics2D.Linecast(eyeCast.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        
        // jos enemy n‰kee pelaajan
        if (hitPlayer.collider != null)
        {
            if (hitPlayer.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(eyeCast.position, hitPlayer.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(eyeCast.position, hitPlayer.point, Color.red);
        }
        return val;

    }
    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            //jos enemy on pelaajan vasemmalla puolella, liiku oikealle
            rb2D.velocity = new Vector2(chaseSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            
        }
        else
        {
            //toisin p‰in, liikkuu vasemmalle
            rb2D.velocity = new Vector2(-chaseSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        //Animator.Play("")
    }

    void Search()
    {
        // set isChase to false and isSearching to true
        isChase = false;
        isSearching = true;

        // play a searching animation
        //Animator.Play("")

        // start a timer to keep track of how long the enemy has been searching
        float searchTimer = 0;

        // while the enemy is searching
        while (isSearching)
        {
            // update the search timer
            searchTimer += Time.deltaTime;

            // check if the enemy can see the player
            bool canSeePlayer = CanSeePlayer(chaseRange);

            // if the enemy can see the player, switch to chase state
            if (canSeePlayer)
            {
                isChase = true;
                isSearching = false;
                break;
            }

            // if the search timer reaches a certain amount of time (e.g. 5 seconds)
            if (searchTimer >= 5f)
            {
                // stop searching and go back to patrol state
                isSearching = false;
                break;
            }
        }
    }



    void Flip()
    {
        // flip funktio
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);

    }

    void FlipTowards()
    {
        // flip funktio
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);

        if (transform.position.x < player.transform.position.x)
        {
            //jos enemy on pelaajan vasemmalla puolella, liiku oikealle
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            //toisin p‰in, liikkuu vasemmalle
            transform.localScale = new Vector2(-1, 1);
        }


    }


    // Update is called once per frame
    void Update()
    {

        // jos enemy n‰kee pelaajan alkaa chase
        if (CanSeePlayer(chaseRange))
        {
            isChase = true;
        }
        else
        {
            // t‰ss‰ toiminto isChase = true mutta pelaaja ei ole n‰kyvill‰
            if (isChase)
            {
                // enemy etsii pelaajaa
                if (!isSearching)
                {
                    //jos ei lˆyd‰ pelaajaa || Invoke Patrol
                    isSearching = true;
                    //jos toiminto toimisi niin enemy pit‰isi etsi‰ 5 sekuntia pelaajaa ja sit invoke patrol
                    Invoke(nameof(Patrol), 5f);
                }

            }

        }
        //t‰ss‰ toiminto jos isChase = true, chase jatkuu ja pelaaja on n‰kyviss‰
        if (isChase)
        {
            ChasePlayer();
        }

        //m‰‰ritys pelaajan ja enemyn v‰liselle et‰isyydelle
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //jos et‰isyys pelaajaan on v‰hemm‰n kun annettu chaseRange arvo 
        if (distToPlayer < chaseRange)
        {
            // ChasePlayer funktio
            ChasePlayer();
        }
        else
        {
            // Stop funktio chase ja aloittaa patrol
            Patrol();
        }
    }
}
