using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAi : MonoBehaviour
{
    public float patrolSpeed = 2.0f; // movement speed while patrolling
    public float attackDelay = 2.0f; // time between attacks
    public Transform[] patrolPoints; // array of patrol points
    public Transform player; // reference to the player's transform component

    private int currentPatrolPoint = 0; // the current patrol point the enemy is moving towards
    private bool attacking = false; // whether or not the enemy is attacking the player

    private void Update()
    {
        if (!attacking)
        {
            Patrol();
        }
        else
        {
            Attack();
        }
    }

    private void Patrol()
    {
        // Move the enemy towards the current patrol point
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].position, patrolSpeed * Time.deltaTime);

        // If the enemy has reached the patrol point, move to the next one
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.1f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        }

        // Check if the player is in line of sight while patrolling
        if (CanSeePlayer())
        {
            attacking = true;
        }
    }

    private void Attack()
    {
        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, patrolSpeed * Time.deltaTime);

        // Check if the enemy has reached the player
        if (Vector2.Distance(transform.position, player.position) < 0.1f)
        {
            Debug.Log("Enemy attacking!");
            StartCoroutine(WaitBeforeNextAttack());
        }
    }

    private IEnumerator WaitBeforeNextAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attacking = false;
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
