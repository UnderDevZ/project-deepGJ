using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public GameObject heart1, heart2, heart3;
    public int health;
    public PlayerControllerScript playerController;
    public ParticleSystem particle; 


    // Start is called before the first frame update
    void Start()
    {

        particle.Pause(); 
        playerController.sprite.enabled = true; 
        health = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        


    }

    // Update is called once per frame
    void Update()
    {

       
        if (health < 0)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            Invoke("GameOverScreen", 2f);
            particle.Play(); 



        }


        switch (health)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);

                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);

                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);

                playerController.sprite.enabled = false;
                particle.Play(); 

               Invoke("GameOverScreen",2f); 
                break; 





        }

    }
   
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1"); 



    }
    public void GameOverScreen() 
    {

        SceneManager.LoadScene("GameOver");
        particle.Pause(); 
    }
    public void MainMenuScreen()
    {
        SceneManager.LoadScene("Main Menu"); 

    }
}
