using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Ads : MonoBehaviour
{
   

    private static string APP_ID = "ca-app-pub-7505493635574859~1943029332";

    private static InterstitialAd interstitialAd;
    private static RewardedAd rewardedAd;
    string adUnitInterstitialId = "ca-app-pub-7505493635574859/4694489218";
    string Rewarded_ID = "ca-app-pub-7505493635574859/4924415896";



    //public Controller controller;
    //public Player_Movement player;
    // Start is called before the first frame update
    void Start()
    {

        // Initialize the Google Mobile Ads SDK.
        //MobileAds.Initialize(initStatus => { });
        MobileAds.Initialize(APP_ID);
        //PlayerPrefs.DeleteKey("ads");

       
            RequestInterstitial();
            RequestRewardedVideo();
       
    }



    ///
    private void RequestInterstitial()
    {


        // Initialize an InterstitialAd.
        interstitialAd = new InterstitialAd(adUnitInterstitialId);

        // Called when an ad request has successfully loaded.
        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitialAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitialAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitialAd.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {

    }

    ////Reward
    ///
    void RequestRewardedVideo()
    {

        // rewardVideoAd = RewardBasedVideoAd.Instance;

        rewardedAd = new RewardedAd(Rewarded_ID);

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;


        //for real app
        AdRequest request = new AdRequest.Builder().Build();


        //for testing
        /// AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        // Load the banner with the request.
        //rewardVideoAd.LoadAd(request, Rewarded_ID);
        rewardedAd.LoadAd(request);

    }


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        //Debug.Log("mymymyq " + "AdLoaded");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        //Debug.Log("mymymyq " + "FailedToLoad");
        //Debug.Log("mymymyq " + "FailedToLoad " + args.Message);
        RequestRewardedVideo();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        //Debug.Log("mymymyq " + "AdOpening");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        //Debug.Log("mymymyq " + "FailedToShow");
        //Debug.Log("mymymyq " + "FailedToShow " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //Debug.Log("mymymyq " + "AdClosed");
        RequestRewardedVideo();
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //Debug.Log("mymymyq " + "1000000");
        //player.ContinueAfterDied();
        if (rewardType != "" && rewardType != null) {
            //Debug.Log("mymymyq " + "2000000");
            if (rewardType == "AfterDied") {
                //Debug.Log("mymymyq " + "3000000");
                GetComponent<Controller>().ReturnPlayerAfterDied();
            } else if (rewardType == "Gift") {
                //Debug.Log("mymymyq " + "4000000");
                GetComponent<Controller>().Gift();
            }
            //Debug.Log("mymymyq " + "5000000");
        }
        //Debug.Log("mymymyq " + "6000000");
        //controller.ReturnPlayerAfterDied();
    }



    ////// Display
    ///

    public static void display_interstitialr()
    {
       

            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
            }

       
    }

    static string rewardType;
    public void display_Rewarded_video(string _rewardType)
    {
        //Debug.Log("mymymyq " + "1000");
        rewardType = _rewardType;

        if (rewardedAd.IsLoaded())
        {
           // Debug.Log("mymymyq " + "2000");
            rewardedAd.Show();
        }
        else {
            //Debug.Log("mymymyq " + "3000");
            GetComponent<Controller>().MessageWarning(_rewardType);
        }
        //Debug.Log("mymymyq " + "4000");
    }

    

}
