using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyScript : MonoBehaviour
{

    public GameObject enemyProjectile;
    public Transform projectilePos;

    public float timer;
    public float cooldown; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {

            timer = 0;
            Shoot();

        }
    }

    public void Shoot() 
    
    {
        Instantiate(enemyProjectile, projectilePos.position, Quaternion.identity); 

    }


}
