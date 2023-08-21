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

    

    // Start is called before the first frame update
    void Start()
    {
       
        isGrounded = false; 
        rb = GetComponent<Rigidbody2D>(); 

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
           


        }


        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
        {

            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y);
           

        }


          if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
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
}
