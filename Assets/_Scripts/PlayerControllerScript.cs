using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControllerScript : MonoBehaviour
{
    public int moveSpeed;
    public int jumpForce;
    public Rigidbody2D rb;
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    public float accelerationRate = 2.0f; // How fast speed increases
    public float decelerationRate = 2.5f; // How fast speed decreases
    public float maxSpeed = 10.0f;
    public bool isMaxSpeed = false;


    public GameObject SparkTrails; 
    public float currentSpeed;
    public Animator PlayerAnimator;
  
    public SpriteRenderer sprite;
    public GameManager GM; 

    // Start is called before the first frame update
    void Start()
    {
        sprite.enabled = true;
        PlayerAnimator.SetBool("IsAdult", true);
        PlayerAnimator.SetBool("IsRunning", false); 
        sprite.flipX = false; 
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            PlayerAnimator.SetBool("IsRunning", true); 
            sprite.flipX = false; 
        }

        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow))) 
        
        {
            PlayerAnimator.SetBool("IsRunning", true);
            sprite.flipX = true;              
        
        }

        if (currentSpeed != 0)
        {
            PlayerAnimator.SetBool("IsRunning", true);

        }
        else 
        {
            PlayerAnimator.SetBool("IsRunning", false);

        }


        // Check for movement input and set velocity accordingly
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate desired speed based on input
        float desiredSpeed = horizontalInput * moveSpeed;

        // Smoothly adjust speed
        if (desiredSpeed > currentSpeed)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, desiredSpeed, Time.deltaTime * accelerationRate);
        }
        else if (desiredSpeed < currentSpeed)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, desiredSpeed, Time.deltaTime * decelerationRate);
        }

        // Clamp speed to maxSpeed
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Apply the final velocity
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Check if player is at max speed
        isMaxSpeed = Mathf.Approximately(currentSpeed, maxSpeed);
        
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }



private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AdultTrigger") && (isMaxSpeed == true)) 
        {
            TriggerAdult();
        }

        if (collision.CompareTag("TeenTrigger") && (isMaxSpeed == true)) 
        {
            TriggerTeen();             
        }

        if (collision.CompareTag("KidTrigger") && (isMaxSpeed == true))
        {
            TriggerKid(); 
        }

        if (collision.CompareTag("EnemyProjectile"))
        {

            GM.health = GM.health - 1;
        }


    }

    public void TriggerAdult() 
    {
        //[Insert Code For Going To The Past Here] e.g change of scenery, change of character sprites and animation

        Debug.Log("Adult Form!");
        PlayerAnimator.SetBool("IsAdult", true);
        PlayerAnimator.SetBool("IsKid", false);
        PlayerAnimator.SetBool("IsTeen", false);

    }

    public void TriggerTeen() 
    {

        PlayerAnimator.SetBool("IsAdult", false);
        PlayerAnimator.SetBool("IsKid", false);
        PlayerAnimator.SetBool("IsTeen", true);


        Debug.Log("Teen Form!");
    }

    public void TriggerKid() 
    {

        PlayerAnimator.SetBool("IsAdult", false);
        PlayerAnimator.SetBool("IsKid", true);
        PlayerAnimator.SetBool("IsTeen", false);

        Debug.Log("Kid Form!"); 
    
    
    }
}
