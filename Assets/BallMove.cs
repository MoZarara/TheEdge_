using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    Rigidbody rig;
    public float speed = 4;

    public AudioClip ballAudio;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") {
            AudioSource.PlayClipAtPoint(ballAudio, transform.position);
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, newPos, 3);

            rig.velocity = newPos /**2*/;
        }



    }


}
