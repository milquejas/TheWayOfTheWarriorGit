//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
////using UnityEngine.SceneManagement;
//using TMPro; // TÄMÄ

//public class FrozenViking : MonoBehaviour
//{
//    public float moveSpeed;
//    public float jumpForce;

//    public Animator animator;
//    public Rigidbody2D rb2D;

//    public float woodAmount; // TÄMÄ
//    public TextMeshProUGUI woodAmountText; // TÄMÄ
//    public GameObject bonfire;

//    public Transform groundCheckPosition;
//    public float groundCheckRadius;
//    public LayerMask groundCheckLayer;
//    public bool grounded;

//    public bool hasAxe;

//    public Image filler;
//    public float counter;
//    public float maxCounter;



//    // Start is called before the first frame update
//    void Start()
//    {
//        // T�m� on sama asia kuin raahaisi inspectorissa
//        animator = GetComponent<Animator>();
//        rb2D = GetComponent<Rigidbody2D>();
//        //GameManager.manager.historyHealth = GameManager.manager.health;
//        //GameManager.manager.historyPreviousHealth = GameManager.manager.previousHealth;
//        //GameManager.manager.historyMaxHealth = GameManager.manager.maxHealth;
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        // Ground testi, eli ollaanko kosketuksissa maahan vai ei.
//        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
//        {
//            grounded = true;
//        }
//        else
//        {
//            grounded = false;

//        }


//        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

//        if (Input.GetAxisRaw("Horizontal") != 0)
//        {
//            // Meill� on a tai d pohjassa
//            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
//            animator.SetBool("Walk", true);
//        }
//        else
//        {
//            // T�m� ajetaan kun ollaan pys�hdyksiss�
//            animator.SetBool("Walk", false);
//        }

//        // Hyppy
//        if (Input.GetButtonDown("Jump") && grounded == true)
//        {
//            rb2D.velocity = new Vector2(0, jumpForce);
//            animator.SetTrigger("Jump");
//        }

//        // t�m� luo counterille laskurin, joka kasvaa maxCounteriin ja aloittaa uudestaan 0:sta. 
//        if (counter > maxCounter)
//        {
//            GameManager.manager.previousHealth = GameManager.manager.health;
//            counter = 0;

//        }
//        else
//        {
//            counter += Time.deltaTime;
//        }


//        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);


//        if (gameObject.transform.position.y < -10)
//        {
//            Die();
//        }
//        //nuotion pystytys
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            if (woodAmount >= 3)
//            {
//                woodAmount -= 3;
//                woodAmountText.text = woodAmount.ToString();
//                Instantiate(bonfire, transform.position + new Vector3(3.1f * transform.localScale.x, -0.5f, 0), Quaternion.identity);
//            }

//        }



//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Trap"))
//        {
//            // Ollaan osuttu ansaan -> V�hennet��n healthia.
//            TakeDamage(20);
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("AddHealth"))
//        {
//            // Pelaaja on koskettanut syd�nt�. Tuhotaan syd�n ja lis�t��n 30 healthia. 
//            Destroy(collision.gameObject); // Tuhotaan syd�n
//            Heal(30);
//        }

//        if (collision.CompareTag("AddMaxHealth"))
//        {
//            Destroy(collision.gameObject);
//            AddMaxHealth(40);
//        }
//        //ilman axee ei pääse leveliä läpi
//        if (collision.CompareTag("LevelEnd"))
//        {

//            if (hasAxe)
//            {
//                //GameManager.manager.previousLevel = GameManager.manager.currentLevel;
//                //SceneManager.LoadScene("Map");
//            }

//        }

//        if (collision.CompareTag("Wood"))
//        {
//            Destroy(collision.gameObject);
//            woodAmount++;
//            woodAmountText.text = woodAmount.ToString();
//        }
//        //tässä luodaan collectable axe ja ilman axee ei pääse läpi
//        //axen muuttujalle annettu boolean. 
//        if (collision.CompareTag("Axe"))
//        {
//            Destroy(collision.gameObject);
//            hasAxe = true;
//        }
//    }

//    private void OnTriggerStay2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Bonfire"))
//        {
//            Heal(Time.deltaTime * 10);
//        }
//    }


//    void AddMaxHealth(float addMaxHealthAmount)
//    {
//        GameManager.manager.maxHealth += addMaxHealthAmount;
//    }

//    void Heal(float healAmount)
//    {
//        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
//        counter = 0;
//        GameManager.manager.health += healAmount;

//        // Vaihtoehto 1
//        /*
//        if(health > maxHealth)
//        {
//            health = maxHealth;
//        }
//        */
//        // Vaihtoehto 2
//        GameManager.manager.health = Mathf.Clamp(GameManager.manager.health, 0, GameManager.manager.maxHealth);
//    }

//    void TakeDamage(float dmg)
//    {

//        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
//        counter = 0;
//        GameManager.manager.health -= dmg; // T�m� v�hent�� dmg:n verran health arvosta. health = health - dmg;

//        if (GameManager.manager.health < 0)
//        {

//            Die();

//        }

//    }

//    public void Die()
//    {
//        //GameManager.manager.currentLevel = GameManager.manager.previousLevel;
//        //GameManager.manager.health = GameManager.manager.historyHealth;
//        //GameManager.manager.previousHealth = GameManager.manager.historyPreviousHealth;
//        //GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
//        //SceneManager.LoadScene("Map");

//    }


//}

