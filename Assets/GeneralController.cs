using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralController : MonoBehaviour
{
    [Header("LoadingScreen")]
    public GameObject loadingPanel;
    public Image loadingImage;
    //public Image loadingImageLogo;
    int sceneId;

    public Text NameApp;

    public bool menuScene = false;

    public GameObject AllBtn;
    public GameObject quitPanel;

    //int indexId;
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;

        //indexId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (!menuScene) {

            if (PlayerPrefs.GetInt("sceneId") != null)
            {
                sceneId = PlayerPrefs.GetInt("sceneId");
            }
            else
            {
                Debug.Log("sevcecn nulll");
            }

            StartCoroutine(LoadingScreen(0, false, false, 0));

        }
        else if (menuScene)
        {
            //
            if (PlayerPrefs.GetInt("sceneId") != null)
            {
                sceneId = PlayerPrefs.GetInt("sceneId");
            }
            else
            {
                Debug.Log("sevcecn nulll");
            }
            //Debug.Log(sceneId);
            //PlayerPrefs.SetInt("sceneId", 2);
            if (sceneId != 1000)
                StartCoroutine(LoadingScreen(0, false, false, 0));
            
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
                QuitMethod();

            }
        }
    }


    public void PlayBtn()
    {
        PlayerPrefs.SetInt("sceneId", 1000);
        StartCoroutine(LoadingScreen(0, true, false, 2));
    }





    IEnumerator LoadingScreen(float timerLoading, bool isPlay, bool clickLevel, int sceneNum)
    {
        if (!menuScene)
            NameApp.enabled = false;

        loadingImage.enabled = false;

        loadingPanel.SetActive(true);
        StartCoroutine(LoadingTimer(timerLoading, isPlay, clickLevel));
        
        yield return new WaitForSeconds(4.0f);
        //loadingImageLogo.enabled = false;
        if (!isPlay && !clickLevel)
        {
            //Debug.Log("666");
            loadingPanel.SetActive(false);
            //allRubbishContainer.SetActive(true);
        } else if (clickLevel && !isPlay) {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNum);
        }
        else
        {
            //Debug.Log("4444");
            Time.timeScale = 1;
            /*levels[0].SetActive(false);
            levels[1].SetActive(true);*/

            // go to level controller
            UnityEngine.SceneManagement.SceneManager.LoadScene(12);
            //SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }

    //float timerLoading = 0;
    IEnumerator LoadingTimer(float timerLoading, bool isPlayClicked, bool clickLevel)
    {


        yield return new WaitForSeconds(0.1f);
        if (timerLoading < 1)
        {
            
            timerLoading += 0.04f;
            if (!isPlayClicked && sceneId == 1000 && !clickLevel)
            {
                
                //Debug.Log("8888");
                NameApp.enabled = true;
                //loadingImageLogo.enabled = true;
            } else if (clickLevel && sceneId == 1000 && !isPlayClicked) {
                //Debug.Log("222");
                loadingImage.fillAmount = timerLoading;
                loadingImage.enabled = true;
            }
            else
            {
                //NameApp.enabled = false;
                //Debug.Log("dfff");
                //loadingImageLogo.enabled = false;
                loadingImage.fillAmount = timerLoading;
                loadingImage.enabled = true;
            }


            //loadingPanel.SetActive(true);
            StartCoroutine(LoadingTimer(timerLoading, isPlayClicked, clickLevel));
        }
        else
        {

            if (isPlayClicked)
            {
                //Debug.Log("100");
                Ads.display_interstitialr();
            }

            if (clickLevel)
            {
                //Debug.Log("200");
                Ads.display_interstitialr();
            }

            if (sceneId != 1000)
            {
                //Debug.Log("300");
                Ads.display_interstitialr();
            }

            //from menu id
            /*if (sceneId == 2)
            {
                Debug.Log("300");
                Ads.display_interstitialr();
            }*/
        }

    }

    public void GoToSceneBtn(int sceneNum)
    {
        int indexId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("levelControllerId", indexId);
        StartCoroutine(LoadingScreen(0, false, true, sceneNum));
    }


    public void MoreGamesBtn()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Mo_Zarara");
    }

    public void PrivacypolicyBtn()
    {
        Application.OpenURL("https://test20209.blogspot.com/2021/10/privacy-policy-edge-game.html");
    }


    public void QuitMethod()
    {
        
            AllBtn.SetActive(false);
        
        
        quitPanel.SetActive(true);
    }

    public void NoExitBtn()
    {
        
            AllBtn.SetActive(true);
        

        quitPanel.SetActive(false);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

}
