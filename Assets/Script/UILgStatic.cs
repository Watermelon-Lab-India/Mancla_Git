using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgStatic : MonoBehaviour {

    public Text profile, freeCoins;
    public string[] profileLg, freeCoinsLg;
    int curseLanguage = 0;

    public GameObject engine;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
            if (PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
            {
                curseLanguage = PlayerPrefs.GetInt("LState");
                //profile.text = profileLg[curseLanguage];
                freeCoins.text = freeCoinsLg[curseLanguage];
            }
            else
            {
                curseLanguage = PlayerPrefs.GetInt("LState");
              //  profile.text = profileLg[curseLanguage];
                freeCoins.text = freeCoinsLg[curseLanguage];
            }
	}
}
