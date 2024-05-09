using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILgPlayType : MonoBehaviour {

    public Image privatePlay, randomPlay, fbPlay;
    public Sprite[] sPrivatePlay, sRandomPlay, sFbplay;

    int selLanguage = 0;

    private void OnEnable()
    {
        Debug.Log("Enable");

        //GoogleMobileAdsDemoScript.Instance.RequestBanner(0);
        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsShow();
        UIManager._instance.backButton3.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            privatePlay.sprite = sPrivatePlay[selLanguage];
            privatePlay.SetNativeSize();
            randomPlay.sprite = sRandomPlay[selLanguage];
            randomPlay.SetNativeSize();
            fbPlay.sprite = sFbplay[selLanguage];
            fbPlay.SetNativeSize();
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            privatePlay.sprite = sPrivatePlay[selLanguage];
            privatePlay.SetNativeSize();
            randomPlay.sprite = sRandomPlay[selLanguage];
            randomPlay.SetNativeSize();
            fbPlay.sprite = sFbplay[selLanguage];
            fbPlay.SetNativeSize();
        }
	}

    public void OnDisable()
    {
        if(GoogleMobileAdsDemoScript.Instance)
        {
         //GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
        }
    }
}
