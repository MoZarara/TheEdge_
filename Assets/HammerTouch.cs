using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerTouch : MonoBehaviour
{

    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            target.transform.parent = transform;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target.transform.parent = null;
        }
    }
}
