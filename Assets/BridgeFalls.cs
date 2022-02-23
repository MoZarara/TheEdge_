using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeFalls : MonoBehaviour
{

    Vector3 firstPos;
    Vector3 firstRotation;
    Rigidbody rig;
    Animator anim;

    public float timeFall = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.position;
        firstRotation = transform.eulerAngles;

        rig = GetComponent<Rigidbody>();
        rig.isKinematic = true;

        anim = GetComponent<Animator>();
        anim.enabled = false;
        
    }

    // Update is called once per frame
    /*void Update()
    {
        //transform.position = firstPos;
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(timerFall());
        }
    }

    private IEnumerator timerFall()
    {
        anim.enabled = true;

        yield return new WaitForSeconds(timeFall);
        anim.enabled = false;
        rig.isKinematic = false;

        yield return new WaitForSeconds(2.5f);
        
        transform.position = firstPos;
        transform.eulerAngles = firstRotation;
        rig.isKinematic = true;
    }
}
