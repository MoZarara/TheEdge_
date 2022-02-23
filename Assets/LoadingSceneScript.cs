using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{

    [Header("LoadingScreen")]
    public GameObject loadingPanel;
    public Image loadingImage;
    int sceneId;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        sceneId = PlayerPrefs.GetInt("sceneId");
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingScreen(0, sceneId));
    }

    IEnumerator LoadingScreen(float timerLoading, int sceneNum)
    {
        

        loadingImage.enabled = false;

        
        StartCoroutine(LoadingTimer(timerLoading));

        yield return new WaitForSeconds(4.0f);
        
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNum);
        
    }

    //float timerLoading = 0;
    IEnumerator LoadingTimer(float timerLoading)
    {


        yield return new WaitForSeconds(0.1f);
        if (timerLoading < 1)
        {

            timerLoading += 0.04f;
           
                loadingImage.fillAmount = timerLoading;
                loadingImage.enabled = true;
          


            StartCoroutine(LoadingTimer(timerLoading));
        }
        else
        {

                Ads.display_interstitialr();
           
        }

    }
}
