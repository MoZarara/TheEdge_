using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiedPlayer : MonoBehaviour
{
    private Controller controller;


    private void Start()
    {
        controller = FindObjectOfType<Controller>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("uuuu");
            collision.gameObject.GetComponent<Player_Movement>().ExplosionPlayer();
            StartCoroutine(controller.FinallyGame("Game Over", 0.4f));
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("oooo");
            other.gameObject.GetComponent<Player_Movement>().ExplosionPlayer();
            StartCoroutine(controller.FinallyGame("Game Over", 0.4f));
        }
    }

}
