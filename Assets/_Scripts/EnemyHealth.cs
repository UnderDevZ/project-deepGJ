using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Health;
    public SpriteRenderer EnemySprite;
  
    public GameObject self; 


    // Start is called before the first frame update
    void Start()
    {
        EnemySprite.color = Color.white;


    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            self.SetActive(false); 
        }

    }
    void EnemyHurt()
    
    {

        EnemySprite.color = Color.red; 
        Health = Health - 1;
     



    }

    void UnHurt()
    {
        EnemySprite.color = Color.white;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            Debug.Log("Enemy Hit!!");
            EnemyHurt();
            Invoke("UnHurt", 0.5f);



        }
    }



}
