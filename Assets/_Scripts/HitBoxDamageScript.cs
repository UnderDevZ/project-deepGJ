using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDamageScript : MonoBehaviour
{
    public Animator HitBoxAnim; 

    public int damageAmount; 
    // Start is called before the first frame update
    void Start()
    {
        HitBoxAnim = GetComponent<Animator>(); 
        damageAmount = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
