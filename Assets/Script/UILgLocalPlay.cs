using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgLocalPlay : MonoBehaviour {

    public Image privatePlay, vsCpu;
    public Sprite[] sPrivatePlay, sVsCpu;
    int selLanguage = 0;

    private void OnEnable()
    {
        //GoogleMobileAdsDemoScript.Instance.RequestBanner(0);
        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsShow();
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
            vsCpu.sprite = sVsCpu[selLanguage];
            vsCpu.SetNativeSize();
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            privatePlay.sprite = sPrivatePlay[selLanguage];
            privatePlay.SetNativeSize();
            vsCpu.sprite = sVsCpu[selLanguage];
            vsCpu.SetNativeSize();
        }
	}

    private void OnDisable()
    {
        Debug.Log("Disable");
       // GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
    }

    public void DestroyBannerOnBackPressed()
    {
        if(gameObject.activeSelf)
        {
     //    GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
        }
    }
}
