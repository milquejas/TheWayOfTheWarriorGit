using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Rigidbody2D rb2D;
    private Animator animator;


    [SerializeField]
    private float minJumpForce = 5f, maxJumpForce = 40f;

    [SerializeField]
    private float minWaitTime = 1.5f, maxWaitTime = 3.5f;

    private float jumpTimer;

    private bool canJump;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpTimer = Time.time + Random.Range(minWaitTime, maxWaitTime);
    }

    private void Update()
    {
        HandleJumping();
        HandleAnimations();
    }

    void HandleAnimations()
    {
        if (rb2D.velocity.magnitude == 0)
            animator.SetBool("Jump", false);
        else
            animator.SetBool("Jump", true);
    }

    void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rb2D.velocity = new Vector2(0f, Random.Range(minJumpForce, maxJumpForce));
        }
    }

    void HandleJumping()
    {
        if (Time.time > jumpTimer)
        {
            jumpTimer = Time.time + Random.Range(minWaitTime, maxWaitTime);
            Jump();
        }

        if (rb2D.velocity.magnitude == 0) 
        {
            canJump = true;
        }
            
    }

} // class

//public class EnemyMovement : MonoBehaviour
//{
//    float enemySpeed;

//    public AudioSource enemyActive;
//    public AudioSource BackgroundMusic; 

//public class Enemy : MonoBehaviour
//{
//    public float enemySpeed = 3;

//    public AudioSource enemyActive;
//    public AudioSource BackgroundMusic;

//    void enemyMovement()
//    {
//        enemySpeed += 0.0001f;
//        transform.Translate(enemySpeed, 0, 0);
//    }

//    void Start()
//    {
//        enemyActive.GetComponent<AudioSource>();
//        BackgroundMusic.GetComponent<AudioSource>();
//        BackgroundMusic.Stop();
//        enemyActive.Play();
//        enemySpeed = 0f;
//    }

//    void Update()
//    {
//        enemyMovement();
//    }
//}

//void enemyMovement()
//    {
//        enemySpeed += 0.0001f;
//        transform.Translate(enemySpeed, 0, 0);
//    }

//    void Start()
//    {
//        enemyActive.GetComponent<AudioSource>();
//        BackgroundMusic.GetComponent<AudioSource>();
//        BackgroundMusic.Stop();
//        enemyActive.Play();
//        enemySpeed = 0f;
//    }

//    void Update()
//    {
//        enemyMovement();
//    }
//}
