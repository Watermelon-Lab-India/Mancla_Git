using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILgExit : MonoBehaviour
{
    public Text exit, title, yes, no;
    int lState =0;

    public string[] sExit, sTitle, sYes, sNo;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("LState") != UILanguageManage.languageState)
        {
            lState = PlayerPrefs.GetInt("LState");
            Debug.Log("not equal");
            exit.text = sExit[lState];
            title.text = sTitle[lState];
            yes.text = sYes[lState];
            no.text = sNo[lState];
        }
        else
        {
            lState = PlayerPrefs.GetInt("LState");
            Debug.Log("equal");
            exit.text = sExit[lState];
            title.text = sTitle[lState];
            yes.text = sYes[lState];
            no.text = sNo[lState];
        }
    }
}
