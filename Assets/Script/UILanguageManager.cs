using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILanguageManager : MonoBehaviour {

    public Image[] textImages;

    public Sprite[] lang_textImages;

	// Use this for initialization
	void Start ()
    {
		
	}

    public void ChangeLanguageSetting(int index)
    {
        for (int i = 0; i < 12; i++)
        {
            if(lang_textImages[i * 6 + index] == null)
            {
                textImages[i].sprite = lang_textImages[i * 6];
                textImages[i].SetNativeSize();
            }
            else               
            {
                textImages[i].sprite = lang_textImages[i * 6 + index];
                textImages[i].SetNativeSize();
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
