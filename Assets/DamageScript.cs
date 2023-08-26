using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite.color = Color.white;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hurtbox") || collision.CompareTag("EnemyProjectile"))
        {

            sprite.color = Color.red;
            Invoke("Unhurt", 0.5f);

        }

    }
    public void Unhurt()
    {
        sprite.color = Color.white;

    }
}
