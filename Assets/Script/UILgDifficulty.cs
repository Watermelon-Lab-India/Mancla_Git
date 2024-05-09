using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgDifficulty : MonoBehaviour {

    public Text easy, medium, diff;
    public string[] sEasy, sMedium, sDiff;
    int selLanguage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            easy.text = sEasy[selLanguage];
            medium.text = sMedium[selLanguage];
            diff.text = sDiff[selLanguage];
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            easy.text = sEasy[selLanguage];
            medium.text = sMedium[selLanguage];
            diff.text = sDiff[selLanguage];
        }
	}
}
