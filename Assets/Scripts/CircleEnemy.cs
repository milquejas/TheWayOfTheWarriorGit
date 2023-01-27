using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;

    public float jumpForce = 10f;
    public float detectionDistance = 5f;
    private bool inAir = false;
    

    void Update()
    {
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
}

