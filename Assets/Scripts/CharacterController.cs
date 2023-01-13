using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
//using UnityEditor.Animations;
using UnityEngine;

public class CharacterController : MonoBehaviour

{

    public float moveSpeed;
    public float jumpForce;
    public float runSpeed;

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

        transform.Translate(Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Run", true);

        }
        else
        {
            animator.SetBool("Run", false);
            if (grounded)
            {
                animator.SetTrigger("Idle");
            }
        }

        //if (Input.GetButtonDown("Run"))
        //{
        //    animator.SetBool("Run", true);
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
        }
        
    }
    
}
