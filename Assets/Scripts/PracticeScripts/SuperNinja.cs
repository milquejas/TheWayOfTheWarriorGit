using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNinja : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce; 
    public float chaseSpeed;

    public bool tracking; 
    public Vector3 lastKnownPosition; 

    private Animator animator;
    private Rigidbody2D rb2D;
    public bool chasing;
    public float forwardDetect; 
    public bool dead; 

    public GameObject player;
    // Voisi olla my�s public Transform detectionPoint;
    public GameObject detectionPoint;  

    // Suunta mihin Goomba kulkee. private sana ei ole pakollinen. Serializefiel tekee sen, ett� muuttuja n�kyy inspectorissa
    // direction = 1 silloin kun Goomba menee oikealle ja -1 silloin kun Goomba menee vasemmalle
    private bool changeDir;
    [SerializeField]
    private float direction;

    public LayerMask groundLayer;
    public LayerMask wallLayer;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void Update()
    {
        if (dead)
        {
            return;
        }

        if (tracking)
        {
            if(Mathf.Abs(transform.position.x - lastKnownPosition.x) < 2)
            {
                tracking = false; 
            }
        }

        // aina kun muutetaan direction 1 tai -1 v�lill�, vaihtaa goomba suuntaa. 
        transform.Translate(moveSpeed  * Time.deltaTime * direction, 0, 0);
        transform.localScale = new Vector3(direction, 1, 1);
        if (chasing)
        {
            transform.Translate(chaseSpeed * Time.deltaTime * direction, 0, 0);
            Vector3 heading = player.transform.position - transform.position;
            direction = CheckDirection(transform.forward, heading, transform.up);
        }
    }

    void LateUpdate()
    {
        if (dead)
        {
            return;
        }
        // visualistointi
        Debug.DrawRay(detectionPoint.transform.position, Vector2.down, Color.green);
        
        // Raycast, real deal. Alasp�in suuntautuva
        RaycastHit2D hit = Physics2D.Raycast(detectionPoint.transform.position, Vector2.down, 1, groundLayer);

        if(hit.collider == null && !chasing && !tracking)
        {
            // T�m� if toteutuu vain jos s�de ei osu mihink��n collideriin. T�ll�in pit�� vaihtaa suuntaa. 
            ChangeDirection();
        }


        // Eteenp�in suuntautuva raycasti. 
        Debug.DrawRay(detectionPoint.transform.position, Vector2.right * forwardDetect * direction, Color.blue);

        RaycastHit2D hit2 = Physics2D.Raycast(detectionPoint.transform.position,
            new Vector2(direction, 0), forwardDetect, wallLayer);

        if (hit2.collider != null && !chasing && !tracking)
        {

            // T�m� if toteutuu vain jos s�de osuu johonkin collideriin. T�ll�in pit�� vaihtaa suuntaa. 
            ChangeDirection();
        }

        else if (hit2.collider != null && chasing || hit2.collider != null && tracking)
        {
            Jump();
        }
    }

    public void Jump()
    {
        rb2D.velocity = new Vector2(0, jumpForce);
    }

    void ChangeDirection()
    {
        direction *= -1;

    }

    public void Die()
    {
        /*
            Menee littuun
            Pys�htyy
            Otetaan pois Rigidbody
            Otetaan pois Collider
            Tuhotaan Gameobject 3 sekunnin kuluttua
        */
        dead = true; 
        animator.SetTrigger("Die");
        moveSpeed = 0;
        chaseSpeed = 0;
        chasing = false; 
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(gameObject, 3);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            chasing = true;
            forwardDetect = 2;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lastKnownPosition = player.transform.position;
            player = null;
            chasing = false;
            forwardDetect = 0.1f;
            tracking = true; 
            
        }
    }


    public float CheckDirection(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        Debug.DrawRay(transform.position, perp, Color.cyan);
        float dir = Vector3.Dot(perp, up);
        if (dir > 0f)
        {
            return 1;
        }
        else
        {
            return -1;
        }

    }
}
