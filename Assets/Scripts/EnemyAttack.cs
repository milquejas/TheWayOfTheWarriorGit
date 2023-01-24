using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
 
    public CharacterController player;

    public void AttackPlayer()
    {
        player.TakeDamage();
    }
}

