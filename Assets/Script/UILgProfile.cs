using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgProfile : MonoBehaviour {

    public Text editProfile, MatchWon, matchPlayed, matchLose, rank, higestRank;
    public Image title;

    public string[] seditProfile, sMatchWon, sMatchPlayed, sMatchLose, sRank, sHigestRank;
    public Sprite[] sTitle;
    int selLanguage = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");
			editProfile.text = seditProfile [selLanguage];
			MatchWon.text = sMatchWon[selLanguage];
            matchPlayed.text = sMatchPlayed[selLanguage];
            matchLose.text = sMatchLose[selLanguage];
            rank.text = sRank[selLanguage];
            higestRank.text = sHigestRank[selLanguage];
            title.sprite = sTitle[selLanguage];
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            editProfile.text = seditProfile[selLanguage];
            MatchWon.text = sMatchWon[selLanguage];
            matchPlayed.text = sMatchPlayed[selLanguage];
            matchLose.text = sMatchLose[selLanguage];
            rank.text = sRank[selLanguage];
            higestRank.text = sHigestRank[selLanguage];
            title.sprite = sTitle[selLanguage];
        }
	}
}
