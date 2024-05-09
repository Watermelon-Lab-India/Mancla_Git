using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgShop : MonoBehaviour {

    public Text moreGames, watchVideo, linkUs, restore;

    public string[] sMoreGames, sWatchVideo, sLinkUs, sRestore;
    int selLanguage = 0;

    public ScrollRect scroll;
	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        Vector2 pos = scroll.normalizedPosition;

        pos.y = 1;

        scroll.normalizedPosition = pos;

        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
    }

    // Update is called once per frame
    void Update ()
    {
		if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            moreGames.text = sMoreGames[selLanguage];
            watchVideo.text = sWatchVideo[selLanguage];
            //            linkUs.text = sLinkUs[selLanguage];

            restore.text = sRestore[selLanguage];
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            moreGames.text = sMoreGames[selLanguage];
            watchVideo.text = sWatchVideo[selLanguage];
            //            linkUs.text = sLinkUs[selLanguage];

            restore.text = sRestore[selLanguage];
        }
	}
}
