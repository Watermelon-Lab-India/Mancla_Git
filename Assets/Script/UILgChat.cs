using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILgChat : MonoBehaviour
{
    public string[] goodLuck, thanks, goodGame, wellPlayed, wow, opps;

    public Text tgoodLuck, tThanks, tGoodGame, tWellPlayed, tWow, tOpps;

    int selLanguage = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            tgoodLuck.text = goodLuck[selLanguage];
            tThanks.text = thanks[selLanguage];
            tGoodGame.text = goodGame[selLanguage];
            tWellPlayed.text = wellPlayed[selLanguage];
            tWow.text = wow[selLanguage];
            tOpps.text = opps[selLanguage];
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            tgoodLuck.text = goodLuck[selLanguage];
            tThanks.text = thanks[selLanguage];
            tGoodGame.text = goodGame[selLanguage];
            tWellPlayed.text = wellPlayed[selLanguage];
            tWow.text = wow[selLanguage];
            tOpps.text = opps[selLanguage];
        }
    }
}
