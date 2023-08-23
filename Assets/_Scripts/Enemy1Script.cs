using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectRange = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 3f;

    private Transform player;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AttackCooldown());
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectRange)
        {
            // Move towards the player
            Vector2 direction = player.position - transform.position;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

            if (!isAttacking && Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                // Within attack range, start attacking
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        // Call attack logic here
        Debug.Log("Enemy is attacking!");

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    private IEnumerator AttackCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }
    }
}
