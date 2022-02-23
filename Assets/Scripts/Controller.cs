using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    public Text timer_tex, timer_gift_tex, timer_gameOver_txt;

    [HideInInspector]
    public bool playerStop;

    [HideInInspector]
    public float currentTime = 0f;
    public float startTime = 40f;
    private bool stopTimer;


    [Header("Buttons")]
    public GameObject PausePanel;
    public GameObject pauseBtn;



    //private DateTime todayTimer;

    [Header("")]
    public GameObject player;
    public Transform[] pointsReturnAfterDied;
    [HideInInspector]
    public Transform pointGeneralRetunnPlayer;

    [Header("")]
    public Transform[] listPointGifts;
    public GameObject[] listGiftsPrefab;
    //bool playerInNewPointNow = false;

    [Header("")]
    public GameObject GiftBtn;
    private float currentTimeGift = 0f;
    private float startTimeGift = 150f;
    private bool stopTimerGift;
    public Image giftImage;
    public Sprite chicken, clock;
    public GameObject GiftPanel;

    [Header("")]
    public GameObject GameOverPanel;
    public GameObject winPanel;


    [Header("ControllerBtn")]
    public GameObject[] controllerBtnUi;

    [Header("")]
    public GameObject returnAfterDiedBtn;

    [HideInInspector]
    public int levelId;

    public bool lastLevel = false;
    public GameObject nextLevelBtn;

    public GameObject handleJoystick;

    [HideInInspector]
    public bool returnAfterDied = false;


    [Header("Message Panel")]
    public GameObject messagePanel;

    public int levelSortWin;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        levelId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        PlayerPrefs.SetInt("sceneId", levelId);
        //PlayerPrefs.SetInt("levelSort", levelSort);

        currentTime = startTime;
        stopTimer = false;

        currentTimeGift = startTimeGift;
        stopTimerGift = false;

        playerStop = false;

        PausePanel.SetActive(false);
        GiftPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        messagePanel.SetActive(false);

        pointGeneralRetunnPlayer = pointsReturnAfterDied[0];

        for (int i = 0; i < listPointGifts.Length; i++)
        {
            //Debug.Log("fff");
            Instantiate(listGiftsPrefab[UnityEngine.Random.Range(0, listGiftsPrefab.Length)], listPointGifts[i].position, Quaternion.identity);

        }

        //Instantiate(listGiftsPrefab[UnityEngine.Random.Range(0, listGiftsPrefab.Length)], listPointGifts[UnityEngine.Random.Range(0, listPointGifts.Length)].position, Quaternion.identity);

        //StartCoroutine(RealTime());


        for (int i = 0; i < controllerBtnUi.Length; i++) {
            controllerBtnUi[i].SetActive(true);
        }

        if (lastLevel)
        {
            nextLevelBtn.GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStop)
        {


            if (!stopTimer)
            {
                currentTime -= 1 * Time.deltaTime;

                if (currentTime >= 0)
                {
                    int minutes = (int)(currentTime / 60f);
                    int seconds = (int)(currentTime % 60f);
                    timer_tex.text = minutes.ToString("00") + ":" + seconds.ToString("00");

                    //StartCoroutine(AfterTimeSpeed());


                }
                else
                {

                    //timer_tex.text = "0";
                    //GameOverMethod();
                    StartCoroutine(FinallyGame("Game Over", 0f));
                }
            }
        }


        //StartCoroutine(RealTime());

        if (!stopTimerGift)
        {
            currentTimeGift -= 1 * Time.deltaTime;

            if (currentTimeGift >= 0)
            {
                int minutes = (int)(currentTimeGift / 60f);
                int seconds = (int)(currentTimeGift % 60f);
                timer_gift_tex.text = minutes.ToString("00") + ":" + seconds.ToString("00");
                timer_gameOver_txt.text = minutes.ToString("00") + ":" + seconds.ToString("00");

                GiftBtn.GetComponent<Button>().interactable = false;
                GiftBtn.GetComponent<Animator>().enabled = false;

                returnAfterDiedBtn.GetComponent<Button>().interactable = false;

            }
            else
            {
                GiftBtn.GetComponent<Button>().interactable = true;
                GiftBtn.GetComponent<Animator>().enabled = true;

                returnAfterDiedBtn.GetComponent<Button>().interactable = true;
                //StartCoroutine(FinallyGame("Game Over", 0f));
            }
        }



    }

    /*private void LateUpdate()
    {
        //Debug.Log(todayTimer.ToString("dd:MM:yy ss:mm:hh"));
        
    }*/



    public IEnumerator FinallyGame(string txt, float timer) {

        if (txt.ToString() == "Win")
        {
            PlayerPrefs.SetInt("levelID", levelId);
            PlayerPrefs.SetInt("levelSortWin", levelSortWin);
        }


        playerStop = true;

        for (int i = 0; i < controllerBtnUi.Length; i++)
        {
            controllerBtnUi[i].SetActive(false);
        }


        player.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(timer);
        

        if (txt.ToString() == "Game Over") {
            GameOverPanel.SetActive(true);           
        } else if (txt.ToString() == "Win") {
            winPanel.SetActive(true);
        }

        //gameOverText.gameObject.SetActive(true);
        //gameOverText.text = txt;

        //playerInNewPointNow = false;
        //OnPause(false);

    }

    public void OnPause(bool isCancel) {
        if (!isCancel)
        {
            pauseBtn.SetActive(false);
            PausePanel.SetActive(true);
            playerStop = true;
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
            playerStop = false;
            pauseBtn.SetActive(true);
            PausePanel.SetActive(false);

        }
         
    }

    public void Restart() {
        PlayerPrefs.SetInt("sceneId", levelId);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(levelId);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(levelId);
        UnityEngine.SceneManagement.SceneManager.LoadScene(13);
    }

    public void NextLevel()
    {
        if (!lastLevel)
        {
            
            PlayerPrefs.SetInt("sceneId", levelId + 1);
            //UnityEngine.SceneManagement.SceneManager.LoadScene(levelId);
            UnityEngine.SceneManagement.SceneManager.LoadScene(13);

        }
        
        
    }

    /*IEnumerator RealTime() {

        while (true) {
            var today = System.DateTime.Now;
            Debug.Log(today.ToString("dd:MM:yy ss:mm:hh"));

            yield return new WaitForSeconds(0.2f);
        }

        
    
    }*/



   /* IEnumerator RealTime()
    {

        todayTimer = System.DateTime.Now;
            //Debug.Log(todayTimer.ToString("dd:MM:yy ss:mm:hh"));

            yield return new WaitForSeconds(1.0f);

        StartCoroutine(RealTime());
    }*/

    public void ADS_ReturnPlayerAfterDied()
    {
        //Debug.Log("mymymyq " + "ReturnPlayerAfterDied");
        GetComponent<Ads>().display_Rewarded_video("AfterDied");
        //ReturnPlayerAfterDied();
    }

    public void ADS_Gift()
    {
        //Debug.Log("mymymyq " + "Gift");
        GetComponent<Ads>().display_Rewarded_video("Gift");

    }

    public void ReturnPlayerAfterDied()
    {
        //Debug.Log("mymymyq " + "1111");
        player.transform.parent = null;
        handleJoystick.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

        returnAfterDied = true;

        player.GetComponent<MyThirdPersonInput>().button.Pressed = false;
        player.GetComponent<Player_Movement>().m_Jump = false;

        //player.GetComponent<Player_Movement>().jumpNow = true;
        //player.GetComponent<Player_Movement>().m_Jump = false;
        //player.GetComponent<Player_Movement>().returnAfterDied = true;

        player.GetComponent<MyThirdPersonInput>().enabled = false;
        player.GetComponent<PlayerUi>().gameObject.SetActive(false);


        /* for (int i = pointsReturnAfterDied.Length - 1; i >= 0; i--) {
             if (pointsReturnAfterDied[i].position.z < player.GetComponent<Player_Movement>().gameObject.transform.position.z && !playerInNewPointNow) {

                 player.GetComponent<Player_Movement>().gameObject.transform.position = pointsReturnAfterDied[i].position;
                 playerInNewPointNow = true;
             }
         }*/


        player.GetComponent<Player_Movement>().transform.position = pointGeneralRetunnPlayer.position;

        
        //Debug.Log("mymymyq " + "2222");

        player.GetComponent<PlayerUi>().gameObject.SetActive(true);
        player.GetComponent<MyThirdPersonInput>().enabled = true;
        //Time.timeScale = 1;
        playerStop = false;
        player.GetComponent<Animator>().enabled = true;
        /*gameOverText.text = "";
        gameOverText.gameObject.SetActive(false);*/


        currentTimeGift = startTimeGift;
        stopTimerGift = false;

        //Debug.Log("jjjjj ");
        //player.GetComponent<Player_Movement>().playerDied = false;

        //



        if (currentTime < 150.0f)
        {
            currentTime = 165.0f;

        }
        player.GetComponent<PlayerUi>().currentHealth = 1.0f;
        //OnPause(true);


        for (int i = 0; i < controllerBtnUi.Length; i++)
        {
            controllerBtnUi[i].SetActive(true);
        }

        GameOverPanel.SetActive(false);
        //Debug.Log("mymymyq " + "3333");
    }


    public void Gift() {

        for (int i = 0; i < controllerBtnUi.Length; i++)
        {
            controllerBtnUi[i].SetActive(false);
        }


        Time.timeScale = 0;
        /*currentTimeGift = startTimeGift;
        stopTimerGift = false;*/

        GiftPanel.SetActive(true);





        int randomNum = UnityEngine.Random.Range(0, 2);
        if (randomNum == 0)
        {
            giftImage.sprite = chicken;
            player.GetComponent<PlayerUi>().FoodGift();
        }
        else {
            giftImage.sprite = clock;
            player.GetComponent<PlayerUi>().TimeGift();
        }

    }

    public void CancelGiftPanel()
    {

       
        Time.timeScale = 1;
        currentTimeGift = startTimeGift;
        stopTimerGift = false;

        GiftPanel.SetActive(false);

        for (int i = 0; i < controllerBtnUi.Length; i++)
        {
            controllerBtnUi[i].SetActive(true);
        }
    }


    public void BackToMenu()
    {
        int menuId = PlayerPrefs.GetInt("levelControllerId");

        PlayerPrefs.SetInt("sceneId", levelId);
        UnityEngine.SceneManagement.SceneManager.LoadScene(menuId);
    }


    string typeAds;
    public void MessageWarning(string type) {
        typeAds = type;
        StartCoroutine(timerWarningMessage());

        GameOverPanel.SetActive(false);
        GiftPanel.SetActive(false);

        /*if (type == "AfterDied") {
            GameOverPanel.SetActive(false);
        } else if (type == "Gift") {
            GiftPanel.SetActive(false);
        
        }   */
    }


    IEnumerator timerWarningMessage() {
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        messagePanel.SetActive(false);
    }

    public void CloseMessageWarning()
    {
        messagePanel.SetActive(false);

        if (typeAds == "AfterDied")
        {
            GameOverPanel.SetActive(true);
        }
        
    }


}

