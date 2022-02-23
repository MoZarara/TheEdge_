using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlatform : MonoBehaviour
{
    //public float min = 3f;
    //public float max = 3f;

    //public Text logTxt;
    //public Transform pos1;
    //public Transform pos2;

    public float posNew;
    //public float pos02;
    public float speed = 2f;

    public GameObject target;
    Transform firstPos;

    Vector3 pos001;
    Vector3 pos002;
    bool reached;

    public bool MoveX = false;
    public bool MoveY = false;
    public bool MoveZ = false;
    // Use this for initialization
    void Start()
    {
        reached = false;
        // min = transform.position.x;
        //max = transform.position.x + 3;
        firstPos = transform;


        if (MoveX && !MoveY & !MoveZ)
        {

            pos001 = new Vector3(firstPos.position.x + posNew, firstPos.position.y, firstPos.position.z);
            pos002 = new Vector3(firstPos.position.x - posNew, firstPos.position.y, firstPos.position.z);
        }


        if (MoveY && !MoveX & !MoveZ)
        {

            pos001 = new Vector3(firstPos.position.x, firstPos.position.y + posNew, firstPos.position.z);
            pos002 = new Vector3(firstPos.position.x, firstPos.position.y - posNew, firstPos.position.z);
        }

        if (MoveZ && !MoveY && !MoveX)
        {
            pos001 = new Vector3(firstPos.position.x, firstPos.position.y, firstPos.position.z + posNew);
            pos002 = new Vector3(firstPos.position.x, firstPos.position.y, firstPos.position.z - posNew);
        }

        if (MoveX && MoveZ)
        {
            pos001 = new Vector3(firstPos.position.x + posNew, firstPos.position.y, firstPos.position.z + posNew);
            pos002 = new Vector3(firstPos.position.x - posNew, firstPos.position.y, firstPos.position.z - posNew);
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == pos001) {
            reached = true;
        }

        if (transform.position == pos002)
        {
            reached = false;
        }

        if (!reached)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos001, speed * Time.deltaTime);
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, pos002, speed * Time.deltaTime);
        }
        

        //transform.position = new Vector3(Mathf.PingPong(Time.time * 1, max - min) + min, transform.position.y, transform.position.z);

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            //Debug.Log("dddd ");
            //logTxt.text = "nooow ";
            //other.transform.parent = transform;
            target.transform.parent = transform;
            //target.transform.localScale = new Vector3(1, 1, 1);
        }
    }

  

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            //other.transform.parent = null;
            target.transform.parent = null;
        }
    }



}
