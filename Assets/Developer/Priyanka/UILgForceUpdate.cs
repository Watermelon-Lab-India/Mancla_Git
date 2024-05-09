using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILgForceUpdate : MonoBehaviour
{

    public Text title,updateText,cancel,exit,update;

    public string[] sTitle, sUpdateText, sCancel, sExit, sUpdate;

    int lState = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            lState = PlayerPrefs.GetInt("LState");
            title.text = sTitle[lState];
            updateText.text = sUpdateText[lState];
            cancel.text = sCancel[lState];
            exit.text = sExit[lState];
            update.text = sUpdate[lState];
        }
        else
        {
            lState = PlayerPrefs.GetInt("LState");
            title.text = sTitle[lState];
            updateText.text = sUpdateText[lState];
            cancel.text = sCancel[lState];
            exit.text = sExit[lState];
            update.text = sUpdate[lState];
        }
    }
}
