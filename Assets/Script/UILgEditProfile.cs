using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgEditProfile : MonoBehaviour
{
    public Text avatar, submit;
    public string[] sAvatar, sSubmit;
    int selLanguage;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            avatar.text = sAvatar[selLanguage];
            submit.text = sSubmit[selLanguage];
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            avatar.text = sAvatar[selLanguage];
            submit.text = sSubmit[selLanguage];
        }
	}
}
