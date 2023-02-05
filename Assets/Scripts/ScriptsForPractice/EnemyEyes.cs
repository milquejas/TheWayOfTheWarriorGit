using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyes : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] Transform eyeCast;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject player;
    [SerializeField] float chaseRange;
    private bool isChase = false;
    private bool isSearching;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (isFacingRight)
        {
            castDist = -distance;
        }


        Vector2 endPos = eyeCast.transform.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(eyeCast.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(eyeCast.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(eyeCast.position, hit.point, Color.red);
        }
        return val;

    }
    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            //jos enemy on pelaajan vasemmalla puolella, liiku oikealle
            rb2D.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            //toisin päin, liikkuu vasemmalle
            rb2D.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        //Animator.Play("")
    }


    //ChatGPT kirjoittama vastike yllä olevalle
    //private void ChasePlayer()
    //{
    //    if (transform.position.x < player.transform.position.x)
    //    {
    //        // move right
    //        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    //        animator.SetBool("isWalking", true);
    //        animator.SetFloat("Horizontal", 1);
    //        GetComponent<SpriteRenderer>().flipX = false;
    //    }
    //    else if (transform.position.x > player.transform.position.x)
    //    {
    //        // move left
    //        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    //        animator.SetBool("isWalking", true);
    //        animator.SetFloat("Horizontal", -1);
    //        GetComponent<SpriteRenderer>().flipX = true;
    //    }
    //    else
    //    {
    //        animator.SetBool("isWalking", false);
    //    }
    //}
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

    void StopChasingPlayer()
    {
        isChase = false;
        isSearching = false;
        rb2D.velocity = new Vector2(0, 0);
        //Animator.Play("")
    }
    
    

    // Update is called once per frame
    void Update()
    {

        // jos enemy näkee pelaajan alkaa chase
        if (CanSeePlayer(chaseRange))
        {
            isChase = true;
        }
        else
        {
            // tässä toiminto isChase = true mutta pelaaja ei ole näkyvillä
            if (isChase)
            {
                // enemy etsii pelaajaa
                if (!isSearching)
                {
                    //jos ei löydä pelaajaa || Invoke Stop
                    isSearching = true;
                    Invoke("StopChasingPlayer", 5f);
                }

            }

        }
        // tässä toiminto isChase = true jatkuu jos pelaaja näkyvissä
        if (isChase)
        {
            ChasePlayer();
        }

        //määritys pelaajan ja enemyn väliselle etäisyydelle
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //jos enemy on lähempänä kuin chaseRange
        if (distToPlayer < chaseRange)
        {
            // ChasePlayer funktio
            ChasePlayer();
        }
        else
        {
            // Stop funktio
            StopChasingPlayer();
        }
    }

    //ChatGPT ´vastike updatelle
    //void Update()
    //{
    //    if (CanSeePlayer(chaseRange))
    //    {
    //        isChase = true;
    //        isSearching = false;
    //        CancelInvoke();
    //    }
    //    else if (isChase)
    //    {
    //        isSearching = true;
    //        Invoke(nameof(Patrol), 5f);
    //    }

    //    if (isChase)
    //    {
    //        ChasePlayer();
    //    }
    //    else if (isSearching)
    //    {
    //        Search();
    //    }
    //    else
    //    {
    //        Patrol();
    //    }

    //    float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
    //    if (distToPlayer < chaseRange)
    //    {
    //        ChasePlayer();
    //    }
    //}
}
