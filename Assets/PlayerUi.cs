using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public Slider healthSlider;
    //float countHealth;
    [HideInInspector]
    public float currentHealth;

    public Image background;
    public Color colorDefault = Color.white;
    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    public Color color3 = Color.gray;
    public Color color4 = Color.green;

    /////timer
    private float currentTime = 0f;
    public float startTime = 5f;


    public Controller controller;

    public GameObject winParticleSystem;
    public GameObject pointParty;


    //public int levelId;


    public AudioClip giftAudio, winAudio;
    //public GameObject giftEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        currentHealth = 1;
        healthSlider.value = currentHealth;
        background.color = colorDefault;

        //PlayerPrefs.SetInt("sceneId", levelId);
    }

    // Update is called once per frame
    void Update()
    {
        


        if (controller.currentTime > 0 && currentHealth > 0)
        {
            currentTime -= 1 * Time.deltaTime;

            if (currentTime <= 0) {

                
                currentHealth -= 0.25f;
                    //healthSlider.value = currentHealth;
                
                
                
                currentTime = startTime;
            }

            healthSlider.value = currentHealth;
        }



        if (currentHealth == 1)
        {
            background.color = colorDefault;
        }


        if (currentHealth < 1 && currentHealth >= 0.75f) {
            background.color = color1;
        }

        if (currentHealth < 0.75f && currentHealth >= 0.50f)
        {
            background.color = color2;
        }

        if (currentHealth < 0.50f && currentHealth >= 0.25f)
        {
            background.color = color3;
        }

        if (currentHealth < 0.25f && currentHealth >= 0.0f)
        {
            background.color = color4;
        }

    }


    



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water" || other.gameObject.tag == "Apple")
        {
            
            Destroy(other.gameObject);
            /*if (currentHealth < 1) {               
                currentHealth = 1.0f;
                healthSlider.value = currentHealth;               
                currentTime += startTime;
            }*/

            FoodGift();
            //GameObject party = Instantiate(giftEffect, other.transform.position, Quaternion.identity);
            //Destroy(party, 1.5f);
            //AudioSource.PlayClipAtPoint(giftAudio, transform.position);
        }


        if (other.gameObject.tag == "IncreaseTime")
        {

            Destroy(other.gameObject);
            TimeGift();
            //controller.currentTime += 20.0f;
            AudioSource.PlayClipAtPoint(giftAudio, transform.position);
        }

        if (other.gameObject.tag == "Win")
        {
            PlayerPrefs.SetInt("levelID", controller.levelId);
            
            AudioSource.PlayClipAtPoint(winAudio, transform.position);

            GameObject party = Instantiate(winParticleSystem, pointParty.transform.position, Quaternion.identity);
            Destroy(party, 8.0f);
            StartCoroutine(controller.FinallyGame("Win", 0.2f));

            
        }

        if (other.gameObject.tag == "PointReturnPlayer2")
        {
            //Debug.Log("ppp111");
            controller.pointGeneralRetunnPlayer.transform.position = controller.pointsReturnAfterDied[1].position;
        }

        if (other.gameObject.tag == "PointReturnPlayer3")
        {
            controller.pointGeneralRetunnPlayer.transform.position = controller.pointsReturnAfterDied[2].position;
        }
    }

    public void FoodGift() {
        if (currentHealth < 1)
        {

            //currentHealth += 0.25f;
            currentHealth = 1.0f;
            healthSlider.value = currentHealth;


            currentTime += startTime;
        }
        AudioSource.PlayClipAtPoint(giftAudio, transform.position);
    }

    public void TimeGift()
    {
        controller.currentTime += 30.0f;
        AudioSource.PlayClipAtPoint(giftAudio, transform.position);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thorns")
        {
            player.ExplosionPlayer();
            StartCoroutine(controller.FinallyGame("Game Over", 0.4f));                   
        }

    }*/



}
