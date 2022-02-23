using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBall : MonoBehaviour
{
    public GameObject ballDie;
    public float timeFallBall = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeFall());
    }

    IEnumerator timeFall() {
        GameObject ball = Instantiate(ballDie, transform.position, Quaternion.identity);
        Destroy(ball, 17.0f);
        yield return new WaitForSeconds(timeFallBall);
        StartCoroutine(timeFall());
    }
}
