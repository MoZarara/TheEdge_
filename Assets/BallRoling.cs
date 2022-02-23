using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoling : MonoBehaviour
{

    bool isGround;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            audioSource.mute = true;
        }
    }

}
