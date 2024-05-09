using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingManage : MonoBehaviour
{
	public GameObject languageObject, musicMute;
	public GameObject soundon, soundMute;

    public GameObject faceBookButton;
    public GameObject GoogleButton;

    public GameObject fbManager;


    public Image offImage;
    public Text txt;


	// Use this for initialization
	void Start ()
    {
        /*
    #if UNITY_IOS
        GoogleButton.SetActive(false);
        faceBookButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-80);
    #endif
    */
    }

    public void MusicClick()
	{
		if (musicMute.activeSelf) {
			musicMute.SetActive (false);
		} else {
			musicMute.SetActive (true);
		}
	}

	public void SoundClick()
	{
		if (soundon.activeSelf) {
			soundon.SetActive (false);
			soundMute.SetActive (true);
		} else {
			soundon.SetActive (true);
			soundMute.SetActive (false);
		}
	}

	public void LanguageClick(int i)
	{
		
	}

    public void OnEnable()
    {
        if(PlayerPrefs.GetInt("FBState") == 1)
        {
            faceBookButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = "off";
            faceBookButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            faceBookButton.transform.GetChild(0).gameObject.SetActive(false);
            faceBookButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = "on";
        }


        StartCoroutine(wait());
     
 //       Invoke("ShowBanner",2f);

        if (PlayerPrefs.GetInt("GoogleLogin") == 1)
        {
            offImage.gameObject.SetActive(true);
            txt.text = "Off";
        }
        else
        {
            offImage.gameObject.SetActive(false);
            txt.text = "On";
        }

    }

    IEnumerator wait()
    {
      //  GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
        yield return new WaitForSeconds(5);
        //GoogleMobileAdsDemoScript.Instance.RequestBanner(1);
        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsShow();
    }

    public void FaceBookButtonClicked()
    {
        if(PlayerPrefs.GetInt("FBState") == 1)
        {
            
            faceBookButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = "on";
            faceBookButton.transform.GetChild(0).gameObject.SetActive(false);
            fbManager.GetComponent<UIFaceBookManager>().FaceBookLogOut();
        }
        else
        {
            fbManager.GetComponent<UIFaceBookManager>().isFirstLogin = true;
            fbManager.GetComponent<UIFaceBookManager>().FaceBookLogin();        
        }
    }

    public void FacebookLoginSuccess()
    {
        faceBookButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = "off";
        faceBookButton.transform.GetChild(0).gameObject.SetActive(true);
    }
    
    public void ShowBanner()
    {
        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsShow();
    }
    
    // Update is called once per frame
    void Update ()
    {
		
	}

    //private void OnDisable()
    //{
    //    GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
    //}

    public void DestroyOnBackPressed()
    {
        if(gameObject.activeSelf)
        {
        //  GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
        }
    }
}
