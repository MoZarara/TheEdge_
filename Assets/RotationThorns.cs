using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationThorns : MonoBehaviour
{
    Vector3 pos01;
    //Vector3 pos02;

    Vector3 firstPos;
    public float posNew;
    public int speed = 4;
    

    float mainNim;
    private void Start()
    {

        
        mainNim = 0;
        StartCoroutine(timerNum());

        firstPos = transform.eulerAngles;
        pos01 = new Vector3(firstPos.x , firstPos.y + posNew, firstPos.z);
        //pos02 = new Vector3(firstPos.x - posNew, firstPos.y, firstPos.z);


        //StartCoroutine(FF());
    }

    private IEnumerator timerNum()
    {
        
            mainNim += speed;
        if (mainNim >= 360) {
            mainNim = 0;
        }

        yield return new WaitForSeconds(0.001f);
        StartCoroutine(timerNum());

    }


    private void Update()
    {

       
            Vector3 pos001 = new Vector3(firstPos.x , firstPos.y + mainNim, firstPos.z);
            transform.rotation = Quaternion.Euler(pos001);

           


    }






}