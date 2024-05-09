using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgSetting : MonoBehaviour {

    public Text lg, policy;
    public string[] slg, sPolicy;
    int selLanguage = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            lg.text = slg[selLanguage];
            policy.text = sPolicy[selLanguage];
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");
            lg.text = slg[selLanguage];
            policy.text = sPolicy[selLanguage];
        }
	}
}
