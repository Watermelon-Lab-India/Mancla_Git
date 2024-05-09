using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_IOS
		private string GAME_ID = "2364066"; //replace with your gameID from dashboard. note: will be different for each platform.
        private const string BANNER_PLACEMENT = "banner";
        private const string VIDEO_PLACEMENT = "iOS_Interstitial";
        private const string REWARDED_VIDEO_PLACEMENT = "iOS_Rewarded";
#elif UNITY_ANDROID
    private string GAME_ID = "69662"; //replace with your gameID from dashboard. note: will be different for each platform.
    private const string BANNER_PLACEMENT = "banner";
    private const string VIDEO_PLACEMENT = "Android_Interstitial";
    private const string REWARDED_VIDEO_PLACEMENT = "Android_Rewarded";
#endif

    [SerializeField] GameObject watchVideoAnimated;
    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private bool testMode = false;
    private bool showBanner = false;
    private bool isRewardedReady = false;
    private bool isInterstitialReady = false;
    //utility wrappers for debuglog
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    public static UIManager uimanager;

    private void Awake()
    {
        Initialize();
        uimanager = this.gameObject.GetComponent<UIManager>();
    }

    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            DebugLog(Application.platform + " supported by Advertisement");
        }
        Advertisement.Initialize(GAME_ID, testMode, this);
    }

    public void ToggleBanner() 
    {
        showBanner = !showBanner;

        if (showBanner)
        {
            Advertisement.Banner.SetPosition(bannerPosition);
            Advertisement.Banner.Show(BANNER_PLACEMENT);
        }
        else
        {
            Advertisement.Banner.Hide(false);
        }
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }

    public void LoadNonRewardedAd()
    {
        Advertisement.Load(VIDEO_PLACEMENT, this);
    }

    public void ShowNonRewardedAd()
    {
        Advertisement.Show(VIDEO_PLACEMENT, this);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        LoadRewardedAd();
        LoadNonRewardedAd();
        DebugLog("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");

        if (placementId == REWARDED_VIDEO_PLACEMENT)
        {
            isRewardedReady = true;
            watchVideoAnimated.SetActive(true);
        }
        else
        {
            isInterstitialReady = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");

        if (placementId == REWARDED_VIDEO_PLACEMENT)
        {
            isRewardedReady = false;
            watchVideoAnimated.SetActive(false);
        }
        else
        {
            isInterstitialReady = false;
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

        if (placementId == REWARDED_VIDEO_PLACEMENT)
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                if (PlayerPrefs.GetInt("ShopVideoUsedData") == 10)
                {
                    uimanager.playerInfo.coins += 300;
                    uimanager.SavePlayerInfo();
                    PlayerPrefs.SetInt("ShopVideoUsedData", 0);
                    // myEvent.name = "Unity Rewarded video ad finished";
                    // Kochava.Tracker.SendEvent(myEvent);
                }
            }
            isRewardedReady = false;
            watchVideoAnimated.SetActive(false);
            LoadRewardedAd();

        }
        else 
        {
            LoadNonRewardedAd();
        }
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
    }
    #endregion


    public bool IsReady(string adUnitId)
    {
        bool ready;
        if (adUnitId == REWARDED_VIDEO_PLACEMENT)
        {
            ready = isRewardedReady;
        }
        else
        {
            ready = isInterstitialReady;
        }
        return ready;
    }

    public void OnGameIDFieldChanged(string newInput)
    {
        GAME_ID = newInput;
    }

    public void ToggleTestMode(bool isOn)
    {
        testMode = isOn;
    }

    //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}
