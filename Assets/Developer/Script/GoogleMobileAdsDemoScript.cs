using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
//using GoogleMobileAds.Api.Mediation.InMobi;
using System.Collections.Generic;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    public static GoogleMobileAdsDemoScript Instance = null;
    private BannerView bannerView;
    private BannerView bannerViewBottom;
    private InterstitialAd interstitial;
    private InterstitialAd Videointerstitial;
    //	private NativeExpressAdView nativeExpressAdView;
    public UIManager uimanager;
    // private RewardBasedVideoAd rewardBasedVideo;
    private RewardedAd rewardedAd;
    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;

    public GameObject PlayTypeButtonGroup, PlayerProfile, PrivatePlayMode, VsCpuDifficultyselection, SettingGroup, Registration, Tutorial
        , InterfaceUI, selectBoardGroup, selectBoardGroupfB, shop;
    public GameObject watchVideoAnimated;

    int Data = 0;
    int Data1 = 0;
    int Data2 = 0;

    bool topBannerActive;
    bool bottomBannerActive;

    Dictionary<string, string> consentObject = new Dictionary<string, string>();

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //			DontDestroyOnLoad (this);
        }
        //else
        //{
        //    DestroyImmediate(gameObject);
        //}
        //		Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });


        this.RequestRewardBasedVideo();
        RequestInterstitial();
        RequestInterstitialVideo();
        RequestBottomBanner();
        RequestTopBanner();
        //		RequestBanner (0);
        //		this.bannerView.LoadAd (this.CreateAdRequest ());

        consentObject.Add("gdpr_consent_available", "true");
        consentObject.Add("gdpr", "1");

        //InMobi.UpdateGDPRConsent(consentObject);
    }

    private void OnInitialized(bool success)
    {
        if (success)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Not Initialized");
        }

    }

    void OnDisable()
    {
        //OnBannerAdsStop();
    }

    public void Update()
    {
        //Debug.Log("fff:-"+PlayerPrefs.GetInt("adState"));
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
        //if (PlayerPrefs.GetInt("adState") != 0)
        //{
        //    //if((PlayTypeButtonGroup.activeSelf && InterfaceUI.activeSelf) || (PlayerProfile.activeSelf) || (PrivatePlayMode.activeSelf && InterfaceUI.activeSelf) || VsCpuDifficultyselection.activeSelf)
        //    if ((PlayTypeButtonGroup.activeSelf && InterfaceUI.activeSelf) || (PrivatePlayMode.activeSelf && InterfaceUI.activeSelf) || VsCpuDifficultyselection.activeSelf)
        //    {
        //        //	Debug.Log("If");
        //        if ((!Tutorial.activeSelf) && (!SettingGroup.activeSelf) && (!selectBoardGroup.activeSelf) && (!selectBoardGroupfB.activeSelf))
        //        {
        //            if (!Registration.activeSelf)
        //            {
        //                if (Data == 0)
        //                {
        //                    Debug.Log("ffff");
        //                    RequestBanner(0);
        //                    this.bannerView.Show();
        //                    Data = 1;
        //                    Data1 = 0;
        //                    Data2 = 0;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("Hide");
        //            this.bannerView.Hide();
        //            Data = 0;
        //            Data1 = 0;
        //            Data2 = 1;
        //        }
        //    }
        //    else
        //    {
        //        //			if(Data2 == 0)
        //        //			{
        //        //				Debug.Log("Else");
        //        this.bannerView.Hide();
        //        Data = 0;
        //        Data1 = 0;
        //        Data2 = 1;

        //        //			}
        //    }
        //}
        if (PlayerPrefs.GetInt("adState") != 0)
        {
            //if (Tutorial.activeSelf)
            //{
            //    GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
            //}

            //if (selectBoardGroup.activeSelf)
            //{
            //    GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
            //}

            //if (selectBoardGroupfB.activeSelf)
            //{
            //    GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
            //}

            //if (Registration.activeSelf)
            //{
            //    GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
            //}

            //if (shop.activeSelf)
            //{
            //    GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
            //}
        }
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
                //.AddTestDevice (AdRequest.TestDeviceSimulator)
                //.AddTestDevice ("0123456789ABCDEF0123456789ABCDEF")
                //.AddKeyword ("game")
                //.SetGender (Gender.Male)
                //.SetBirthday (new DateTime (1985, 1, 1))
                // .TagForChildDirectedTreatment(false)
                //.AddExtra ("color_bg", "9B30FF")
                .Build();
    }

    public void RequestTopBanner()
    {
        if (PlayerPrefs.GetInt("adState") != 0)
        {
#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
		//string adUnitId = "ca-app-pub-3940256099942544/6300978111";//test id
        string adUnitId = "ca-app-pub-7368003023090562/6401995002"; //real id
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7368003023090562/1200898000";
#else
		string adUnitId = "unexpected_platform";
#endif
            AdSize adSize = new AdSize(300, 50);

            AdPosition pos = new AdPosition();
            pos = AdPosition.Top;

            this.bannerView = new BannerView(adUnitId, adSize, pos);

            // Register for ad events.
            this.bannerView.OnAdLoaded += this.HandleAdLoaded;
            this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
            this.bannerView.OnAdOpening += this.HandleAdOpened;
            this.bannerView.OnAdClosed += this.HandleAdClosed;
            // this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

            // Load a banner ad.
            this.bannerView.LoadAd(this.CreateAdRequest());
            //this.bannerView.Show();
            Debug.Log("Request Banner Top");
        }
    }

    public void RequestBottomBanner()
    {
        if (PlayerPrefs.GetInt("adState") != 0)
        {
            // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
            string adUnitId = "unused";
#elif UNITY_ANDROID
		//string adUnitId = "ca-app-pub-3940256099942544/6300978111";//test id
          string adUnitId = "ca-app-pub-7368003023090562/4010322308 ";//real id
            //final id ca-app-pub-7368003023090562/4010322308
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7368003023090562/3196830333";
#else
		string adUnitId = "unexpected_platform";
#endif

            // Create a 320x50 banner at the top of the screen.
            AdSize adSize = new AdSize(300, 50);

            AdPosition pos = new AdPosition();
            pos = AdPosition.Bottom;
            this.bannerViewBottom = new BannerView(adUnitId, adSize, pos);

            // Register for ad events.
            this.bannerViewBottom.OnAdLoaded += this.HandleAdLoadedBottom;
            this.bannerViewBottom.OnAdFailedToLoad += this.HandleAdFailedToLoadBottom;
            this.bannerViewBottom.OnAdOpening += this.HandleAdOpenedBottom;
            this.bannerViewBottom.OnAdClosed += this.HandleAdClosedBottom;
            //this.bannerViewBottom.OnAdLeavingApplication += this.HandleAdLeftApplicationBottom;

            // Load a banner ad.
            this.bannerViewBottom.LoadAd(this.CreateAdRequest());
            //this.bannerViewBottom.Show();

            Debug.Log("Request Banner Bottom");
        }
    }

    public void DestroyBanner()
    {
        /*if(bannerView!= null)
        {
         this.bannerView.Destroy();
        }*/
    }

    public void DestroyBannerBottom()
    {
        /*if (bannerViewBottom != null)
        {
            this.bannerViewBottom.Destroy();
        }*/
    }

    //	public void RequestBanner (int No)
    //	{
    //        if (PlayerPrefs.GetInt("adState") != 0)
    //        {
    //            // These ad units are configured to always serve test ads.
    //#if UNITY_EDITOR
    //            string adUnitId = "unused";
    //#elif UNITY_ANDROID
    //		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
    //            //final id ca-app-pub-7368003023090562/6401995002
    //#elif UNITY_IPHONE
    //		string adUnitId = "ca-app-pub-7368003023090562/1200898000";
    //#else
    //		string adUnitId = "unexpected_platform";
    //#endif

    //            // Create a 320x50 banner at the top of the screen.
    //            AdSize adSize = new AdSize(300, 50);

    //            AdPosition pos = new AdPosition();

    //            if (No == 0)
    //            {
    //                //			this.bannerView = new BannerView (adUnitId, adSize, AdPosition.Top);
    //                pos = AdPosition.Top;
    //            }
    //            else if (No == 1)
    //            {
    //                //			this.bannerView = new BannerView (adUnitId, adSize, AdPosition.Bottom);
    //                pos = AdPosition.Bottom;
    //            }

    //            Debug.Log("pos:-" + pos);
    //            this.bannerView = new BannerView(adUnitId, adSize, pos);

    //            // Register for ad events.
    //            this.bannerView.OnAdLoaded += this.HandleAdLoaded;
    //            this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
    //            this.bannerView.OnAdOpening += this.HandleAdOpened;
    //            this.bannerView.OnAdClosed += this.HandleAdClosed;
    //            this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

    //            // Load a banner ad.
    //            this.bannerView.LoadAd(this.CreateAdRequest());
    //        }
    //	}

    public void ShowBanner()
    {
        Debug.Log("ShowBanner ads top");
        if (PlayerPrefs.GetInt("adState") != 0 && this.bannerView != null)
        {
            this.bannerView.Show();
        }
    }

    public void ShowBannerBottom()
    {
        Debug.Log("ShowBanner ads bottom");
        if (PlayerPrefs.GetInt("adState") != 0 && this.bannerViewBottom != null)
        {
            this.bannerViewBottom.Show();
        }
    }

    public void HideBanner()
    {
        if (this.bannerView != null)
            this.bannerView.Hide();
    }

    public void HideBannerBottom()
    {
        if (this.bannerViewBottom != null)
            this.bannerViewBottom.Hide();
    }

    private void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		//string adUnitId = "ca-app-pub-3940256099942544/1033173712"; // test id
          string adUnitId = "ca-app-pub-7368003023090562/7199592424"; //real id
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7368003023090562/4673563531";
#else
		string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        //  this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    private void RequestInterstitialVideo()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-7368003023090562/9210292990";
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7368003023090562/9168617310";
#else
		string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        this.Videointerstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.Videointerstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.Videointerstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.Videointerstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.Videointerstitial.OnAdClosed += this.HandleInterstitialClosed;
        //this.Videointerstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.Videointerstitial.LoadAd(this.CreateAdRequest());

    }

    private void RequestNativeExpressAdView()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/1072772517";
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/2562852117";
#else
		string adUnitId = "unexpected_platform";
#endif

        // Create a 320x150 native express ad at the top of the screen.
        //		this.nativeExpressAdView = new NativeExpressAdView (
        //			adUnitId,
        //			new AdSize (320, 150),
        //			AdPosition.Top);

        // Register for ad events.
        //		this.nativeExpressAdView.OnAdLoaded += this.HandleNativeExpressAdLoaded;
        //		this.nativeExpressAdView.OnAdFailedToLoad += this.HandleNativeExpresseAdFailedToLoad;
        //		this.nativeExpressAdView.OnAdOpening += this.HandleNativeExpressAdOpened;
        //		this.nativeExpressAdView.OnAdClosed += this.HandleNativeExpressAdClosed;
        //		this.nativeExpressAdView.OnAdLeavingApplication += this.HandleNativeExpressAdLeftApplication;
        //
        //		// Load a native express ad.
        //		this.nativeExpressAdView.LoadAd (this.CreateAdRequest ());
    }

    private void RequestRewardBasedVideo()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
		string adUnitId ="ca-app-pub-7368003023090562/4705769957"; //real id
        //string adUnitId = "ca-app-pub-3940256099942544/5224354917";//test id
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-7368003023090562/7928525943";
#else
		string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
       // this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);






        //// Get singleton reward based video ad reference.
        //this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        //// RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        //this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        //this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        //this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        //this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        //this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        //this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        //this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
        //this.rewardBasedVideo.OnAdCompleted += this.HandleRewardBasedVideoCompletedVideo;


        //AdRequest request = new AdRequest.Builder().Build();
        //this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        watchVideoAnimated.SetActive(false);
        Debug.Log("WatchVideoAnimated deactive by google");
        //throw new NotImplementedException();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        watchVideoAnimated.SetActive(true);
        Debug.Log("WatchVideoAnimated active by google");
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        watchVideoAnimated.SetActive(false);
        Debug.Log("WatchVideoAnimated deactive by google");
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        isRewarded = false;
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");

        if (!isRewarded)
        {
            //			temptxt.text = "Closed";
            //			g.GetComponent<Renderer> ().material = UIManager.instance.btnMats [2];
        }
        watchVideoAnimated.SetActive(false);
        Debug.Log("WatchVideoAnimated deactive by google");
        this.RequestRewardBasedVideo();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        isRewarded = true;
        //		g.GetComponent<Renderer> ().material = UIManager.instance.btnMats [1];
        //		UIManager.instance.generateObj ();
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);

        if (PlayerPrefs.GetInt("ShopVideoUsedData") == 10)
        {
            uimanager.playerInfo.coins += 300;
            uimanager.SavePlayerInfo();
            PlayerPrefs.SetInt("ShopVideoUsedData", 0);

            // Kochava.Event myEvent = new Kochava.Event(Kochava.EventType.AdView);
            // myEvent.name = "Admob rewarded video ad finished";
            // Kochava.Tracker.SendEvent(myEvent);
        }


        PlayerPrefs.SetString("VideoCompleted", "true");

    }

    public void ShowInterstitial()
    {
        if (interstitial == null)
        {
            RequestInterstitial();
        }
        else
        {
            if (PlayerPrefs.GetInt("adState") != 0)
            {
                Debug.Log("Show Inter");
                if (this.interstitial.IsLoaded())
                {
                    Debug.Log("Show Inter loaded");
                    this.interstitial.Show();
                    RequestInterstitial();
                    print("denish ads :- ");

                }
                else
                {
                    ToastExample.Instance.Toast();
                    Debug.Log("Show Inter Not loaded");
                    RequestInterstitial();
                    MonoBehaviour.print("Interstitial is not ready yet");
                }
            }
        }
    }

    public void ShowInterstitialVideo()
    {
#if UNITY_ANDROID
        string adUnitForVideo = "Android_Interstitial";
#endif
#if UNITY_IPHONE
        string adUnitForVideo = "iOS_Interstitial";
#endif

        if (PlayerPrefs.GetInt("adState") != 0)
        {
            Debug.Log("Show Inter");
            if (GetComponent<UnityAdsManager>().IsReady(adUnitForVideo))
            {
                GetComponent<UnityAdsManager>().ShowNonRewardedAd();
            }
            else if (this.Videointerstitial.IsLoaded())
            {
                Debug.Log("Show Inter loaded");
                this.Videointerstitial.Show();
                RequestInterstitialVideo();
                print("denish ads :- ");
            }
            else if (this.interstitial.IsLoaded())
            {
                Debug.Log("Show Inter loaded");
                this.interstitial.Show();
                RequestInterstitial();
                print("denish ads :- ");
            }
            else
            {
                ToastExample.Instance.Toast();
                Debug.Log("Show Inter Not loaded");
                RequestInterstitialVideo();
                MonoBehaviour.print("Interstitial is not ready yet");
            }
        }
    }

    public void ShowRewardBasedVideo()
    {
        if (PlayerPrefs.GetInt("adState") != 0)
        {
            Debug.Log("Show reward");
            if (this.rewardedAd.IsLoaded())
            {
                Debug.Log("Show reward load");
                this.rewardedAd.Show();
                RequestRewardBasedVideo();
            }
            else if (GetComponent<UnityAdsManager>().IsReady("rewardedVideoZone"))
            {
                GetComponent<UnityAdsManager>().ShowRewardedAd();
            }
            else
            {
                ToastExample.Instance.Toast();
                Debug.Log("Show reward not load");
                MonoBehaviour.print("Reward based video ad is not ready yet");
                this.RequestRewardBasedVideo();
            }
        }
    }

    public void ShowRewardBasedVideoShop()
    {
        Debug.Log("Show reward");

#if UNITY_ANDROID
        string adUnitForRewardedVideo = "Android_Rewarded";
#endif
#if UNITY_IPHONE
        string adUnitForRewardedVideo = "iOS_Rewarded";
#endif
        if (GetComponent<UnityAdsManager>().IsReady(adUnitForRewardedVideo))
        {
            Debug.Log("Show reward if video is ready");
            GetComponent<UnityAdsManager>().ShowRewardedAd();
            Debug.Log("Show reward after show ad");
        }
        else if (this.rewardedAd.IsLoaded())
        {
            Debug.Log("Show reward load");
            this.rewardedAd.Show();
            RequestRewardBasedVideo();
        }
        else
        {
            ToastExample.Instance.Toast();
            Debug.Log("Show reward not load");
            MonoBehaviour.print("Reward based video ad is not ready yet");
            this.RequestRewardBasedVideo();
        }
    }

    #region Banner callback handlers
    #endregion
    //public void OnBannerAdsShow ()
    //{
    //       if((!shop.activeSelf) &&(!selectBoardGroup.activeSelf) && (!selectBoardGroupfB.activeSelf))
    //       {
    //	  this.bannerView.Show ();
    //       }
    //}

    //public void OnBannerAdsStop ()
    //{
    //       this.bannerView.Destroy();
    //	//this.bannerView.Hide ();
    //}

    #region TopBannerCallBack
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        topBannerActive = true;
        //this.bannerView.Show ();
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    #region BottomBannerCallBack
    public void HandleAdLoadedBottom(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        bottomBannerActive = true;
        //this.bannerView.Show ();
    }

    public void HandleAdFailedToLoadBottom(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpenedBottom(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosedBottom(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplicationBottom(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }
    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print(
        //    "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        //Kochava.Event myEvent = new Kochava.Event(Kochava.EventType.AdView);
        //myEvent.name = "Admob interstitial ad displayed";
        //Kochava.Tracker.SendEvent(myEvent);
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        //Kochava.Event myEvent = new Kochava.Event(Kochava.EventType.AdClick);
        //myEvent.name = "Admob interstitial ad clicked";
        //Kochava.Tracker.SendEvent(myEvent);
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region Native express ad callback handlers

    public void HandleNativeExpressAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdLoaded event received");
    }

    public void HandleNativeExpresseAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print(
        //    "HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleNativeExpressAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdOpened event received");
    }

    public void HandleNativeExpressAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdClosed event received");
    }

    public void HandleNativeExpressAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleNativeExpressAdAdLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print(
        //    "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        isRewarded = false;
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        if (!isRewarded)
        {
            //			temptxt.text = "Closed";
            //			g.GetComponent<Renderer> ().material = UIManager.instance.btnMats [2];
        }
        this.RequestRewardBasedVideo();
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    //	public UnityEngine.UI.Text temptxt;
    //	public GameObject g;
    bool isRewarded;

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        //		temptxt.text = "1";
        //		int val = (int.Parse (temptxt.text) + 1);
        //		temptxt.text = val.ToString ();
        isRewarded = true;
        //		g.GetComponent<Renderer> ().material = UIManager.instance.btnMats [1];
        //		UIManager.instance.generateObj ();
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);

        if (PlayerPrefs.GetInt("ShopVideoUsedData") == 10)
        {
            uimanager.playerInfo.coins += 300;
            uimanager.SavePlayerInfo();
            PlayerPrefs.SetInt("ShopVideoUsedData", 0);

            //Kochava.Event myEvent = new Kochava.Event(Kochava.EventType.AdView);
            //myEvent.name = "Admob rewarded video ad finished";
            //Kochava.Tracker.SendEvent(myEvent);
        }


        PlayerPrefs.SetString("VideoCompleted", "true");
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    public void HandleRewardBasedVideoCompletedVideo(object sender, EventArgs args)
    {
        //Completed video call method used

        MonoBehaviour.print("HandleRewardBasedVideoLCompletedvieo event received");
    }
    #endregion

    public void InterstailSet3Time()
    {
        if (PlayerPrefs.GetInt("adState") != 0)
        {
            if (PlayerPrefs.GetInt("Board3Time") >= 2)
            {
                Debug.Log("Other");
                ShowInterstitial();
                PlayerPrefs.SetInt("Board3Time", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Board3Time", PlayerPrefs.GetInt("Board3Time") + 1);
            }
        }
    }

    public void ShopRewardevideoUsed()
    {
        PlayerPrefs.SetInt("ShopVideoUsedData", 10);
        ShowRewardBasedVideoShop();
    }
}
