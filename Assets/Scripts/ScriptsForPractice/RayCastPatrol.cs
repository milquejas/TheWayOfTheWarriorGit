using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPatrol : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";

    private Rigidbody2D rb2D;

    [SerializeField] float moveSpeed;    
    [SerializeField] GameObject player;
    [SerializeField] Transform terrainCast;

    [SerializeField] float baseCastDistance;
    [SerializeField] float chaseRange;

    Vector3 baseScale;
    string facingDirection; 





    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb2D = GetComponent<Rigidbody2D>();

    }



    bool IsHittingWall()
    {
        bool val = false;

        float castDist = baseCastDistance;
        //define the castDistance for left and right
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDistance;
        }
        else
        {
            castDist = baseCastDistance;
        }
        //determine the target destination based on the cast distance
        Vector3 targetPos = terrainCast.position;
        targetPos.x += castDist;

        Debug.DrawLine(terrainCast.position, targetPos, Color.green);

        if (Physics2D.Linecast(terrainCast.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool IsNearEdge()
    {
        bool val = true;

        float castDist = baseCastDistance;
      
        //determine the target destination based on the cast distance
        Vector3 targetPos = terrainCast.position;
        targetPos.y -= castDist;

        Debug.DrawLine(terrainCast.position, targetPos, Color.red);

        if (Physics2D.Linecast(terrainCast.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }


// Update is called once per frame
    private void FixedUpdate()
    {
        float velocityX = moveSpeed;

        if(facingDirection== LEFT)
        {
            velocityX = -moveSpeed;

        }

        //enemyn liike
        rb2D.velocity = new Vector2(velocityX, rb2D.velocity.y);

        if (IsHittingWall() || IsNearEdge())
        {          
            if (IsHittingWall())
            {
                if(facingDirection == LEFT)
                {
                    ChangeFacingDirection(RIGHT);
                }
                else if (facingDirection == RIGHT)
                {
                    ChangeFacingDirection(LEFT);
                }
            }
        }

        void ChangeFacingDirection(string newDirection)
        {
            Vector3 newScale = baseScale;
            if(newDirection == LEFT)
            {
                newScale.x = -baseScale.x;
            }
            else if (newDirection == RIGHT)
            {
                newScale.x = baseScale.x;
            }
            transform.localScale = newScale;

            facingDirection = newDirection;

        }

       
    }

}
