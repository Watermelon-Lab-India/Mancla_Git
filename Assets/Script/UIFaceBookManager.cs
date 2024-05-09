using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System;
using UnityEngine.Networking;

public class UIFaceBookManager : MonoBehaviour
{
    public GameObject engine;
    public GameObject facebookPlay;

    public static bool isFacebookLogin = false;

    public bool isFirstLogin = true;
    public Text profileName;
    public Image profilePicture;

    public List<string> fList = new List<string>();
    public List<string> fIdList = new List<string>();

    public List<string> fOnlineUser = new List<string>();

    public string chPlayerName;
    public string chPlayerId;

    // Use this for initialization
    void Start()
    {

    }

    public string getFriendIdFromName(string name)
    {
        for (int i = 0; i < fList.Count; i++)
        {
            if (fList[i].Contains(name))
            {
                return fIdList[i];
            }
        }
        return "empty";
    }

    public void FaceBookLogin()
    {
        if (!FB.IsInitialized)
        {
            Debug.Log("FB Login IF ");
            FB.Init(onInitComplete, onHideUnity);
        }
        else
        {
            Debug.Log("FB Login ELSE ");
            onInitComplete();
        }
    }

    public void FaceBookLogOut()
    {
        isFacebookLogin = false;
        this.gameObject.GetComponent<UIManager>().SetNormalCharacterImage();
    }

    private void onInitComplete()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("[FB] [onInitComplete] ..... OK!");

            FB.ActivateApp();

            if (FB.IsLoggedIn)
            {
                Debug.Log("FB Login 1");
                onLoginSuccess(null);
            }
            else
            {
                Debug.Log("FB try Login 1");
                TryCallFBLogin();
            }
        }
        else
        {
            Debug.Log("[FB] [onInitComplete] ..... ERROR!");
        }
    }

    private void onHideUnity(bool isGameShown)
    {
        Debug.Log("[FB] [onHideUnity] Is Game Showing? " + isGameShown);
    }

    private void TryCallFBLogin()
    {
        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, this.onLoginSuccess);
        //FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email" }, this.onLoginSuccess);
    }

    private void TryCallFBLoginForPublish()
    {
        // It is generally good behavior to split asking for read and publish
        // permissions rather than ask for them all at once.
        //
        // In your own game, consider postponing this call until the moment
        // you actually need it.
        FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, this.HandleResult);
    }

    public GameObject settingButtonGroup;

    protected void onLoginSuccess(IResult result)
    {
        Debug.Log("FB login sucess : "+result);
        if(result!=null)
            {
            Debug.Log("FB login sucess : " + result.Cancelled + "====" + FB.IsLoggedIn);
        }
        if (FB.IsLoggedIn || (!result.Cancelled))
        {
            Debug.Log("[FB] [onLoginSuccess] ..... OK!" + result);

            isFacebookLogin = true;
            PlayerPrefs.SetInt("FBState", 1);

            //		int coin = getServerCoinFromParse(fbid);
            //		Debug.Log("Coin:-"+coin);

            if (settingButtonGroup.activeSelf)
            {
                settingButtonGroup.GetComponent<UISettingManage>().FacebookLoginSuccess();
            }

            if (isFirstLogin)
            {
                Debug.Log("FB login sucess : isFirstLogin");
                engine.GetComponent<UIManager>().uiLoadingAnim.SetActive(true);

                FB.API("me?fields=first_name", HttpMethod.GET, onGetProfileName);
                FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, onGetProfilePicture);

                //FB.GetAppLink(onDeepLinkCallback);

                //            StartCoroutine(ReceiveRequestMsg(3));
                //            StartCoroutine(SendOnlineState(10));

            }
            else
            {
                Debug.Log("FB login sucess : else");
                if (playerFBId == "" || playerFBId == null)
                {
                    FB.API("me?fields=first_name", HttpMethod.GET, onGetProfileName);
                    //                FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, onGetProfilePicture);
                }
                FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, onGetProfilePicture);

                FB.API("/me/friends", HttpMethod.GET, onGetFriends);
            }
        }
        else if (!FB.IsLoggedIn && result.Cancelled)
        {
            Debug.Log("exittttttttttttttttttt");
            facebookPlay.SetActive(false);
            UIManager._instance.ReturnOnFBCancel();
        }
    }

    void onDeepLinkCallback(IAppLinkResult result)
    {
        if (!String.IsNullOrEmpty(result.Url))
        {
            var index = (new Uri(result.Url)).Query.IndexOf("request_ids");
            Debug.LogError("result.Url : " + result.Url);
            Debug.LogError("result.Url Index : " + index);
            if (index != -1)
            {
                // ...have the user interact with the friend who sent the request,
                // perhaps by showing them the gift they were given, taking them
                // to their turn in the game with that friend, etc.
            }
        }
    }

    public string playerFBId, playerFBName;

    protected void onGetProfileName(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error) && result.ToString() != null)
        {
            Debug.Log("[FB] [onGetProfileName] ..... OK!");

            playerFBId = result.ResultDictionary["id"].ToString();
            Debug.Log("fb id : " + playerFBId);
            this.gameObject.GetComponent<NetworkGlobalManager>().ConnectToServerWithPlayerId(playerFBId);
            this.gameObject.GetComponent<ParseManager>().SaveFbId(playerFBId);

            //			this.gameObject.GetComponent<ParseManager>().GetCoinsFromServer();

            profileName.GetComponent<Text>().text = this.gameObject.GetComponent<UIManager>().playerInfo.playerName;
            foreach (string key in result.ResultDictionary.Keys)
            {
                // first_name : Chris
                // id : 12345678901234567

                if (key.ToString() != "first_name")
                {
                    playerFBId = result.ResultDictionary[key].ToString();
                    Debug.Log("fb id 1 : " + playerFBId);
                    //                    this.gameObject.GetComponent<NetworkingManager>().ConnectToServerWithPlayerId(playerFBId);
                }

                if (key.ToString() == "first_name")
                {
                    playerFBName = result.ResultDictionary[key].ToString();
                    this.gameObject.GetComponent<UIManager>().playerInfo.playerName = playerFBName;
                    profileName.GetComponent<Text>().text = result.ResultDictionary[key].ToString();
                    PlayerPrefs.SetString("FBName", playerFBName);
                    Debug.Log(PlayerPrefs.GetString("FBName"));
                    break;
                }
            }
        }
    }

    private void Awake()
    {
        //        StartSendStat();
    }

    protected void onGetProfilePicture(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
        {
            Debug.Log("[FB] [onGetProfilePicture] ..... OK!");

            //profilePic.GetComponent<RawImage>().texture = result.Texture;
            profilePicture.gameObject.SetActive(true);
            profilePicture.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());

            for (int i = 0; i < 8; i++)
            {
                this.gameObject.GetComponent<UIManager>().charactersImages[i].gameObject.SetActive(false);
            }

            engine.GetComponent<UIManager>().uiLoadingAnim.SetActive(false);
            engine.GetComponent<UIManager>().registerPanel.GetComponent<UIRegisterManage>().RegisterWithFaceBook();
        }

        this.HandleResult(result);
    }



    public string[] friendIdList;

    private void onGetFriends(IGraphResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("[FB] [onGetFriends] ..... OK!\n" + result.RawResult);
            fList = new List<string>();
            fIdList = new List<string>();

            facebookPlay.GetComponent<UIFaceBookPlay>().friendsIDList = new List<string>();
            facebookPlay.GetComponent<UIFaceBookPlay>().friendsNameList = new List<string>();

            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
            foreach (var dict in friendsList)
            {
                fList.Add(((Dictionary<string, object>)dict)["id"].ToString());
                fIdList.Add(((Dictionary<string, object>)dict)["id"].ToString());
                facebookPlay.GetComponent<UIFaceBookPlay>().friendsIDList.Add(((Dictionary<string, object>)dict)["id"].ToString());
                facebookPlay.GetComponent<UIFaceBookPlay>().friendsNameList.Add(((Dictionary<string, object>)dict)["name"].ToString());
            }

            facebookPlay.GetComponent<UIFaceBookPlay>().ShowPlayerList();

            friendIdList = new string[fIdList.Count];

            for (int i = 0; i < fIdList.Count; i++)
            {
                friendIdList[i] = fIdList[i];
            }
            List<string> onlineFriends = new List<string>();
            this.gameObject.GetComponent<NetworkGlobalManager>().GetFriendsList(friendIdList);


            foreach (var dict in friendsList)
            {
                FB.API("/" + ((Dictionary<string, object>)dict)["id"].ToString() + "/picture?redirect=0&type=square&height=128&width=128",
                    HttpMethod.GET, onGetFriendPicture);
            }
        }
        else
        {
            Debug.Log("[FB] [onGetFriends] ..... error\n" + result.Error);
        }
    }

    public void SetOppoProfileImage()
    {
        if (FB.IsInitialized)
        {
            FB.API("/me/friends", HttpMethod.GET, onGetOneFriend);
        }
    }

    private void onGetOneFriend(IGraphResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("[FB] [onGetFriends] ..... OK!\n" + result.RawResult);

            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
            foreach (var dict in friendsList)
            {
                string name = ((Dictionary<string, object>)dict)["name"].ToString();
                if (name.Contains(chPlayerName))
                {
                    FB.API("/" + ((Dictionary<string, object>)dict)["id"].ToString() + "/picture?redirect=0&type=square&height=128&width=128",
    HttpMethod.GET, onOneGetFriendPicture);
                }
            }
        }
    }

    private void onOneGetFriendPicture(IGraphResult result)
    {
        var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
        var dataPicture = (Dictionary<string, object>)dictionary["data"];
        var picUrl = ((Dictionary<string, object>)dataPicture)["url"].ToString();
        int nMarkID1 = picUrl.IndexOf("/?asid=") + 7;
        int nMarkID2 = picUrl.IndexOf("&height=");
        if (nMarkID1 < 0 || nMarkID2 < 0 || nMarkID1 > nMarkID2)
            return;
        string friendID = picUrl.Substring(nMarkID1, nMarkID2 - nMarkID1);

        StartCoroutine(getTextureFromUrl(picUrl));
    }

    public IEnumerator getTextureFromUrl(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("[UIFaceBookPlay::getTextureFromUrl] www.error");
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite spt = Sprite.Create(myTexture, new Rect(0, 0, 128, 128), new Vector2());
            this.gameObject.GetComponent<UIManager>().opponentImage.sprite = spt;
        }
    }

    private void onGetFriendPicture(IGraphResult result)
    {
        //Debug.Log("onGetFriendPicture = " + result.RawResult);

        var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
        var dataPicture = (Dictionary<string, object>)dictionary["data"];
        var picUrl = ((Dictionary<string, object>)dataPicture)["url"].ToString();

        int nMarkID1 = picUrl.IndexOf("/?asid=") + 7;
        int nMarkID2 = picUrl.IndexOf("&height=");
        if (nMarkID1 < 0 || nMarkID2 < 0 || nMarkID1 > nMarkID2)
            return;
        string friendID = picUrl.Substring(nMarkID1, nMarkID2 - nMarkID1);

        facebookPlay.GetComponent<UIFaceBookPlay>().setFriendPicture(friendID, picUrl);
    }

    public void GamePlayRequest(string friendID)
    {
        List<string> toUsers = new List<string>() { friendID };
        List<string> filter = new List<string>() { "app_users" };

        FB.AppRequest(
            "I want to play with you.", // message
            toUsers, // to users
            filter, // filters
            null, // excludeIds
            null, // maxRec
            "1033334", // data
            null, // title
            delegate (IAppRequestResult result1)
            {
                Debug.Log("AppRequest---------------------... OK!     " + result1.RawResult);
            }
        );
    }

    IEnumerator ReceiveRequestMsg(float delayTime)
    {
        //FB.Mobile.AppInvite(new Uri("https://play.google.com"), null, callback: this.HandleResult);
        FB.API("/me/apprequests?access_token=" + AccessToken.CurrentAccessToken.TokenString, HttpMethod.GET, onAppRequest);
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(ReceiveRequestMsg(3));
    }

    //send message to all friends that the user is online state()
    public void StartSendStat()
    {
        //        StartCoroutine(SendOnlineState(10));
    }

    IEnumerator SendOnlineState(float delayTime)
    {
        if (isFacebookLogin)
        {
            if (fList.Count > 0)
            {
                List<string> filter = new List<string>() { "app_non_users" };

                FB.AppRequest(
                    "online", // message
                    fList, // to users
                    null, // filters
                    null, // excludeIds
                    null, // maxRec
                    "1033334", // data
                    null, // title
                    delegate (IAppRequestResult result1)
                    {
                        //Debug.Log("AppRequest ... OK!     " + result1.RawResult);
                    }
                );
            }
        }
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(SendOnlineState(10));
    }


    private void onAppRequest(IGraphResult result)
    {
        Debug.Log("[onAppRequest] RawResult: " + result.RawResult);

        if (result.RawResult == null || result.RawResult.Length < 1)
            return;

        object objRetValue;

        var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
        if (!dictionary.TryGetValue("data", out objRetValue))
            return;

        var data = (List<object>)dictionary["data"];
        foreach (var dictData in data)
        {
            var dictMsg = (Dictionary<string, object>)dictData;

            string appID = "";
            string requestId = "";
            string message = "";
            string dataInfo = "";

            if (dictMsg.TryGetValue("application", out objRetValue))
            {
                var appDict = (Dictionary<string, object>)objRetValue;
                appID = ((Dictionary<string, object>)appDict)["id"].ToString();
            }
            if (appID == FB.AppId)
            {
                // msg from friend
                object objValue;
                if (dictMsg.TryGetValue("id", out objValue))
                {
                    requestId = ((Dictionary<string, object>)dictMsg)["id"].ToString();
                    if (dictMsg.TryGetValue("message", out objValue))
                    {
                        message = ((Dictionary<string, object>)dictMsg)["message"].ToString();
                        if (message == "online")
                        {
                            Debug.Log("--- arrived ---");
                        }
                        else
                        {
                            OnChallengeRequestArrived(message);
                        }

                        if (dictMsg.TryGetValue("data", out objValue))
                        {
                            dataInfo = ((Dictionary<string, object>)dictMsg)["data"].ToString();
                            Debug.Log("[onAppRequest] appID: " + appID + ", data: " + dataInfo +
                                ", message: " + message + ", requestId: " + requestId);
                        }
                    }

                    FB.API(requestId, HttpMethod.DELETE, delegate (IGraphResult result1) { Debug.LogError("Deleted RequestID!"); });
                }
            }
        }
    }

    public void OnChallengeRequestArrived(string chPlayer)
    {
        chPlayerName = chPlayer;
        this.gameObject.GetComponent<UIManager>().ShowChallengeWindow();
    }

    public void OnOnlineStateRequestArrived()
    {

    }

    protected void HandleResult(IResult result)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}





