
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;


public class FBScript : MonoBehaviour
{



	//buttons

//	public Text FriendsText;
//	public Text connectingText;

	public static FBScript Instance = null;



	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}


//		if (FB.IsLoggedIn)
//		{
//			return;
//		}
//		
//		if (!FB.IsLoggedIn)
//		{
//			FacebookLogin ();	
//		}
//		
		/*else if (FB.IsInitialized) {
			fb.SetActive (false);
			invite_Friend.SetActive (false);
			findFriend.SetActive (false);
			fb_login.SetActive (true);

		}*/

//		FB.Init (InitCallBack);
//		MyFacebookLogin ();
	}


	private void Update ()
	{
		//		Debug.Log("Initzlized : " + FB.IsInitialized);
		//		Debug.Log("Log in : " + FB.IsLoggedIn);
	}

	public void MyFacebookLogin ()
	{

		if (!FB.IsInitialized) {
			FB.Init (() => {
				if (FB.IsInitialized) {
					//PlayerPrefs.SetInt ("facebook_Login", 10);
					FB.ActivateApp ();
				} else {
					Debug.Log (" Could not Initalize");
				}
			},
				isGameShown => {
					if (!isGameShown) {
						Time.timeScale = 0;
					} else {
						Time.timeScale = 0;
					}
				}


			);
		} else {
			//PlayerPrefs.SetInt ("facebook_Login", 10);
			FB.ActivateApp ();
		}
	}


	void OnApplicationPause (bool pauseStatus)
	{
		if (Application.isEditor) {
			return;
		} else {
			if (!pauseStatus) {
				if (FB.IsInitialized) {
					FBAppEvents.LaunchEvent ();
				} else {
					FB.Init (InitCallBack);
				}
			}
		}
	}

	private void InitCallBack ()
	{



		Debug.Log ("InitCallback");
		FBAppEvents.LaunchEvent ();

		if (FB.IsLoggedIn) {
			OnLoginComplete ();

		}
	}


	void FriendsWhoPlay ()
	{
//		var scores = GameStateManager.Scores;
//		for (int i = 0; i < scores.Count; i++) {
//			GameObject LBgameObject = Instantiate (LeaderboardItemPrefab) as GameObject;
//			LeaderBoardElement LBelement = LBgameObject.GetComponent<LeaderBoardElement> ();
//			LBelement.SetupElement (i + 1, scores [i]);
//			LBelement.transform.SetParent (LeaderboardPanel.transform, false);
//		}
	}


	public void findFriendOpen ()
	{

//		bl_FriendList.Instance.blocker.SetActive (true);
//		bl_FriendList.Instance.headerColor.GetComponent <RawImage> ().color = new Color (255, 255, 255, 97);
//		sendRequest.SetActive (true);

	}


	#region login / logout

	public void FacebookLogin ()
	{
		//var permisssion = new List<string> (){ "public_profile", "email", "user_friends" };
		//FB.LogInWithReadPermissions (permisssion);

		FBLogin.PromptForLogin (OnLoginComplete);
	}

	void OnLoginComplete ()
	{
		if (!FB.IsLoggedIn) 
		{
			return;
		} else {

//			fb.SetActive (true);
//			invite_Friend.SetActive (true);
//			findFriend.SetActive (true);
//			fb_login.SetActive (false);
//
//			FBGraph.GetPlayerInfo ();
//			FBGraph.GetFriends ();
//			FBGraph.GetInvitableFriends ();
//			FBGraph.GetScores ();
			FriendsWhoPlay ();
		}
	}

	public void FacebookLogout ()
	{
		FB.LogOut ();
	}

	#endregion

	#region share

	public void FacebookShare ()
	{
		Debug.Log("Shhh");
		FB.ShareLink (new System.Uri ("https://play.google.com/store/apps/details?id=com.water.mancalabestboardgame"), "facebook demo", "facebook demo", 
			new System.Uri ("https://lh3.googleusercontent.com/ufKF6g-lnwYNGbyAQd7Dq_L1Uiioxux3BA8OwGgwDUUcb5vkJZYttgbErS3HgGuu6VZc=w300-rw"));

		//		FB.FeedShare (string.Empty, new System.Uri ("https://play.google.com/store/apps/details?id=com.idivinecreation.ClashofModernForceOnline&hl=en"), "ClashofModernForceOnline", "MultiPlayerGame", "Check Out This Game",
		//			new System.Uri ("https://lh3.googleusercontent.com/pT8a-PepVk_2dx6FsjaA5CqQJW-tg-CK69yDppbJKBexJDrI80V90m3qKUtDcoRJkoIO=w300-rw"),
		//			string.Empty, ShareCallBack);
	}

	void ShareCallBack (IShareResult result)
	{
		if (result.Cancelled) {
			Debug.Log ("share Cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Error on share!");
		} else if (!string.IsNullOrEmpty (result.RawResult)) {
			Debug.Log ("Success on share");
		}
	}

	#endregion

	#region inviting

	public void FacebookGameRequest ()
	{
		FB.AppRequest ("Heyy ! come and play  this awesome game!", title: "fcebook demo");
	}

	public void FacebookInvite ()
	{
		Debug.Log ("fff");
		//FB.Mobile.AppInvite (new System.Uri ("https://play.google.com/store/apps/details?id=com.idivinecreation.ClashofModernForceOnline&hl=en"));

//		FB.Mobile.AppInvite (new System.Uri ("https://play.google.com/store/apps/details?id=com.math.kidsgame.count.brain"),
//			new System.Uri ("https://lh3.googleusercontent.com/ufKF6g-lnwYNGbyAQd7Dq_L1Uiioxux3BA8OwGgwDUUcb5vkJZYttgbErS3HgGuu6VZc=w300-rw"), InviteCallBack);

//		FB.Mobile.AppInvite(new System.Uri("https://fb.me/434759107045250"),callback:InviteCallBack);


        

		//FB.Mobile.AppInvite(new System.Uri("https://play.google.com/store/apps/details?id=com.car.race.extreme.track.simulator.impossible"));
	}

	//void InviteCallBack (IAppInviteResult result)
	//{
	//	if (result.Cancelled) {
	//		Debug.Log ("Invite Cancelled");
	//	} else if (!string.IsNullOrEmpty (result.Error)) {
	//		Debug.Log ("Error on Invite!");
	//	} else if (!string.IsNullOrEmpty (result.RawResult)) {
	//		Debug.Log ("Success on Invite");
	//	}
	//}

	public void GetFriendsPlayingThisGame ()
	{
		//string query = "/me/friends";
		string query = "/me/friends?fields=friends.limit(10){first_name}";
		FB.API (query, HttpMethod.GET, result =>
		{


			var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize (result.RawResult);
			var friendList = (List<object>)dictionary ["data"];
//			FriendsText.text = string.Empty;

			foreach (var dict in friendList) 
			{
//					FriendsText.text = friendList.Count.ToString ();
//				FriendsText.text += ((Dictionary<string, object>)dict) ["name"] + " " + ((Dictionary<string, object>)dict) ["id"];
			}
		});
	}

	#endregion


	//public void FbAppInvite (string message)
	//{
		
	//	FB.Mobile.AppInvite (
	//		new System.Uri ("https://mycustomurl"),
	//		new System.Uri ("https://mycustomimage.png"),
	//		(IAppInviteResult result) => {
	//			if (string.IsNullOrEmpty (result.Error)
	//			          && !result.Cancelled) {
	//				object o;
	//				if (result.ResultDictionary != null
	//				          && result.ResultDictionary.TryGetValue ("did_complete", out o)) {
	//					if (o.Equals (true)) {
	//						// Success
	//					}
	//				}
	//			}
	//		});
	//}


}
