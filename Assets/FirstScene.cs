using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //int indexId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(indexId);
        PlayerPrefs.SetInt("sceneId", 1000);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
