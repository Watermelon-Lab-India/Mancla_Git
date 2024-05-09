using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using PlayFab;
//using PlayFab.ClientModels;
//using UnityEngine.Advertisements;
using Photon.Realtime;
using Photon.Pun;
using SimpleJson;

//using GooglePlayGames;
//using GooglePlayGames.BasicApi;

//using Parse;
using CloudOnce;
using System.Threading.Tasks;

public class PlayerInformation
{
    public int logInWithFB = 0;
    public bool isGoogleLogin = false;
    public string playerName = "";
    public int coins = 500;  //500
    public int winCount = 0;
    public int totalMatchCount = 0;
    public int matchLoseCount = 0;
    public int rank = 0;
    public int highestRank = 0;
    public string characterName = "";
    public string characterId = "";
    public DateTime freeCoinTime;
    public int[] tableOpenState;
    public bool gameMusic = true;
    public bool gameSound = true;
    public int adsState = 1;
    public bool isFirstWin = false;
    public bool achievement_semi = false;
    public bool achievement_pro = false;
    public bool achievement_veteran = false;
    public bool achievement_expert = false;
    public int curScore = 0;
}

public class UIManager : MonoBehaviour
{
    static public string[] randNames = {"Christina89","Alex1234","Rooney Rocks","Luke786",
        "Melina Holmes","Melinda Gates","Sarah45","Nicole23","TomTom",
        "Kiera86","Tanya Hot22","Jordan","Nicole Kidman","Jon Snow",
        "Raven61","Cierra","Richard","Jyajah","Nancie",
        "Mario1990","Mr.Logan","Courtney","Marcus1234","Wanda",
        "Pablo","Becky","Jessica","Marlin","Sally",
        "Rochell","Kaysha","Ashley","Ruthie","Diandra"};

    public GameObject interfaceUI, gameUI, interback, gameBack, chat, emotion, gameResult, timmerObject, exitUI, settings, playTypeBtnGrp, privateCodeUI, leftSettingUI;

    public GameObject registerPanel, wifi_connection;

    public GameObject uiFacebookPlay;

    public GameObject uiLoadingAnim;

    public GameObject pointerAnim;

    public GameObject challengeWindow, challengeWindow1;

    public PlayerInformation playerInfo = new PlayerInformation();

    public Text playerName, coins, freeCoinTimer;

    public Image characterImage_facebook;

    public Image[] charactersImages;

    public Sprite[] characters;

    bool freeCoinState = false;

    int freeCoinWaitTime = 7200;

    public bool myPositionInfo = false;

    float dTime = 0f;

    public GoogleMobileAdsDemoScript GoogleAd;

    [Header("Remove ads image object")]
    public GameObject CoinImage;
    public GameObject BGImage;
    public GameObject TextObject;

    [Header("Facebook like image object")]
    public GameObject FacebookLike;
    public GameObject FLCoinImage;
    public GameObject FLBGImage;
    public GameObject FLTextObject;

    ParseManager parsMang;

    public GameObject exitWindow;
    public static UIManager _instance;
    public int game_Mode = 0;
    public GameObject PrivatePlayScreen;
    public InputField RoomCodeInputField;
    public Text txtwarning;
    // Use this for initialization
    void Start()
    {
        _instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //ParseObject gameScore = new ParseObject("GameScore");
        //gameScore["score"] = 1337;
        //gameScore["playerName"] = "sean plott";
        //System.Threading.Tasks.Task task = gameScore.SaveAsync();

        //				playerInfo.coins += 100000;
        //		SavePlayerInfo();
        //		Debug.Log("coin:-"+playerInfo.coins);

        //				PlayerPrefs.DeleteAll();

        // PlayerPrefs.SetInt("adState", 0);

        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        //PlayGamesPlatform.InitializeInstance(config);

        game_Mode = PlayerPrefs.GetInt("gamemode", game_Mode);

        if (PlayerPrefs.GetString(Constants.KEY_TUTORIAL, "true") == "false")
        {
            pointerAnim.SetActive(false);
        }
        if (PlayerPrefs.GetInt("FacebookLikeUsed") == 10)
        {
            FLCoinImage.SetActive(false);
            FLBGImage.SetActive(false);
            FLTextObject.SetActive(false);
            FacebookLike.SetActive(false);
        }
        parsMang = this.gameObject.GetComponent<ParseManager>();
        
    }


    /*Parse.Cloud.define("destroyObj", async(request) => {

 var recipientUserId = request.params.recipientId;

 let Class = Parse.Object.extend("Installation");
    let query = new Parse.Query(Class);

    query.equalTo("appVersion", recipientUserId);

 let results = await query.find();

 for(let i = 0; i<results.length; i++){
    myObject = results[i];
    console.log("This object will be destroyed = ", myObject.id)

    try {
       await myObject.destroy();
    console.log("Object destroyed")
    } catch (e) {
       console.log("Error = ", e.message)
    }
 }
 return "Function executed!" 
})*/



    public void ChangeMode(int gamemode)
    {
        game_Mode = gamemode;
        PlayerPrefs.SetInt("gamemode", game_Mode);
        Debug.Log("game mode : " + game_Mode);
    }

    //	public void Data()
    //	{
    ////		Destroy(GoogleAd.gameobject);
    //		PlayerPrefs.SetInt("adState",0);
    //	}

    public void RemoveAds()
    {
        PlayerPrefs.SetInt("adState", 0);
    }

    public void DestroyBannerAds()
    {
       // parsMang.DeleteObjectFromParse();

        GoogleMobileAdsDemoScript.Instance.DestroyBanner();
        GoogleMobileAdsDemoScript.Instance.DestroyBannerBottom();
    }

    public void RankingButtonClicked()
    {
#if UNITY_ANDROID
        /*
        if (!playerInfo.isGoogleLogin)
        {
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate(AuthenticateHandler);
            return;
        }

        PlayGamesPlatform.Activate();
        // Social.localUser.Authenticate(AuthenticateHandler);
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIyKTG_KYOEAIQBg");
        */
#endif
    }

    public void ShowInterOnBackPressed()
    {
        /*GoogleMobileAdsDemoScript.Instance.ShowInterstitial();*/
        exitUI.SetActive(true);
    }

    void AuthenticateHandler(bool isSuccess)
    {
        UnityEngine.Debug.Log("called ----" + isSuccess);
        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "called ----";
        googleLoadingObj.SetActive(false);
        if (isSuccess)
        {
            playerInfo.isGoogleLogin = true;
            PlayerPrefs.SetInt("Login", 1);
            //float highScore = 10;// PlayerPrefs.GetFloat("HighScore", 0);
            //Social.ReportScore((long)highScore, "CgkIyKTG_KYOEAIQBg", (bool success) =>
            //{
            //    if (success)
            //    {
            //        UnityEngine.Debug.Log("called 1----");
            //        this.transform.parent.FindChild("Text").gameObject.GetComponent<Text>().text += "called 1----";
            //        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIyKTG_KYOEAIQBg");
            //    }
            //    else
            //    {
            //        UnityEngine.Debug.Log("called 2----");
            //        this.transform.parent.FindChild("Text").gameObject.GetComponent<Text>().text += "called 2----";
            //    }
            //});
        }
        else
        {
            playerInfo.isGoogleLogin = false;
            PlayerPrefs.SetInt("Login", 0);
            UnityEngine.Debug.Log("called ----3");
            this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "called 3----";
        }
        SetGoogleButtonState();
    }

    public void SendGameWinInfoToGooglePlay()
    {
        //if ((!playerInfo.isGoogleLogin) || (PlayerPrefs.GetInt("Login") == 0))
        //{
        //    Debug.Log("--------Not Login");
        //    //PlayGamesPlatform.Activate();
        //    //Social.localUser.Authenticate(AuthenticateHandler);
        //    return;
        //}
        CloudOnce.Leaderboards.leaderboard.SubmitScore((long)(playerInfo.winCount));
        //Social.ReportScore((long)(playerInfo.winCount), "CgkIyKTG_KYOEAIQBg", (bool success) =>
        //{
        //    if (success)
        //    {
        //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "game score update successed";
        //    }
        //    else
        //    {
        //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "game score update failed";
        //    }
        //});
    }

    public GameObject googleLoadingObj;

    public void GooglePlayButtonClicked()
    {
#if UNITY_ANDROID
        /*
        if (playerInfo.isGoogleLogin)
        {
            playerInfo.isGoogleLogin = false;
            PlayGamesPlatform.Instance.SignOut();
            SetGoogleButtonState();
            SavePlayerInfo();
        }
        else
        {
            playerInfo.isGoogleLogin = true;
            PlayGamesPlatform.Activate();
            googleLoadingObj.SetActive(true);
            Social.localUser.Authenticate(AuthenticateHandler);
        }
        // Social.localUser.Authenticate(AuthenticateHandler);
        // if(!Social.localUser.authenticated)
        // {
        //     Debug.Log("Not Authenticated");
        //  Social.localUser.Authenticate(AuthenticateHandler);
        ////  PlayGamesPlatform.Activate();
        // }
        // else
        // {
        //     Debug.Log("Already Authenticated");
        //     PlayGamesPlatform.Activate();
        // }
        */
#endif
    }

    public Text googleButton;
    public GameObject googleStateButtonObj;

    public void SetGoogleButtonState()
    {
        if (playerInfo.isGoogleLogin)
        {
            googleButton.text = "Off";
            googleStateButtonObj.SetActive(true);
        }
        else
        {
            googleButton.text = "On";
            googleStateButtonObj.SetActive(false);
        }

        SavePlayerInfo();
        //if (PlayerPrefs.GetInt("GoogleLogin") == 1)
        //{
        //    Debug.Log("Alreday on");
        //    googleButton.text = "Off";
        //    googleStateButtonObj.SetActive(true);
        //}
        //else
        //{
        //    Debug.Log("OFF");
        //    googleButton.text = "On";
        //    googleStateButtonObj.SetActive(false);
        //}
    }

    public void SendAchievementProgressToServer()
    {
        //if (!playerInfo.isGoogleLogin)
        //{
        //    //PlayGamesPlatform.Activate();
        //    //Social.localUser.Authenticate(AuthenticateHandler);
        //    return;
        //}

        if (!playerInfo.isFirstWin)
        {
            playerInfo.isFirstWin = true;
            SavePlayerInfo();
            CloudOnce.Achievements.first_win.Unlock();
            //Social.ReportProgress("CgkIyKTG_KYOEAIQAQ", 100.0f, (bool success) =>
            //{
            //    if (success)
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "game first win update successed";
            //    }
            //    else
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "game first win update failed";
            //    }
            //});
            return;
        }

        if (!playerInfo.achievement_semi)
        {
            float progress = 0.0f;
            if (playerInfo.winCount >= 11)
            {
                progress = 100.0f;
                playerInfo.achievement_semi = true;
                SavePlayerInfo();
            }
            else
            {
                progress = (float)((playerInfo.winCount - 1) / 10);
            }
            CloudOnce.Achievements.semi_pro.Unlock();
            //Social.ReportProgress("CgkIyKTG_KYOEAIQAg", progress, (bool success) =>
            //{
            //    if (success)
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update successed";
            //    }
            //    else
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update failed";
            //    }
            //});
        }

        if (!playerInfo.achievement_pro)
        {
            float progress = 0.0f;
            if (playerInfo.winCount >= 31)
            {
                progress = 100.0f;
                playerInfo.achievement_pro = true;
                SavePlayerInfo();
            }
            else
            {
                progress = (float)((playerInfo.winCount - 11) / 20);
            }
            CloudOnce.Achievements.pro.Unlock();
            //Social.ReportProgress("CgkIyKTG_KYOEAIQAw", progress, (bool success) =>
            //{
            //    if (success)
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update successed";
            //    }
            //    else
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update failed";
            //    }
            //});
        }

        if (!playerInfo.achievement_veteran)
        {
            float progress = 0.0f;
            if (playerInfo.winCount >= 61)
            {
                progress = 100.0f;
                playerInfo.achievement_veteran = true;
                SavePlayerInfo();
            }
            else
            {
                progress = (float)((playerInfo.winCount - 31) / 30);
            }
            CloudOnce.Achievements.veteran.Unlock();
            //Social.ReportProgress("CgkIyKTG_KYOEAIQBA", progress, (bool success) =>
            //{
            //    if (success)
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update successed";
            //    }
            //    else
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update failed";
            //    }
            //});
        }

        if (!playerInfo.achievement_expert)
        {
            float progress = 0.0f;
            if (playerInfo.winCount >= 101)
            {
                progress = 100.0f;
                playerInfo.achievement_expert = true;
                SavePlayerInfo();
            }
            else
            {
                progress = (float)((playerInfo.winCount - 61) / 40);
            }
            CloudOnce.Achievements.expert.Unlock();
            //Social.ReportProgress("CgkIyKTG_KYOEAIQBQ", progress, (bool success) =>
            //{
            //    if (success)
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update successed";
            //    }
            //    else
            //    {
            //        this.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "update failed";
            //    }
            //});
        }
    }

    public void ShowArchivmentUIButtonClicked()
    {
#if UNITY_ANDROID
        /*
        if (!playerInfo.isGoogleLogin)
        {
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate(AuthenticateHandler);
            return;
        }

        PlayGamesPlatform.Activate();
        Social.ShowAchievementsUI();
        */
#endif
    }

    //    public void ShowAdvertisements()
    //    {
    ////        if (Advertisement.IsReady("rewardedVideo"))
    ////        {
    ////            var option = new ShowOptions { resultCallback = HandleShowResult };
    ////            Advertisement.Show("rewardedVideo", option);
    ////        }
    //    }
    /*
    public void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
    */

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowChallengeWindow()
    {
        if (this.gameObject.GetComponent<NetworkingManager>().isConnecting)
        {
            this.gameObject.GetComponent<NetworkGlobalManager>().RejectChallenge();
            return;
        }

        if (gameResult.activeSelf)
        {
            interfaceUI.SetActive(true);
            interback.SetActive(true);

            gameUI.SetActive(false);
            gameBack.SetActive(false);

            chat.SetActive(false);
            emotion.SetActive(false);

            gameResult.SetActive(false);
            challengeWindow.SetActive(true);

            challengeWindow.transform.GetChild(4).gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<UIFaceBookManager>().chPlayerName + " challenged you!!!";
        }
        else
        {
            challengeWindow.SetActive(true);
            challengeWindow.transform.GetChild(4).gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<UIFaceBookManager>().chPlayerName + " challenged you!!!";
        }
    }

    public IEnumerator WaitSomeTimeForConnectting()
    {
        PhotonNetwork.LeaveRoom();
        yield return new WaitForSeconds(0f);
        challengeWindow.SetActive(false);
        string chPlayer = this.gameObject.GetComponent<UIFaceBookManager>().chPlayerName;
        string plId = this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId;
        this.gameObject.GetComponent<UIFaceBookManager>().SetOppoProfileImage();

        //        string rId = this.gameObject.GetComponent<UIFaceBookManager>().getFriendIdFromName(chPlayer);
        StartCoroutine(waitSomeTimeForLeaveRoom());
        //        this.gameObject.GetComponent<Engine>().AcceptPlayWithFacebookPlayer(this.gameObject.GetComponent<UIFaceBookManager>().playerFBId);
    }

    public void AcceptChallenge()
    {
        challengeWindow.transform.Find("LoadingAnimation").gameObject.SetActive(true);
        //        challengeWindow.transform.FindChild("LoadingAnimation").gameObject.SetActive(true);
        this.gameObject.GetComponent<NetworkGlobalManager>().SendAcceptChallenge(true);
        //        StartCoroutine(WaitSomeTimeForConnectting());
    }

    public void ChallengedRoomCreated(string roomId)
    {
        PhotonNetwork.LeaveRoom();
        string chPlayer = this.gameObject.GetComponent<UIFaceBookManager>().chPlayerName;
        string plId = this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId;
        this.gameObject.GetComponent<UIFaceBookManager>().SetOppoProfileImage();
        StartCoroutine(waitSomeTimeForLeaveRoom());
    }

    public IEnumerator waitSomeTimeForLeaveRoom()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<Engine>().AcceptPlayWithFacebookPlayer(this.gameObject.GetComponent<UIFaceBookManager>().playerFBId);
    }

    public GameObject backButton1, backButton2, backButton3;

    public GameObject SettingGroup, Tutorial, selectBoardGroup, selectBoardGroupfB, Registration, shop;

    // Update is called once per frame
    void Update()
    {
        //if(PlayerPrefs.GetInt("adState") !=0)
        //{
        //    if ((interfaceUI.activeSelf) && (!settings.activeSelf))
        //    {
        //        GoogleMobileAdsDemoScript.Instance.RequestBanner(0);
        //        GoogleMobileAdsDemoScript.Instance.ShowBanner();
        //    }

        //    if (settings.activeSelf)
        //    {
        //        GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
        //    }
        //}

        //		Debug.Log("Fb:-"+ PlayerPrefs.GetInt("FBState"));

        if (int.Parse(coins.text) != playerInfo.coins)
        {
            coins.text = playerInfo.coins.ToString();
        }

        if (wifi_connection != null)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                wifi_connection.SetActive(true);
            }
            else
            {
                wifi_connection.SetActive(false);
            }
        }

        if ((Input.GetKeyDown(KeyCode.Escape)) && interfaceUI.activeSelf && interfaceUI.transform.Find("PlayTypeButtonGroup").gameObject.activeSelf && !interfaceUI.transform.Find("FaceBookFriends").gameObject.activeSelf)
        {
            if (exitUI != null)
            {
                if (exitUI.activeSelf)
                {
                    Debug.Log("exit ui false");
                    exitUI.SetActive(false);
                }
                else
                {
                    //GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
                    Debug.Log("exit ui true");
                    exitUI.SetActive(true);
                }
            }
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) && gameUI.activeSelf)
        {
            Debug.Log("Escape 2");
            this.gameObject.GetComponent<Engine>().isWaitingOtherJoinRoom = false;
            interfaceUI.SetActive(true);
            interback.SetActive(true);
            playTypeBtnGrp.SetActive(true);

            gameUI.SetActive(false);
            gameBack.SetActive(false);
            chat.SetActive(false);
            emotion.SetActive(false);

            InitUIPlayerInfo();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) && !gameUI.activeSelf)
        {

            if (backButton1.transform.parent.gameObject.activeSelf)
            {
                Debug.Log("Escape 3");
                backButton1.GetComponent<Button>().onClick.Invoke();
            }
            else if (backButton2.transform.parent.gameObject.activeSelf)
            {
                Debug.Log("Escape 4");
                backButton2.GetComponent<Button>().onClick.Invoke();
            }
            else if (backButton3.activeSelf)
            {
                Debug.Log("Escape 5");
                backButton3.GetComponent<Button>().onClick.Invoke();
            }
        }
        if (freeCoinState && freeCoinWaitTime > 0 && Time.time - dTime >= 1f)
        {
            dTime = Time.time;
            freeCoinWaitTime--;
            if (freeCoinWaitTime == 0)
            {
                freeCoinState = false;
                freeCoinTimer.gameObject.SetActive(false);

            }
            else
            {
                int min, sec;
                int hour = freeCoinWaitTime / 3600;
                if (freeCoinWaitTime % 3600 == 0)
                {
                    min = 0;
                    sec = 0;
                }
                else
                {
                    min = (freeCoinWaitTime % 3600) / 60;
                    sec = freeCoinWaitTime - (3600 * hour + min * 60);
                }
                freeCoinTimer.text = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            }
        }
    }

    void Awake()
    {
        playerInfo.tableOpenState = new int[6];
        for (int i = 0; i < 6; i++)
        {
            //            playerInfo.tableOpenState[i] = 0;
        }
        if (this.transform.parent.gameObject.name == "Canvas")
        {
            Resolution curResolution = Screen.currentResolution;

            int curWidth = curResolution.width;
            int curHeight = curResolution.height;

            if ((curHeight / 9) * 16 > curWidth)
            {
                interfaceUI.transform.parent.gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            }
            else
            {
                interfaceUI.transform.parent.gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            }
        }
        else
        {
            interfaceUI.transform.parent.gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
        //PlayerPrefs.DeleteAll();
        InitPlayerInformation();
    }

    public void InitUIPlayerInfo()
    {
        playerName.text = playerInfo.playerName;
        coins.text = playerInfo.coins.ToString();
    }

    bool isFirstInstall = false;

    public GameObject adButtonDesk, adButtonShop, restorePurchaseButton;


    void OnApplicationFocus()
    {
        Debug.Log("Pause");
        int delta = (int)((DateTime.Now.Ticks - playerInfo.freeCoinTime.Ticks) / 10000000);
        freeCoinWaitTime = 7200 - delta;
        //		SavePlayerInfo();
    }

    //load player information whent the program initialized or started
    public void InitPlayerInformation()
    {
        playerInfo = new PlayerInformation();
        playerInfo.tableOpenState = new int[6];
        playerInfo.tableOpenState[0] = 1;
        string plName = PlayerPrefs.GetString("guestName");
        UnityEngine.Debug.Log("pl name ---" + plName);
        if (plName == "")
        {
            Debug.Log("Play name:-");
            isFirstInstall = true;
            registerPanel.SetActive(true);
            playerInfo.coins = 500;  //500
        }
        else
        {
            isFirstInstall = false;
            playerInfo.logInWithFB = PlayerPrefs.GetInt("FBState");

            if (playerInfo.logInWithFB == 1)
            {
                Debug.Log("Logfb 1");
                this.gameObject.GetComponent<UIFaceBookManager>().FaceBookLogin();
            }

            playerName.text = plName;

            if (PlayerPrefs.HasKey("google_Login"))
            {
                if (Convert.ToBoolean(PlayerPrefs.GetInt("google_Login")))
                {
#if UNITY_ANDROID
                    /*
                    PlayGamesPlatform.Activate();
                    Social.localUser.Authenticate(AuthenticateHandler);
                    playerInfo.isGoogleLogin = true;
                    */
#endif
                }
                else
                {
                    playerInfo.isGoogleLogin = false;
                }
            }
            else
            {
                playerInfo.isGoogleLogin = false;

            }

            if (PlayerPrefs.HasKey("adState"))
            {
                playerInfo.adsState = PlayerPrefs.GetInt("adState");
            }

            if (playerInfo.adsState == 0)
            {
                adButtonDesk.SetActive(false);
                adButtonShop.SetActive(false);
                CoinImage.SetActive(false);
                BGImage.SetActive(false);
                TextObject.SetActive(false);
                GoogleAd.enabled = false;
                restorePurchaseButton.SetActive(false);
            }

            int cInfo = PlayerPrefs.GetInt("numberOFchipsKey");
            if (cInfo < 0)
            {
                Debug.Log("Cinfo 00:-");
                playerInfo.coins = 0;
            }
            else
            {
                playerInfo.coins = cInfo;
                Debug.Log("Cinfo:-" + cInfo);
            }

            if (PlayerPrefs.HasKey("isFirstWin"))
            {
                playerInfo.isFirstWin = Convert.ToBoolean(PlayerPrefs.GetInt("isFirstWin"));
            }
            else
            {
                playerInfo.isFirstWin = false;
            }

            if (PlayerPrefs.HasKey("semi"))
            {
                playerInfo.achievement_semi = Convert.ToBoolean(PlayerPrefs.GetInt("semi"));
            }
            else
            {
                playerInfo.achievement_semi = false;
            }

            if (PlayerPrefs.HasKey("pro"))
            {
                playerInfo.achievement_pro = Convert.ToBoolean(PlayerPrefs.GetInt("pro"));
            }
            else
            {
                playerInfo.achievement_pro = false;
            }

            if (PlayerPrefs.HasKey("veteran"))
            {
                playerInfo.achievement_veteran = Convert.ToBoolean(PlayerPrefs.GetInt("veteran"));
            }
            else
            {
                playerInfo.achievement_veteran = false;
            }

            if (PlayerPrefs.HasKey("expert"))
            {
                playerInfo.achievement_expert = Convert.ToBoolean(PlayerPrefs.GetInt("expert"));
            }
            else
            {
                playerInfo.achievement_expert = false;
            }

            coins.text = playerInfo.coins.ToString();

            cInfo = PlayerPrefs.GetInt("tableOpenState");
            if (cInfo < 0)
            {
                playerInfo.tableOpenState[0] = 1;
            }
            else
            {
                playerInfo.tableOpenState[0] = 1;
            }

            cInfo = PlayerPrefs.GetInt("tableOpenState1");
            if (cInfo < 0)
            {
                playerInfo.tableOpenState[1] = 0;
            }
            else
            {
                playerInfo.tableOpenState[1] = cInfo;
            }

            cInfo = PlayerPrefs.GetInt("tableOpenState2");
            if (cInfo < 0)
            {
                playerInfo.tableOpenState[2] = 0;
            }
            else
            {
                playerInfo.tableOpenState[2] = cInfo;
            }

            cInfo = PlayerPrefs.GetInt("tableOpenState3");
            if (cInfo < 0)
            {
                playerInfo.tableOpenState[3] = 0;
            }
            else
            {
                playerInfo.tableOpenState[3] = cInfo;
            }

            cInfo = PlayerPrefs.GetInt("tableOpenState4");
            if (cInfo < 0)
            {
                playerInfo.tableOpenState[4] = 0;
            }
            else
            {
                playerInfo.tableOpenState[4] = cInfo;
                restorePurchaseButton.SetActive(false);
            }

            cInfo = PlayerPrefs.GetInt("tableOpenState5");
            if (cInfo < 0)
            {
                playerInfo.tableOpenState[5] = 0;
            }
            else
            {
                playerInfo.tableOpenState[5] = cInfo;
                restorePurchaseButton.SetActive(false);
            }

            cInfo = PlayerPrefs.GetInt("winCnt");
            if (cInfo < 0)
            {
                playerInfo.winCount = 0;
            }
            else
            {
                playerInfo.winCount = cInfo;
            }

            cInfo = PlayerPrefs.GetInt("loseCnt");
            if (cInfo < 0)
            {
                playerInfo.matchLoseCount = 0;
            }
            else
            {
                playerInfo.matchLoseCount = cInfo;
            }

            cInfo = PlayerPrefs.GetInt("totalCnt");
            if (cInfo < 0)
            {
                playerInfo.totalMatchCount = 0;
            }
            else
            {
                playerInfo.totalMatchCount = cInfo;
            }

            cInfo = PlayerPrefs.GetInt("rank");
            if (cInfo < 0)
            {
                playerInfo.rank = 0;
            }
            else
            {
                playerInfo.rank = cInfo;
            }

            cInfo = PlayerPrefs.GetInt("highestRank");
            if (cInfo < 0)
            {
                playerInfo.highestRank = 0;
            }
            else
            {
                playerInfo.highestRank = cInfo;
            }

            string timeString = PlayerPrefs.GetString("freeCoin");
            if (timeString == null || timeString == "")
            {
                playerInfo.freeCoinTime = DateTime.Now;
                freeCoinState = true;
                freeCoinTimer.gameObject.SetActive(true);
                Debug.Log("delta timestring");
            }
            else
            {
                playerInfo.freeCoinTime = DateTime.Parse(timeString);

                int delta = (int)((DateTime.Now.Ticks - playerInfo.freeCoinTime.Ticks) / 10000000);

                if (delta > 3600 * 2)
                {
                    freeCoinWaitTime = 0;
                    freeCoinState = false;
                    freeCoinTimer.gameObject.SetActive(false);
                    Debug.Log("delta if");
                }
                else
                {
                    freeCoinState = true;
                    freeCoinWaitTime = 7200 - delta;
                    freeCoinTimer.gameObject.SetActive(true);
                    Debug.Log("delta else");
                }
            }

            string characInfo = PlayerPrefs.GetString("characterId");
            if (characInfo == null || characInfo == "")
                characInfo = "0";

            playerInfo.characterName = characInfo;
            int cId = int.Parse(playerInfo.characterName);
            if (cId < 0)
                cId = 0;

            if (playerInfo.logInWithFB == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i == cId)
                    {
                        charactersImages[cId].gameObject.SetActive(true);
                    }
                    else
                    {
                        charactersImages[i].gameObject.SetActive(false);
                    }
                }
                characterImage_facebook.gameObject.SetActive(false);
            }
            else
            {
                characterImage_facebook.gameObject.SetActive(true);
            }

            registerPanel.SetActive(false);
            playerInfo.playerName = plName;
            playerName.text = playerInfo.playerName;
            coins.text = playerInfo.coins.ToString();

        }
    }

    public void SubmitPlayerProfileFB(string chName)
    {
        playerInfo.playerName = chName;
        playerInfo.characterName = "";

        if (isFirstInstall)
        {
            //            playerInfo.coins = 500;  //500
            Debug.Log("is First Insatall");
            //			freeCoinState = false;
            //			freeCoinTimer.gameObject.SetActive(false);
            //			freeCoinWaitTime = 0;
            //			playerInfo.freeCoinTime = DateTime.Today;
        }
        else if (playerInfo.coins < 0)
        {
            Debug.Log(" 0 0 0  0");
            playerInfo.coins = 0;
            //            playerInfo.coins = 500;
            freeCoinState = true;
            freeCoinTimer.gameObject.SetActive(true);
            freeCoinWaitTime = 7200;
            playerInfo.freeCoinTime = DateTime.Now;
        }
        else
        {
            Debug.Log("DFFDFD");
            freeCoinState = true;
            freeCoinTimer.gameObject.SetActive(true);
            //            freeCoinWaitTime = 7200;
            //			playerInfo.freeCoinTime = DateTime.Now;
        }

        //set ui information
        playerName.text = chName;
        coins.text = playerInfo.coins.ToString(); ;

        //save player information to local
        //        SavePlayerInfo();

        //        freeCoinTimer.gameObject.SetActive(false);
        //        freeCoinWaitTime = 0;
        //        playerInfo.freeCoinTime = DateTime.Today;
        playerInfo.logInWithFB = 1;
        //        SavePlayerInfo();
    }

    public void ShowInterFaceAfterFBLogin()
    {
        registerPanel.SetActive(false);
        registerPanel.GetComponent<UIRegisterManage>().backUI.SetActive(false);
    }

    public void SubmitPlayerProfile(string chName, string avatarName)
    {
        //set logic info
        playerInfo.playerName = chName;
        playerInfo.characterName = avatarName;

        if (isFirstInstall)
        {
            Debug.Log("Player first install:-");
            playerInfo.coins = 500; //500   ahiya the first time guest login the thay che
            freeCoinState = false;
            freeCoinTimer.gameObject.SetActive(false);
            freeCoinWaitTime = 0;
            playerInfo.freeCoinTime = DateTime.Today;
        }
        else if (playerInfo.coins < 0)
        {
            playerInfo.coins = 0;
            //            playerInfo.coins = 500;
            freeCoinState = true;
            freeCoinTimer.gameObject.SetActive(true);
            freeCoinWaitTime = 7200;
            playerInfo.freeCoinTime = DateTime.Now;
        }
        else
        {
            freeCoinState = true;
            freeCoinTimer.gameObject.SetActive(true);
            freeCoinWaitTime = 7200;
            playerInfo.freeCoinTime = DateTime.Now;
        }

        characterImage_facebook.gameObject.SetActive(false);

        //set ui information
        for (int i = 0; i < 8; i++)
        {
            if (i == int.Parse(avatarName))
            {
                charactersImages[i].gameObject.SetActive(true);
            }
            else
            {
                charactersImages[i].gameObject.SetActive(false);
            }
        }


        playerName.text = chName;
        coins.text = playerInfo.coins.ToString();

        //save player information to local
        SavePlayerInfo();

        //        SavePlayerInfo();
        registerPanel.SetActive(false);
        registerPanel.GetComponent<UIRegisterManage>().backUI.SetActive(false);
    }

    //add information to the playfab to show user list and coins list
    public void SetUserInformationToThePlayFabServer()
    {
        //if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        //{
        //    PlayFabSettings.TitleId = "9B0B";
        //}
        //var request = new LoginWithCustomIDRequest();
        //request.CustomId = playerInfo.playerName;
        //request.CreateAccount = true;

        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    //this function is called when the register is called and returns success code
    //private void OnLoginSuccess(LoginResult result)
    //{
    //    Debug.Log("register information called and returns success");

    //    var request = new UpdateUserDataRequest();
    //    request.Data = new Dictionary<string, string>()
    //    {
    //        {"playerName",  playerInfo.playerName},
    //        {"coins",  playerInfo.coins.ToString()}
    //    };

    //    PlayFabClientAPI.UpdateUserData(request, UpdateInformationResult, UpdateInformationError);
    //}

    //private void UpdateInformationResult(UpdateUserDataResult result)
    //{
    //    Debug.Log("update information successed");
    //}

    //private void UpdateInformationError(PlayFabError error)
    //{
    //    Debug.Log("update information failed");
    //}

    ////called when register information is called and returns failure code
    //private void OnLoginFailure(PlayFabError error)
    //{
    //    Debug.Log("register information called and returns failture code");
    //}

    public void SavePlayerInfo()
    {
        Debug.Log("Save Data Used:-");
        parsMang.SaveCoinToServer();
        //        SetUserInformationToThePlayFabServer();
        PlayerPrefs.SetInt("FBState", playerInfo.logInWithFB);
        PlayerPrefs.SetString("guestName", playerInfo.playerName);
        PlayerPrefs.SetInt("numberOFchipsKey", playerInfo.coins);
        PlayerPrefs.SetInt("tableOpenState", playerInfo.tableOpenState[0]);
        PlayerPrefs.SetInt("tableOpenState1", playerInfo.tableOpenState[1]);
        PlayerPrefs.SetInt("tableOpenState2", playerInfo.tableOpenState[2]);
        PlayerPrefs.SetInt("tableOpenState3", playerInfo.tableOpenState[3]);
        PlayerPrefs.SetInt("tableOpenState4", playerInfo.tableOpenState[4]);
        PlayerPrefs.SetInt("tableOpenState5", playerInfo.tableOpenState[5]);
        PlayerPrefs.SetInt("winCnt", playerInfo.winCount);
        PlayerPrefs.SetInt("loseCnt", playerInfo.matchLoseCount);
        PlayerPrefs.SetInt("totalCnt", playerInfo.totalMatchCount);
        PlayerPrefs.SetInt("rank", playerInfo.rank);
        PlayerPrefs.SetInt("highestRank", playerInfo.highestRank);
        PlayerPrefs.SetString("characterId", playerInfo.characterName);
        PlayerPrefs.SetString("freeCoin", playerInfo.freeCoinTime.ToString());
        PlayerPrefs.SetInt("adState", playerInfo.adsState);

        PlayerPrefs.SetInt("isFirstWin", Convert.ToInt32(playerInfo.isFirstWin));
        PlayerPrefs.SetInt("semi", Convert.ToInt32(playerInfo.achievement_semi));
        PlayerPrefs.SetInt("pro", Convert.ToInt32(playerInfo.achievement_pro));
        PlayerPrefs.SetInt("veteran", Convert.ToInt32(playerInfo.achievement_veteran));
        PlayerPrefs.SetInt("expert", Convert.ToInt32(playerInfo.achievement_expert));

        PlayerPrefs.SetInt("google_Login", Convert.ToInt32(playerInfo.isGoogleLogin));
        PlayerPrefs.Save();
    }


    //player gets free coins from the game and starts new counter for free coins
    public void PlusFreeCoins()
    {
        DateTime curTime = DateTime.Now;
        Debug.Log("Curr:-" + curTime.Ticks + "freecointicks:-" + playerInfo.freeCoinTime.Ticks);
        int delta = (int)((curTime.Ticks - playerInfo.freeCoinTime.Ticks) / 10000000);
        Debug.Log(delta + "delta plusfreecoins" + freeCoinTimer.gameObject.activeSelf);
        if (delta > 3600 * 2 || !freeCoinTimer.gameObject.activeSelf)
        {
            playerInfo.coins += 200;
            coins.text = playerInfo.coins.ToString();
            playerInfo.freeCoinTime = curTime;
            SavePlayerInfo();
            freeCoinState = true;
            freeCoinWaitTime = 7200;
            freeCoinTimer.gameObject.SetActive(true);
        }
        else
        {
            //	freeCoinTimer.gameObject.SetActive (false);
        }
        // GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), delta+" timer state "+ freeCoinTimer.gameObject.activeSelf);
    }

    //player submit the changed profile setting when the player changed the profile
    public void PlayerProfileChanged(string name, string characterName)
    {
        if (name == "")
        {

        }
        else
        {

        }

        if (characterName == playerInfo.characterName)
        {

        }
        else
        {

        }
    }

    //player wants to change the character image
    public void CharacterImageChanged(string chName)
    {

    }

    public Image opponentImage;

    //player with another player on the local area
    public void PlayerWithAnotherPlayer()
    {
        opponentImage.sprite = charactersImages[1].sprite;
        UnityEngine.Debug.Log("play game called");
        gameUI.GetComponent<UIGamePlayManager>().oppoName.text = "Opponent";
        timmerObject.SetActive(false);
        this.gameObject.GetComponent<Engine>().StartGame(Match_Type.MT_PvsP, 0);
    }

    //player is playing game with AiBot
    public void PlayWithAIBot(int dif)
    {
        Engine.isconnectWithBot = false;
        opponentImage.sprite = charactersImages[1].sprite;
        gameUI.GetComponent<UIGamePlayManager>().oppoName.text = "CPU";
        timmerObject.SetActive(false);
        this.gameObject.GetComponent<Engine>().SetAIBOTDifficulty(dif);
        this.gameObject.GetComponent<Engine>().StartGame(Match_Type.MT_PvsCPU, 0);
    }

    //player buy item from the shop
    public void BuyItemFromShop(int itemId)
    {

    }

    //player changed the language setting
    public void ChangeLanguageSetting(string langName)
    {

    }

    //player changed the music setting
    public void SetMusicsetting(bool musicSetting)
    {

    }

    public void SetPlayingGameState(int tableId)
    {
        interfaceUI.SetActive(false);
        interback.SetActive(false);

        gameUI.SetActive(true);
        gameBack.SetActive(true);
        chat.SetActive(true);
        emotion.SetActive(true);

        gameResult.SetActive(false);

        gameUI.GetComponent<UIGamePlayManager>().SetWaitingState();
    }

    //couldn't connect the cloud network and disable loading ui
    public void CouldConnectionFailed()
    {
        interfaceUI.transform.Find("LoadingAnimation").gameObject.SetActive(false);
    }

    public void CreateFBRoom()
    {
        Debug.Log("FB room callFBshowFrnd");
        StartCoroutine("callFBshowFrnd");
    }

    IEnumerator callFBshowFrnd()
    {
        Debug.Log("FB room callFBshowFrnd called");
        yield return new WaitForSeconds(0.0f);
        int tbId = PlayerPrefs.GetInt("tbid", 0);
        Debug.Log("FB room callFBshowFrnd called 1");
        this.GetComponent<UIFaceBookManager>().facebookPlay.GetComponent<UIFaceBookPlay>().ShowPlayerList(tbId);
    }

    public void SetRoomCode()
    {
        this.gameObject.GetComponent<NetworkingManager>().CreatePrivateRoom();
    }

    //select table to play Game on online mode
    public void TableSelected(int tableId)
    {
        if (this.gameObject.GetComponent<Engine>().coinsArray[tableId] > playerInfo.coins)
        {
            return;
        }

        opponentImage.sprite = charactersImages[1].sprite;
        this.gameObject.GetComponent<Engine>().PlayGameOnline(tableId, playerInfo.playerName);
    }

    public void TableSelectedByCode(int tableId)
    {
        if (this.gameObject.GetComponent<Engine>().coinsArray[tableId] > playerInfo.coins)
        {
            return;
        }

        opponentImage.sprite = charactersImages[1].sprite;
        this.gameObject.GetComponent<Engine>().PlayGameByCode(tableId, playerInfo.playerName, RoomCodeInputField.text);
    }

    public IEnumerator WaitSomeTimeToDisplayResult(int state, int myCnt, int otherCnt)
    {
        yield return new WaitForSeconds(4.5f);
        interfaceUI.SetActive(false);
        interback.SetActive(true);

        gameUI.SetActive(false);
        gameBack.SetActive(false);
        chat.SetActive(false);
        emotion.SetActive(false);

        gameResult.SetActive(true);

        gameResult.GetComponent<UIGameResult>().SetGameState(state, myCnt, otherCnt);
        InitUIPlayerInfo();
    }

    //display result of game that shows you are lose or win ./..
    public void DisplayWinningState(int state, int myCnt, int otherCnt)
    {
        this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();

        if (!gameUI.activeSelf)
        {
            return;
        }

        StartCoroutine(WaitSomeTimeToDisplayResult(state, myCnt, otherCnt));
        switch (state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    //player changed the background music setting
    public void SetBackMusicSetting(bool bgMusicSetting)
    {

    }

    public void PlayRandomWithNetWorkPlayer(int tableId)
    {

    }

    public void ReceiveChatMessage(string chText)
    {
        gameUI.GetComponent<UIGamePlayManager>().OtherChat(chText);
    }

    public void ReceiveEmotionMessage(int id)
    {
        emotion.GetComponent<UIEmotionManage>().ReceiveEmotion(id);
    }
    public void BuyUnlockedTableWithCoins(int money, int tableId)
    {
        playerInfo.tableOpenState[tableId] = 1;
        playerInfo.coins -= money;
        SavePlayerInfo();
    }

    public void RemoveAdsWithMoney()
    {
        playerInfo.adsState = 0;
        PlayerPrefs.SetInt("adState", 0);
        adButtonDesk.SetActive(false);
        adButtonShop.SetActive(false);
        CoinImage.SetActive(false);
        BGImage.SetActive(false);
        TextObject.SetActive(false);
        GoogleAd.enabled = false;
        //		playerInfo.coins += 300;
        //        SavePlayerInfo();
    }

    public GameObject selBoard1, selBoard2;

    public void UnlockTableWithMoney(int tableId)
    {
        if (selBoard1.activeSelf)
        {
            selBoard1.GetComponent<UISelectBoardManager>().tableObjects[tableId].GetComponent<UITableSelectionObject>().UnLockTable();
        }
        else if (selBoard2.activeSelf)
        {
            selBoard2.GetComponent<UISelectBoardManager>().tableObjects[tableId].GetComponent<UITableSelectionObject>().UnLockTable();
        }
        playerInfo.tableOpenState[tableId] = 1;
        SavePlayerInfo();
    }

    public void SetNormalCharacterImage()
    {
        playerInfo.characterId = "0";
        playerInfo.logInWithFB = 0;
        SavePlayerInfo();
        characterImage_facebook.gameObject.SetActive(false);

        //set ui information
        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
            {
                charactersImages[i].gameObject.SetActive(true);
            }
            else
            {
                charactersImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetWhoesMoveToUI(int state)
    {
        if (state == 1)
        {
            gameUI.GetComponent<UIGamePlayManager>().SetMyTurn();
        }
        else
        {
            gameUI.GetComponent<UIGamePlayManager>().SetOtherTurn();
        }
    }

    public void SetOpponentInformation(string playerName)
    {
        interfaceUI.SetActive(false);
        interback.SetActive(false);

        gameUI.SetActive(true);
        gameBack.SetActive(true);
        chat.SetActive(true);
        emotion.SetActive(true);

        gameResult.SetActive(false);
        if (playerName != "")
        {
            gameUI.GetComponent<UIGamePlayManager>().oppoName.text = playerName;
        }

        gameUI.GetComponent<UIGamePlayManager>().PrepareStartGame();

        StartTimerCount();
    }

    public IEnumerator WaitToReturnHome()
    {
        this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();
        gameUI.transform.Find("WaitingState").gameObject.GetComponent<Text>().text = "Opponent is not online.";

        yield return new WaitForSeconds(2);

        interfaceUI.SetActive(true);
        interback.SetActive(true);
        playTypeBtnGrp.SetActive(true);
        leftSettingUI.SetActive(true);

        privateCodeUI.SetActive(false);
        gameUI.SetActive(false);
        gameBack.SetActive(false);
        chat.SetActive(false);
        emotion.SetActive(false);

        gameResult.SetActive(false);
        gameUI.transform.Find("WaitingState").gameObject.GetComponent<Text>().text = "Waiting for Opponent";

        if (selBoard1.activeSelf)
        {
            selBoard2.SetActive(false);
        }


        InitUIPlayerInfo();
    }

    public void ReturnOnFBCancel()
    {

        if (settings.activeSelf)
        {
            return;
        }


        interfaceUI.SetActive(true);
        interback.SetActive(true);
        playTypeBtnGrp.SetActive(true);
        interfaceUI.transform.Find("StaticUI").gameObject.SetActive(true);
        interfaceUI.transform.Find("leftSetting").gameObject.SetActive(true);
        interfaceUI.transform.Find("SettingButtonGroup").gameObject.SetActive(true);

        privateCodeUI.SetActive(false);
        gameUI.SetActive(false);
        gameBack.SetActive(false);
        chat.SetActive(false);
        emotion.SetActive(false);

        gameResult.SetActive(false);

        if (selBoard1.activeSelf)
        {
            selBoard2.SetActive(false);
        }


        InitUIPlayerInfo();
    }

    public void OpponentRejectRequest()
    {
        if (uiLoadingAnim.activeSelf)
        {
            uiLoadingAnim.SetActive(false);
        }
        if (gameUI.activeSelf)
        {
            ReturnToHome();
        }
        if (gameResult.activeSelf)
        {
            ReturnToHome();
        }
    }

    public void ReturnToHome()
    {
        StartCoroutine(WaitToReturnHome());
    }


    //count timmer when you are playing on online mode
    public void StartTimerCount()
    {
        timmerObject.SetActive(true);

        timmerObject.GetComponent<UITimmerController>().StartTimeCount();

    }

    public void StopCounting()
    {
        timmerObject.GetComponent<UITimmerController>().StopCounting();
    }

    //cancel all the information when the connetion time out
    public void ConnectionTimeOut()
    {
        if (!gameUI.activeSelf || interfaceUI.activeSelf)
        {
            return;
        }
        StartCoroutine(ConnectionTimeOutDelay());
    }

    public IEnumerator ConnectionTimeOutDelay()
    {
        Debug.Log("isconnecting false-----------------------------------------");
        this.gameObject.GetComponent<NetworkingManager>().isConnecting = false;
        this.gameObject.GetComponent<Engine>().isPlayingGame = false;
        this.gameObject.GetComponent<Engine>().isWaitingOtherJoinRoom = false;
        gameUI.GetComponent<UIGamePlayManager>().otherTurn.gameObject.SetActive(false);
        gameUI.GetComponent<UIGamePlayManager>().yourturn.gameObject.SetActive(false);
        gameUI.GetComponent<UIGamePlayManager>().gameStateDisp.gameObject.SetActive(false);
        gameUI.GetComponent<UIGamePlayManager>().timeOut.gameObject.SetActive(true);
        if (this.gameObject.GetComponent<Engine>().b.whoseMove() == Mancala.Position.Bottom)
        {
            gameUI.GetComponent<UIGamePlayManager>().timeOut.text = "Time Out!!!";
        }
        else
        {
            gameUI.GetComponent<UIGamePlayManager>().timeOut.text = gameUI.GetComponent<UIGamePlayManager>().oppoName.text + "Exit!!!";
        }
        yield return new WaitForSeconds(1f);

        this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();
        //Debug.Log("isconnecting false-----------------------------------------"+ Engine.gamePlayState);
        if ((Engine.gamePlayState == 3 && this.gameObject.GetComponent<Engine>().b.whoseMove() != Mancala.Position.Top) || (Engine.isconnectWithBot && this.gameObject.GetComponent<Engine>().b.whoseMove() != Mancala.Position.Top))
        {
            playTypeBtnGrp.SetActive(true);
            interfaceUI.SetActive(true);
            interback.SetActive(true);
            leftSettingUI.SetActive(true);
            gameUI.GetComponent<UIGamePlayManager>().timeOut.gameObject.SetActive(false);
            gameUI.SetActive(false);
            gameBack.SetActive(false);
            chat.SetActive(false);
            emotion.SetActive(false);
        }
        else
        {
            //Debug.Log("isconnecting false----------DisplayWinningState-------------------------------");
            playerInfo.winCount++;
            SendAchievementProgressToServer();
            SendGameWinInfoToGooglePlay();
            playerInfo.coins = this.gameObject.GetComponent<UIManager>().playerInfo.coins + (this.gameObject.GetComponent<Engine>().coinsArray[this.gameObject.GetComponent<Engine>().curSelTable] * 2);
            SavePlayerInfo();
            DisplayWinningState(1, this.gameObject.GetComponent<Engine>().b.scoreTop(), this.gameObject.GetComponent<Engine>().b.scoreBot());
        }

        InitUIPlayerInfo();
    }

    public void OpponentLeaveRoom()
    {
        if (!gameUI.activeSelf || interfaceUI.activeSelf)
        {
            return;
        }
        StartCoroutine(OpponentConnectionTimeOutDelay());
    }

    public IEnumerator OpponentConnectionTimeOutDelay()
    {
        yield return new WaitForSeconds(0f);

        if (gameUI.activeSelf)
        {
            Debug.Log("isconnecting false-----------------------------------------");
            this.gameObject.GetComponent<NetworkingManager>().isConnecting = false;
            this.gameObject.GetComponent<Engine>().isPlayingGame = false;
            this.gameObject.GetComponent<Engine>().isWaitingOtherJoinRoom = false;
            gameUI.GetComponent<UIGamePlayManager>().otherTurn.gameObject.SetActive(false);
            gameUI.GetComponent<UIGamePlayManager>().yourturn.gameObject.SetActive(false);
            gameUI.GetComponent<UIGamePlayManager>().gameStateDisp.gameObject.SetActive(false);
            gameUI.GetComponent<UIGamePlayManager>().timeOut.gameObject.SetActive(true);
            gameUI.GetComponent<UIGamePlayManager>().timeOut.text = gameUI.GetComponent<UIGamePlayManager>().oppoName.text + "Exit!!!";
            yield return new WaitForSeconds(1f);

            this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();

            playerInfo.winCount++;
            SendAchievementProgressToServer();
            SendGameWinInfoToGooglePlay();
            playerInfo.coins = this.gameObject.GetComponent<UIManager>().playerInfo.coins + (this.gameObject.GetComponent<Engine>().coinsArray[this.gameObject.GetComponent<Engine>().curSelTable] * 2);
            SavePlayerInfo();
            DisplayWinningState(1, this.gameObject.GetComponent<Engine>().b.scoreTop(), this.gameObject.GetComponent<Engine>().b.scoreBot());

            InitUIPlayerInfo();
        }
    }

    public GameObject wi_fiObject;
    public void ShowWiFiAnimation()
    {
        wi_fiObject.SetActive(true);
        wi_fiObject.GetComponent<Animator>().Play("PlayAnim");
        StartCoroutine(StopAnim());
    }

    public IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(0.8f);
        wi_fiObject.SetActive(false);

        interfaceUI.SetActive(true);
        interback.SetActive(true);

        gameUI.SetActive(false);
        gameBack.SetActive(false);
        chat.SetActive(false);
        emotion.SetActive(false);

        gameResult.SetActive(false);
        gameUI.transform.Find("WaitingState").gameObject.GetComponent<Text>().text = "Waiting for Opponent";
        InitUIPlayerInfo();
    }

    public void BuyCoinsResult(int id)
    {
        Debug.Log(id.ToString());

        //Kochava.Event myEvent = new Kochava.Event(Kochava.EventType.Purchase);
        switch (id)
        {

            case 1:
                playerInfo.coins += 3000;
                //myEvent.name = "3000 coin";
                //myEvent.price = 130.00;
                //Kochava.Tracker.SendEvent(myEvent);
                break;
            case 2:
                playerInfo.coins += 10000;
                //myEvent.name = "10000 coin";
                //myEvent.price = 350.00;
                //Kochava.Tracker.SendEvent(myEvent);
                break;
            case 3:
                playerInfo.coins += 30000;
                //myEvent.name = "30000 coin";
                //myEvent.price = 1000.00;
                //Kochava.Tracker.SendEvent(myEvent);
                break;
            case 4:
                playerInfo.coins += 100000;
                //myEvent.name = "100000 coin";
                //myEvent.price = 3250.00;
                //Kochava.Tracker.SendEvent(myEvent);
                break;
            case 5:
                this.gameObject.GetComponent<UIManager>().RemoveAdsWithMoney();
                //myEvent.name = "Remove ads";
                //myEvent.price = 350.00;
                //Kochava.Tracker.SendEvent(myEvent);
                break;
            case 6:
                this.gameObject.GetComponent<UIManager>().UnlockTableWithMoney(4);
                //myEvent.name = "Table 5";
                //myEvent.price = 250.00;
                //Kochava.Tracker.SendEvent(myEvent);
                break;
            case 7:
                this.gameObject.GetComponent<UIManager>().UnlockTableWithMoney(5);
                //myEvent.name = "Table 6";
                //myEvent.price = 400.00;
                //Kochava.Tracker.SendEvent(myEvent);

                break;
            default:
                break;
        }
        coins.text = playerInfo.coins.ToString();
        SavePlayerInfo();
    }

    public void FacebookLikeMethod()
    {
        FLCoinImage.SetActive(false);
        FLBGImage.SetActive(false);
        FLTextObject.SetActive(false);
        FacebookLike.SetActive(false);
        //playerInfo.coins += 300;
        //SavePlayerInfo();
        PlayerPrefs.SetInt("FacebookLikeUsed", 10);
        Application.OpenURL("https://www.facebook.com/watermelonlab");

    }

    public void FacebookLikeMainMenu()
    {
        Application.OpenURL("https://www.facebook.com/watermelonlab");
    }


}
