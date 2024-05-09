using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtons : MonoBehaviour
{
    public GameObject gmAchv, gmLeader, gmShop, gmSetting;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_IOS
            gmAchv.SetActive(false);
            gmLeader.SetActive(false);
            Vector2 shoppos = gmShop.GetComponent<RectTransform>().anchoredPosition;
            gmShop.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120,shoppos.y);
            Vector2 settingpos = gmSetting.GetComponent<RectTransform>().anchoredPosition;
            gmSetting.GetComponent<RectTransform>().anchoredPosition = new Vector2(120, settingpos.y);
        #endif
    }
}
