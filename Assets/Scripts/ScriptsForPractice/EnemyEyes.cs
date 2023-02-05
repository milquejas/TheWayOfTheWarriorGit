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
            //toisin p�in, liikkuu vasemmalle
            rb2D.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        //Animator.Play("")
    }

    void StopChasingPlayer()
    {
        isChase = false;
        isSearching = false;
        rb2D.velocity = new Vector2(0, 0);
        //Animator.Play("")
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
    // Update is called once per frame
    void Update()
    {

        // jos enemy n�kee pelaajan alkaa chase
        if (CanSeePlayer(chaseRange))
        {
            isChase = true;
        }
        else
        {
            // t�ss� toiminto isChase = true mutta pelaaja ei ole n�kyvill�
            if (isChase)
            {
                // enemy etsii pelaajaa
                if (!isSearching)
                {
                    //jos ei l�yd� pelaajaa || Invoke Stop
                    isSearching = true;
                    Invoke("StopChasingPlayer", 5f);
                }

            }

        }
        // t�ss� toiminto isChase = true jatkuu jos pelaaja n�kyviss�
        if (isChase)
        {
            ChasePlayer();
        }

        //m��ritys pelaajan ja enemyn v�liselle et�isyydelle
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //jos enemy on l�hemp�n� kuin chaseRange
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
}
