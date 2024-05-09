using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProfileUpdate : MonoBehaviour
{
    public GameObject uiEngine;
    public Text plName;

    private void OnEnable()
    {
        //GoogleMobileAdsDemoScript.Instance.RequestBanner(0);
        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsShow();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(PlayerPrefs.GetInt("FBState") == 1)
        {
            plName.text = uiEngine.GetComponent<UIManager>().playerInfo.playerName;
        }

		if(uiEngine.GetComponent<UIManager>().playerInfo.playerName != plName.text)
        {
            plName.text = uiEngine.GetComponent<UIManager>().playerInfo.playerName;
            Debug.Log("aaa" + plName.text);
        }
	}
}