using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunLaser : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(laserTimer(laser));
    }

    private IEnumerator laserTimer(GameObject laserObject)
    {
        laserObject.SetActive(true);
        yield return new WaitForSeconds(Random.Range(2, 5));
        laserObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(laserTimer(laser));
    }
}
