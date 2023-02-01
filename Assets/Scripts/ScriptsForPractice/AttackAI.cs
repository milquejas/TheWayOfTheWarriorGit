using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : MonoBehaviour
{
 
    public float attackDelay = 2.0f; // time between attacks
    public Transform player; // reference to the player's transform component

    private void Start()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);

            // Check if player is in line of sight
            if (CanSeePlayer())
            {
                Debug.Log("Enemy attacking!");
            }
        }
    }

    private bool CanSeePlayer()
    {
        // Use raycasting to check if there's anything blocking the line of sight to the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position);

        if (hit.collider != null && hit.collider.transform == player)
        {
            return true;
        }
        return false;
    }
}

