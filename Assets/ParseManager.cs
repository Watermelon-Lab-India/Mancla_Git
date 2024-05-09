using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parse;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class ParseManager : MonoBehaviour {

	public UIManager uimanager;
	public UIFaceBookManager uifacebookmanager;
	// Use this for initialization
	void Start () {
        //var installation = ParseInstallation.CurrentInstallation;
        //installation.Channels = new List<string> { "Giants" };
        //installation.SaveAsync();

//		GetCoinsFromServer();
//		SaveCoinToServer();
		uimanager = this.gameObject.GetComponent<UIManager>();
		uifacebookmanager = this.gameObject.GetComponent<UIFaceBookManager>();
        
    }

    private void Awake()
    {
#if UNITY_IOS
   //UnityEngine.iOS.NotificationServices.RegisterForRemoteNotificationTypes(NotificationType.Alert |
   //                                                         NotificationType.Badge |
   //                                                         NotificationType.Sound);
#endif

        ParsePush.ParsePushNotificationReceived += (sender, args) => 
		{
			
            Debug.Log("message arrived");
#if UNITY_ANDROID
            AndroidJavaClass parseUnityHelper = new AndroidJavaClass("com.parse.ParsePushUnityHelper");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            // Call default behavior.
            parseUnityHelper.CallStatic("handleParsePushNotificationReceived", currentActivity, args.StringPayload);
#elif UNITY_IOS
      IDictionary<string, object> payload = args.Payload;

      foreach (var key in payload.Keys) 
		{
        Debug.Log("Payload: " + key + ": " + payload[key]);
      }
#endif
        };
    }

    public void SendPushNotification()
    {
        IDictionary<string, object> paramsParse = new Dictionary<string, object> { { "message", "hi" }, { "recipientId", "106585480344390" } };

        ParseCloud.CallFunctionAsync<object>("sendPushToUser", paramsParse).ContinueWith(t =>
        {
				
            Debug.Log(t.Result.GetType().ToString());

        });


	}
    public void DeleteObjectFromParse()
    {
        Debug.Log("DeleteObjectFromParse  start");
        IDictionary<string, object> paramsParse = new Dictionary<string, object> {{ "appVersion", "4.10" } };

        ParseCloud.CallFunctionAsync<object>("destroyObj", paramsParse).ContinueWith(t =>
        {
            Debug.Log("dd:-" + t.Result.ToString());

        });

        Debug.Log("DeleteObjectFromParse  end");
    }
    public void SaveCoinToServer()
    {
//		Debug.Log("Coin Save Server:-"+UIFaceBookManager.isFacebookLogin);
		if(UIFaceBookManager.isFacebookLogin)
		{
//			Debug.Log("Login:-");
			int curCoins = uimanager.playerInfo.coins;
			string fbId = uifacebookmanager.playerFBId;

	        IDictionary<string, object> paramsParse = new Dictionary<string, object> { { "coins", curCoins }, { "recipientId", fbId } };



	        ParseCloud.CallFunctionAsync<object>("saveCoinsOnParse", paramsParse).ContinueWith(t =>
	        {
	            Debug.Log("dd:-"+t.Result.ToString());
				
	            if (Convert.ToString(t.Result) == "0 objects are saved!")
	            {
	                
	            }
	        });
		}
    }

    public void SaveFbId(string fbID)
    {
		Debug.Log("Save Fb Id:-");
        ParseInstallation.CurrentInstallation["fbID"] = fbID;
        ParseInstallation.CurrentInstallation.SaveAsync().ContinueWith(t1 =>
        {
				Debug.Log("Fb Belove");
            if (!t1.IsFaulted)
            {
					Debug.Log("Get coin:-");
                GetCoinsFromServer();
            }
        });
    }

    public void GetCoinsFromServer()
    {
		Debug.Log("get coin under"+uimanager.playerInfo.coins);
		int curCoins = uimanager.playerInfo.coins;
		Debug.Log("play fab id");
		string fbId = uifacebookmanager.playerFBId;
		Debug.Log("dic");
        IDictionary<string, object> paramsParse = new Dictionary<string, object> { { "recipientId", fbId} };
		Debug.Log("fbid:-"+fbId);
        ParseCloud.CallFunctionAsync<object>("getCoinsFromParse", paramsParse).ContinueWith(t =>
        {
            Debug.Log(t.Result.ToString());

            if (!t.IsFaulted)
            {
					Debug.Log("ff");
					
                if(Convert.ToInt32(t.Result) == 0 && curCoins != 0)
                {
						Debug.Log("Fault Not");
                    SaveCoinToServer();

                }
                else
                {
						Debug.Log("Ft else");
                    SaveInfo(Convert.ToInt32(t.Result));

                }

            }
        });



	}

    public void SaveInfo(int coinsInfo)
    {
		Debug.Log("Player info:-"+uimanager.playerInfo.coins);
		Debug.Log("coins info:-"+coinsInfo);

		if(coinsInfo >= uimanager.playerInfo.coins)
		{
			Debug.Log("ServerCoin:-");
			uimanager.playerInfo.coins = coinsInfo;
		}
		else
		{
			uimanager.playerInfo.coins = uimanager.playerInfo.coins;
//			uimanager.playerInfo.coins;
			Debug.Log("Device Coin:-");

		}

		uimanager.SavePlayerInfo();



    }

    // Update is called once per frame
    void Update () {
		
	}
}
