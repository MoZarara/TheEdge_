using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHammer : MonoBehaviour
{

    /*float timer = 0f;
    float speed = 1f;
    int phase = 0;
    */
    

    Vector3 pos01;
    Vector3 pos02;

    Vector3 firstPos;
    public float posNew;

    bool reached;

    float mainNim;
    private void Start()
    {
        
        reached = false;
        mainNim = 0;
        StartCoroutine(timerNum());

        firstPos = transform.eulerAngles;
        pos01 = new Vector3(firstPos.x + posNew, firstPos.y, firstPos.z);
        pos02 = new Vector3(firstPos.x - posNew, firstPos.y, firstPos.z);

        
        //StartCoroutine(FF());
    }

    private IEnumerator timerNum()
    {
        if (!reached)
        {
            //if (mainNim != posNew) {
                mainNim += 1f;
            //}
        }
        else
        {
            //if (mainNim != -posNew){
                mainNim -= 1f;
           // }
        }

        yield return new WaitForSeconds(0.001f);
        StartCoroutine(timerNum());

    }

    /*private IEnumerator FF()
    {
        Vector3 rotCurrent = transform.rotation.eulerAngles;
        Debug.Log("ddd " + rotCurrent);
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(FF());

    }*/

    private void Update()
    {
        //Vector3 rotCurrent = transform.rotation.eulerAngles;

        if (transform.rotation == Quaternion.Euler( pos01))
        {
            reached = true;
        }

        if (transform.rotation == Quaternion.Euler(pos02))
        {
            reached = false;
        }

        if (!reached)
        {
            //Debug.Log("ooo ");
            Vector3 pos001 = new Vector3(firstPos.x + mainNim, firstPos.y, firstPos.z);
            transform.rotation = Quaternion.Euler(pos001);
            
            //Vector3 newDirection = Vector3.RotateTowards(rotCurrent, pos01, .1f * Time.deltaTime, .1f * Time.deltaTime);
            //transform.eulerAngles = newDirection;
            //transform.Rotate(pos01 /** Time.deltaTime*/);
        }
        else
        {
            //Debug.Log("www ");
            Vector3 pos002 = new Vector3(firstPos.x + mainNim, firstPos.y, firstPos.z);
            transform.rotation = Quaternion.Euler(pos002);

            //Vector3 newDirection = Vector3.RotateTowards(rotCurrent, pos02, .1f * Time.deltaTime, .1f * Time.deltaTime);
            //transform.eulerAngles = newDirection;
            //transform.Rotate(pos02 /** Time.deltaTime*/);
        }

        
    }

    /*
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > 1f)
        {
            phase++;
            phase %= 4;            //Keep the phase between 0 to 3.
            timer = 0f;
        }

        switch (phase)
        {
            case 0:
                transform.Rotate(speed * (1 - timer), 0f, 0f);  //Speed, from maximum to zero.
                break;
            case 1:
                transform.Rotate(-speed * timer, 0f, 0f);       //Speed, from zero to maximum.
                break;
            case 2:
                transform.Rotate(-speed * (1 - timer), 0f, 0f); //Speed, from maximum to zero.
                break;
            case 3:
                transform.Rotate(speed * timer, 0f, 0f);        //Speed, from zero to maximum.
                break;
        }
    }*/
}