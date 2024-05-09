using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsPopup : MonoBehaviour
{
    //admob app id:
    //ios: "ca-app-pub-7368003023090562~1720097131"
    //android: "ca-app-pub-7368003023090562~2836587937"

    public GameObject rateuspopup;

    // Start is called before the first frame update
    void Start()
    {
//		Debug.Log("1:-"+PlayerPrefs.GetInt("rateuspopupStore"));

		//61071f33-f2e1-4076-ba7b-4539022f441b          // Old Photon Id

		if(PlayerPrefs.GetInt("rateuspopupStore") >= 3 && PlayerPrefs.GetInt("rateusNeverpopupStore") != 10)
		{

//			Debug.Log("Show");
			PlayerPrefs.SetInt("rateuspopupStore",0);
			rateuspopup.SetActive(true);
		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnApplicationQuit()
    {
		PlayerPrefs.SetInt("rateuspopupStore",PlayerPrefs.GetInt("rateuspopupStore") + 1);

//		Debug.Log(PlayerPrefs.GetInt("rateuspopupStore"));
     
    }


	public void NotNow()
	{
		rateuspopup.SetActive(false);
		PlayerPrefs.SetInt("rateuspopupStore",0);
	}

	public void RateUs()
	{
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.water.mancalabestboardgame");
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/us/app/mancala-and-friends/id1041732970");
#endif

        rateuspopup.SetActive(false);
		PlayerPrefs.SetInt("rateusNeverpopupStore",10);

	}

	public void Never()
	{
		PlayerPrefs.SetInt("rateusNeverpopupStore",10);
		rateuspopup.SetActive(false);
	}


	public void Share()
	{ 
//		Debug.Log("Share");

		NativeShare myNativeShare = new NativeShare();

		myNativeShare.Share();
	}
}
