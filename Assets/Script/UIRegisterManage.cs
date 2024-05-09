using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System;
#if UNITY_IOS
using UnityEngine.SignInWithApple;
using UnityEngine.iOS;
#endif
public class UIRegisterManage : MonoBehaviour
{
    public GameObject engine;
    public GameObject backUI, loadingAnim, AppleSignInButton,GuestUI;
    
    // Use this for initialization
    void Start()
    {
        #if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            AppleSignInButton.SetActive(true);

            Version currentVersion = new Version(Device.systemVersion); // Parse the version of the current OS
            Version ios13 = new Version("13.0"); // Parse the iOS 13.0 version constant
            gameObject.AddComponent<SignInWithApple>();
            gameObject.SetActive(true);
            if (currentVersion >= ios13)
            {
                AppleSignInButton.SetActive(true);
                // Enable the button...
            }
            else
            {
                AppleSignInButton.SetActive(false);
            }
        }
        else
        {
            AppleSignInButton.SetActive(false);
        }
        #endif
    }

    public void GuestType()
    {

    }
	public void AppleLogin()
    {
        #if UNITY_IOS
        var siwa = gameObject.GetComponent<SignInWithApple>();
        siwa.Login(OnLogin);
#endif
    }
    #if UNITY_IOS
    private void OnLogin(SignInWithApple.CallbackArgs args)
    {
        Debug.Log("Sign in with Apple login has completed.");
        GuestUI.SetActive(true);
    }
#endif
    private void OnDisable()
        {
        loadingAnim.SetActive(false);
        }

    public void FaceBookLogin()
    {
        //Kochava.Event myEvent = new Kochava.Event(Kochava.EventType.RegistrationComplete);
        //myEvent.name = "Login Via Facebook";
        //Kochava.Tracker.SendEvent(myEvent);

        Firebase.Analytics.FirebaseAnalytics.LogEvent(
          Firebase.Analytics.FirebaseAnalytics.EventLogin,
          new Firebase.Analytics.Parameter[] {
            new Firebase.Analytics.Parameter(
              Firebase.Analytics.FirebaseAnalytics.ParameterMethod, "Facebook"),
          }
        );
        engine.GetComponent<UIFaceBookManager>().isFirstLogin = true;
        loadingAnim.SetActive(true);
        engine.GetComponent<UIFaceBookManager>().FaceBookLogin();
    }
    
    public void RegisterUser(string userName, string avatarName)
    {
        engine.GetComponent<UIManager>().playerInfo.coins = -1000;
        engine.GetComponent<UIManager>().SubmitPlayerProfile(userName, avatarName);
        PlayerPrefs.SetString(Constants.KEY_TUTORIAL, "false");
    }

    public void RegisterWithFaceBook()
    {
        loadingAnim.SetActive(false);
        string profileName = engine.GetComponent<UIFaceBookManager>().profileName.GetComponent<Text>().text;
        engine.GetComponent<UIManager>().SubmitPlayerProfileFB(profileName);
        engine.GetComponent<UIManager>().ShowInterFaceAfterFBLogin();
        PlayerPrefs.SetString(Constants.KEY_TUTORIAL, "false");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Awake()
    {
        backUI.SetActive(true);
    }
}
