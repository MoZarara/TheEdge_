using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Laser : MonoBehaviour
{
    private LineRenderer lR;
    public Player_Movement player;
    public Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        lR = GetComponent<LineRenderer>();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        lR.SetPosition(0, transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {

            if (hit.collider)
            {
                lR.SetPosition(1, hit.point);
            }

            
            if (hit.collider.gameObject.tag == "Player")
            {
                player.ExplosionPlayer();
                StartCoroutine(controller.FinallyGame("Game Over", 0.5f));
            }



        }
        else lR.SetPosition(1, transform.forward * 100);



        
    }




}
