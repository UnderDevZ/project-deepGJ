using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    public float moveSpeed;
    public float detectRange;
    public float attackRange;
    public float attackCooldown = 3f; // Cooldown time in seconds

    public Transform flipLine;  // The line the player needs to cross to trigger flip

    private Transform player;
    private Rigidbody2D enemyRB;
    private Animator BigAnim;
    private bool canAttack = true;
    public float lastAttackTime ;
    public GameObject SlamHurtBox; 

    // Start is called before the first frame update
    void Start()
    {
        SlamHurtBox.SetActive(false); 
        BigAnim = GetComponent<Animator>();
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

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > detectRange)
        {
            BigAnim.Play("Big_Walk");
        }
        else if (distanceToPlayer <= detectRange)
        {
            Vector2 moveDirection = (player.position - transform.position).normalized;
            enemyRB.velocity = new Vector2(moveDirection.x * moveSpeed, enemyRB.velocity.y);

            if (distanceToPlayer <= attackRange && canAttack)
            {
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    SlamHurtBox.SetActive(true); 
                    lastAttackTime = Time.time;
                    StartCoroutine(AttackCooldown());
                    Debug.Log("Enemy is attacking!");
                    BigAnim.Play("Big_Slam");
                }
                else
                {
                    SlamHurtBox.SetActive(false);
                    BigAnim.Play("Big_Walk"); 
                }
            }
        }

        float distanceToLine = Mathf.Abs(transform.position.x - flipLine.position.x);

        if (player.position.x > flipLine.position.x && IsFacingRight())
        {
            Flip();
        }
        else if (player.position.x < flipLine.position.x && !IsFacingRight())
        {
            Flip();
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((player.position.x > flipLine.position.x && !IsFacingRight()) ||
            (player.position.x < flipLine.position.x && IsFacingRight()))
        {
            Flip();
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(flipLine.position.x, -1000f, 0f), new Vector3(flipLine.position.x, 1000f, 0f));
    }
}
