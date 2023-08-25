using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{
    public GameObject enemyProjectile;
    public Transform projectilePos;

    public float timer;
    public float cooldown;

    public float detectionRange = 10f;

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;

    public Animator GunAnim; 

    private void Start()
    {
        GunAnim.SetBool("IsShooting", false);
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRange)
            {
                GunAnim.SetBool("IsShooting", true); 
                if (player.transform.position.x > transform.position.x && !isFacingRight)
                {
                    Flip();
                }
                else if (player.transform.position.x < transform.position.x && isFacingRight)
                {
                    Flip();
                }

                timer = 0;
                Shoot();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void Flip()
    {

        isFacingRight = !isFacingRight;

        // Flip the enemy's sprite horizontally
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Adjust the local position of the projectilePos
        Vector3 projectilePosLocalPosition = projectilePos.localPosition;
        projectilePosLocalPosition.x *= -1;
        projectilePos.localPosition = projectilePosLocalPosition;

        // Adjust the local position based on the enemy's scale
        if (!isFacingRight)
        {
            projectilePos.localPosition = new Vector3(-projectilePos.localPosition.x, projectilePos.localPosition.y, projectilePos.localPosition.z);
        }
    }

    private void Shoot()
    {
        Instantiate(enemyProjectile, projectilePos.position, Quaternion.identity);
    }
}
