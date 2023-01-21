using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.Animations;
using UnityEngine;

public class CharacterController : MonoBehaviour

{
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    private bool Walk = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 30f;
    private float dashingTime =  0.2f;
    private float dashingCooldown = 1f;

    
    [SerializeField] private Animator animator;  
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private TrailRenderer tr;
    
    
    [SerializeField] private LayerMask groundCheckLayer;    
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public bool grounded;
    
    
    public AudioSource VoiceJump;
    public AudioSource takeHitAudio;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

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
            
            if (animator.GetBool("battlePosition"))
            {
                animator.SetBool("battleWalk", true);
            }
            else
            {
                animator.SetBool("Walk", true);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("battleWalk", false);
            if (grounded)
            {
                animator.SetTrigger("Idle");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Walk)
            {
                moveSpeed = runSpeed;
                Walk = false;
                if (animator.GetBool("battlePosition") == true)
                {
                    animator.SetBool("battleRun", true);
                }
                else
                {
                    animator.SetBool("Run", true);
                }
                        
            }
            else
            {
                moveSpeed = walkSpeed;
                Walk = true;
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                animator.SetBool("battleRun", false);
            }
        }



        if (Input.GetKeyDown(KeyCode.RightShift)&& canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump");
            VoiceJump.Play();

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



    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

 
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0.2f;
        rb2D.velocity = new Vector2(transform.localScale.x  * dashingPower, 0f );
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
