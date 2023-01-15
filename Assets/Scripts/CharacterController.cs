using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
//using UnityEditor.Animations;
using UnityEngine;

public class CharacterController : MonoBehaviour

{

    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    private bool Walk = true;
    private bool battleWalk = true;
    private bool battleRun = true;




    public Animator animator;
    public Rigidbody2D rb2D;


    
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;
    




    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LogAfterSecond(5));
    }

    //IEnumerator LogAfterSecond(float seconds)
    //{
    //    print("Timer Starts");
    //    yield return null;
    //    print("second passed");
    //}

    // Update is called once per frame
    void Update()
    {


        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true);

        }
        else
        {
            animator.SetBool("Walk", false);
            if (grounded)
            {
                animator.SetTrigger("Idle");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Walk || battleWalk)
            {
                moveSpeed = runSpeed;
                Walk = false;
                animator.SetBool("Run", true);
                animator.SetBool("battleRun", true);



            }
            else
            {
                moveSpeed = walkSpeed;
                Walk = true;
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);

            }
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    if (battleWalk)
        //    {
        //        moveSpeed = runSpeed;
        //        battleWalk = false;
        //        animator.SetBool("battleRun", true);


        //    }
        //    else
        //    {
        //        moveSpeed = walkSpeed;
        //        Walk = true;
        //        animator.SetBool("battleWalk", true);
        //        animator.SetBool("Run", false);
        //    }
        //}


        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");

        }

        if (Input.GetButtonDown("takeWeapon"))
        {
            animator.SetBool("takeWeapon", true);
            animator.SetBool("battlePosition", true);
        }

        if (Input.GetButtonDown("returnWeapon"))
        {

            animator.SetBool("returnWeapon", true);
            animator.SetBool("battlePosition", false);
            animator.SetBool("takeWeapon", false);
        }
        
    }
    
}
