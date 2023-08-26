using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControllerScript : MonoBehaviour
{
    public bool IsTeen; 
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


    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    public float knockbackTimer;

    private Vector3 originalHitBoxParentScale;
    private Vector3 originalHitBoxLocalPosition;

    public GameObject HitBox;

    public SpriteRenderer HitBoxSprite;
    public HitBoxDamageScript hitBoxDamage; 
    public int AgeHolder; 

    
    

    // Start is called before the first frame update
    void Start()
    {
        IsTeen = false; 

        AgeHolder = 0; 
        originalHitBoxLocalPosition = HitBox.transform.localPosition;
        HitBox.SetActive(false); 
        sprite.enabled = true;
        PlayerAnimator.SetBool("IsAdult", true);
        PlayerAnimator.SetBool("IsRunning", false); 
        sprite.flipX = false; 
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        SparkTrails.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if(AgeHolder > 2)
        {

            AgeHolder = 0; 
        }

       
        if (Input.GetKeyDown(KeyCode.C))
        {
            AgeHolder = AgeHolder + 1; 
            


        }
        switch (AgeHolder)
        {
            case 0: 
                TriggerAdult();
                break;
            case 1:
                TriggerTeen();
                break;
            case 2:
                TriggerKid();
                break;
            default:
                TriggerAdult(); 
                break; 



        }


        if (Input.GetKeyDown(KeyCode.KeypadEnter) || (Input.GetKeyDown(KeyCode.O)))
        {
            Attack(); 

        }

        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            PlayerAnimator.SetBool("IsRunning", true); 
            sprite.flipX = false;
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow))) 
        
        {
            PlayerAnimator.SetBool("IsRunning", true);
            sprite.flipX = true;
            Flip();

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
       
        if (IsTeen == true)
        {

            hitBoxDamage.HitBoxAnim.Play("HitBoxPunch");
        }

        else if (IsTeen == false)
        {
            hitBoxDamage.HitBoxAnim.Play("HitBoxMoney");

        }
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
        if (collision.CompareTag("Hurtbox"))
        {
            GM.health = GM.health - 1;

            Vector2 knockbackDirection = new Vector2(0f, 1f); // Define your knockback direction
            ApplyKnockback(knockbackDirection);
        }

    }
    public void ApplyKnockback(Vector2 direction)
    {
        if (knockbackTimer <= 0)
        {
           rb.velocity = Vector2.zero;  // Stop any current movement
           rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
            knockbackTimer = knockbackDuration;
            Invoke("ResetKnockback", 1f); 
        }
    }

    public void TriggerAdult() 
    {
        //[Insert Code For Going To The Past Here] e.g change of scenery, change of character sprites and animation

        maxSpeed = 5; 
        Debug.Log("Adult Form!");
        PlayerAnimator.SetBool("IsAdult", true);
        PlayerAnimator.SetBool("IsKid", false);
        PlayerAnimator.SetBool("IsTeen", false);
        SparkTrails.SetActive(false);

        IsTeen = false; 

    }

    public void TriggerTeen() 
    {
        IsTeen = true; 
        
       
        PlayerAnimator.SetBool("IsAdult", false);
        PlayerAnimator.SetBool("IsKid", false);
        PlayerAnimator.SetBool("IsTeen", true);


        Debug.Log("Teen Form!");
        SparkTrails.SetActive(false);

    }

    public void TriggerKid() 
    {

        IsTeen = false; 
        currentSpeed = currentSpeed * 2; 
        maxSpeed = 10; 

        PlayerAnimator.SetBool("IsAdult", false);
        PlayerAnimator.SetBool("IsKid", true);
        PlayerAnimator.SetBool("IsTeen", false);

        Debug.Log("Kid Form!");
        SparkTrails.SetActive(true); 
    
    }
    public void ResetKnockback()
    {

        knockbackTimer = 0f; 
    }

    public void Attack()
    {
        HitBox.SetActive(true);
        Invoke("OffHitBox", 0.5f); 

    }
    public void OffHitBox() 
    {
        HitBox.SetActive(false); 
    }

    private void Flip()
    {
        // Flip the sprite ...

        // Adjust the local position of the HitBox
        if (sprite.flipX)
        {
            HitBoxSprite.flipX = true;
            HitBox.transform.localPosition = new Vector3(-originalHitBoxLocalPosition.x * originalHitBoxParentScale.x,
                                                        originalHitBoxLocalPosition.y * originalHitBoxParentScale.y,
                                                        originalHitBoxLocalPosition.z * originalHitBoxParentScale.z);
        }
        else
        {
            HitBoxSprite.flipX = false; 
            HitBox.transform.localPosition = originalHitBoxLocalPosition;
        }
    }
}
