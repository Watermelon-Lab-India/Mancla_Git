using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILanguageManage : MonoBehaviour {

    public UILanguageManager uLManager;

    public GameObject[] selLangeImages;
    static public int languageState = 0;
	// Use this for initialization
	void Start () {
		
	}

	public void LanguageSelected(int index)
	{
        languageState = index;
        PlayerPrefs.SetInt("LState", index);
        for(int i = 0; i < 6; i ++)
        {
            if(i == index)
            {
                selLangeImages[i].SetActive(true);
            }
            else
            {
                selLangeImages[i].SetActive(false);
            }
        }

        uLManager.ChangeLanguageSetting(index);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
