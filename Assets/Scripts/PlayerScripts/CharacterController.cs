using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour

{
    public float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    private bool Walk = true;

    public bool isAttacked;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private TrailRenderer tr;


    [SerializeField] private LayerMask groundCheckLayer;
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public bool grounded;

    // healtbar 
    public Image filler;
    //laskee nollasta kahteen ja aloittaa alusta
    public float counter;
    //2sec, m‰‰ritt‰‰ sen kuinka nopeasti healthbar liikkuu uuteen arvoon
    public float maxCounter;



    public AudioSource VoiceJump;
    public AudioSource takeHitAudio;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        GameManager.manager.historyHealth = GameManager.manager.health;
        GameManager.manager.historyPreviousHealth = GameManager.manager.previousHealth;
        GameManager.manager.historyMaxHealth = GameManager.manager.maxHealth;
    }

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



        if (Input.GetKeyDown(KeyCode.RightShift) && canDash)
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
        // TƒMƒ luo counterille laskurin, joka kasvaa maxCounteriin ja aloittaa uudestaan 0:sta. 
        if (counter > maxCounter)
        {
            
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;

        }
        else
        {
            counter += Time.deltaTime;
        }

        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, 
            GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);

        if (gameObject.transform.position.y < -20)
        {
            Die();
        }
    }



    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Osuma");
            TakeDamage(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AddHealth"))
        {
            Destroy(collision.gameObject);
            Heal(10);
        }

        if (collision.CompareTag("AddMaxHealth"))
        {
            Destroy(collision.gameObject);
            AddMaxHealth(50);
        }

        if (collision.CompareTag("LevelEnd")) 
        {
            SceneManager.LoadScene("Map");
        }
    }

    private void AddMaxHealth(float amt)
    {
        GameManager.manager.maxHealth += amt;
    }

    private void Heal(float amt)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health += amt;
        if (GameManager.manager.health > GameManager.manager.maxHealth)
        {
            GameManager.manager.health = GameManager.manager.maxHealth;
        }
    }

    private void TakeDamage(float dmg)
    {
        isAttacked = true;
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health -= dmg;

        if (GameManager.manager.health < 0)
        {

            Die();

        }
    }

    public void Die()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel;
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.previousHealth = GameManager.manager.historyPreviousHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        SceneManager.LoadScene("Map");

    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0.2f;
        rb2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    

}
