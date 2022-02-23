using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControler : MonoBehaviour
{

    [Range(0, 9)]
    public int levelReached;
    public Button[] levelBtn;
    public Image[] levelImageBlockBtn;


    //Controller controller;
    // bool startGame;

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //levelReached = PlayerPrefs.GetInt("levelID");
        levelReached = PlayerPrefs.GetInt("levelSortWin");

        //Debug.Log("dd " + levelReached);
        //Debug.Log("gggg " + levelBtn.Length);
        //controller = GameObject.FindGameObjectWithTag("controller_id").GetComponent<Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        if (levelReached < levelBtn.Length)
        {
            for (int i = 0; i < levelReached + 1; i++)
            {
                //Debug.Log("kkkk "  + i);
                levelImageBlockBtn[i].enabled = false;
                levelBtn[i].interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < levelReached; i++)
            {
                //Debug.Log("uuu " + i);
                levelImageBlockBtn[i].enabled = false;
                levelBtn[i].interactable = true;
            }
        }
    }



    private void LateUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                // Quit the application
                backToHome();

            }
        }
    }

    public void backToHome() {
        int indexId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("sceneId", indexId);
        //PlayerPrefs.SetInt("levelControllerId", indexId);
        //PlayerPrefs.SetInt("sceneId", 2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}

