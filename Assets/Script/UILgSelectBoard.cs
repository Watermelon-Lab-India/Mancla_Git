using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILgSelectBoard : MonoBehaviour
{
    public Text table1, table2, table3, table4, table5, table6;
    public Text[] entry;

    public string[] stable1, stable2, stable3, stable4, stable5, stable6;
    public string[] sentry, sunlock;

    int selLanguage = 0;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            selLanguage = PlayerPrefs.GetInt("LState");

            table1.text = stable1[selLanguage];
            table2.text = stable2[selLanguage];
            table3.text = stable3[selLanguage];
            table4.text = stable4[selLanguage];
            table5.text = stable5[selLanguage];
            table6.text = stable6[selLanguage];

            for(int i = 0; i < 6; i ++)
            {
                if(entry[i].text == "UNLOCK")
                {
                    entry[i].text = sunlock[selLanguage];
                }
                else
                {
                    entry[i].text = sentry[selLanguage];
                }
            }
        }
        else
        {
            selLanguage = PlayerPrefs.GetInt("LState");

            table1.text = stable1[selLanguage];
            table2.text = stable2[selLanguage];
            table3.text = stable3[selLanguage];
            table4.text = stable4[selLanguage];
            table5.text = stable5[selLanguage];
            table6.text = stable6[selLanguage];

            for (int i = 0; i < 6; i++)
            {
                if (entry[i].text == "UNLOCK")
                {
                    entry[i].text = sunlock[selLanguage];
                }
                else
                {
                    entry[i].text = sentry[selLanguage];
                }
            }
        }
	}
}
