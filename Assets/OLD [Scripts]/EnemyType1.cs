using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    public float moveSpeed;
    public float detectRange;
    public float attackRange;

    private Transform player;
    private Rigidbody2D enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);
        }
        else
        {
            enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within the detect range
        if (distanceToPlayer <= detectRange)
        {
            // Move towards the player
            Vector2 moveDirection = (player.position - transform.position).normalized;
            enemyRB.velocity = new Vector2(moveDirection.x * moveSpeed, enemyRB.velocity.y);

            // Check if the player is within attack range
            if (distanceToPlayer <= attackRange)
            {
                // Implement attack logic here
                Debug.Log("Enemy is attacking!");
            }
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-Mathf.Sign(enemyRB.velocity.x), transform.localScale.y);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw Detect Range Gizmo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        // Draw Attack Range Gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
