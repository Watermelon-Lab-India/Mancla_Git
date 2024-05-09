using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIGameResult : MonoBehaviour {

    public GameObject win, lose, draw;
    public Text myScore, oppocnt, coinInfo;
    public GameObject engineObj, loadingObj;

    public Button privatePlay, vsPlayer;
    Button btn;
	// Use this for initialization
	void Start ()
	{
        if(privatePlay == null)
        {
            privatePlay = this.transform.Find("AIPlayButton").gameObject.GetComponent<Button>();
        }
        if(vsPlayer == null)
        {
            vsPlayer = this.transform.Find("PrivatePlayButton").gameObject.GetComponent<Button>();
        }


	}


    public void RandomPlayAgain()
    {
        //Check Game  type

        if(Engine.gamePlayState == 1 && !Engine.isconnectWithBot)
        {
//            engineObj.GetComponent<UIManager>().PlayWithAIBot(1);
            privatePlay.onClick.Invoke();
            loadingObj.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if(Engine.gamePlayState == 1 && Engine.isconnectWithBot)
        {
            if (engineObj.GetComponent<Engine>().curSelTable < 0)
            {
                engineObj.GetComponent<Engine>().curSelTable = 0;
            }
            if (engineObj.GetComponent<Engine>().coinsArray[engineObj.GetComponent<Engine>().curSelTable] > engineObj.GetComponent<UIManager>().playerInfo.coins)
            {
                return;
            }
            else
            {
                engineObj.GetComponent<UIManager>().TableSelected(engineObj.GetComponent<Engine>().curSelTable);
                loadingObj.SetActive(true);
            }
        }
        else if(Engine.gamePlayState == 2)
        {
            //            engineObj.GetComponent<UIManager>().PlayerWithAnotherPlayer();
            loadingObj.SetActive(true);
            vsPlayer.onClick.Invoke();
            this.gameObject.SetActive(false);
        }
        else if(Engine.gamePlayState == 3)
        {
            if (engineObj.GetComponent<Engine>().curSelTable < 0)
            {
                engineObj.GetComponent<Engine>().curSelTable = 0;
            }
            if (engineObj.GetComponent<Engine>().isFaceBookPlay)
            {
                loadingObj.SetActive(true);
                engineObj.GetComponent<UIManager>().ReturnToHome();
            }
            else
            {
                if (engineObj.GetComponent<Engine>().coinsArray[engineObj.GetComponent<Engine>().curSelTable] > engineObj.GetComponent<UIManager>().playerInfo.coins)
                {
                    return;
                }
                else
                {
                    engineObj.GetComponent<UIManager>().TableSelected(engineObj.GetComponent<Engine>().curSelTable);
                    loadingObj.SetActive(true);
                    }
            }
        }
    }

    private void OnEnable()
    {
        loadingObj.SetActive(false);
        coinInfo.text = engineObj.GetComponent<UIManager>().playerInfo.coins.ToString();
        //GoogleMobileAdsDemoScript.Instance.RequestBanner(0);
        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsShow();
    }

    public GameObject fbPlayObj;

    public void PlayRematch()
    {
        /*
        if (engineObj.GetComponent<Engine>().curSelTable < 0)
        {
            engineObj.GetComponent<Engine>().curSelTable = 0;
        }
        engineObj.GetComponent<UIManager>().TableSelected(engineObj.GetComponent<Engine>().curSelTable);
        loadingObj.SetActive(true);*/
        if (Engine.gamePlayState == 1 && !Engine.isconnectWithBot)
        {
            //            engineObj.GetComponent<UIManager>().PlayWithAIBot(1);
            privatePlay.onClick.Invoke();
            loadingObj.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else if (Engine.gamePlayState == 1 && Engine.isconnectWithBot)
        {
            if (engineObj.GetComponent<Engine>().curSelTable < 0)
            {
                engineObj.GetComponent<Engine>().curSelTable = 0;
            }
            if (engineObj.GetComponent<Engine>().coinsArray[engineObj.GetComponent<Engine>().curSelTable] > engineObj.GetComponent<UIManager>().playerInfo.coins)
            {
                return;
            }
            else
            {
                engineObj.GetComponent<UIManager>().TableSelected(engineObj.GetComponent<Engine>().curSelTable);
                loadingObj.SetActive(true);
            }
        }
        else if (Engine.gamePlayState == 2)
        {
            //            engineObj.GetComponent<UIManager>().PlayerWithAnotherPlayer();
            vsPlayer.onClick.Invoke();
            this.gameObject.SetActive(false);
        }
        else if (Engine.gamePlayState == 3)
        {
            if (engineObj.GetComponent<Engine>().curSelTable < 0)
            {
                engineObj.GetComponent<Engine>().curSelTable = 0;
            }
            if (engineObj.GetComponent<Engine>().isFaceBookPlay)
            {
                loadingObj.SetActive(true);
                engineObj.GetComponent<Engine>().PlayGameWithFaceBook(engineObj.GetComponent<Engine>().curSelTable, engineObj.GetComponent<UIFaceBookManager>().chPlayerId);
//                engineObj.GetComponent<UIManager>().ReturnToHome();
            }
            else
            {
                if (engineObj.GetComponent<Engine>().coinsArray[engineObj.GetComponent<Engine>().curSelTable] > engineObj.GetComponent<UIManager>().playerInfo.coins)
                {
                    return;
                }
                else
                {
                    engineObj.GetComponent<UIManager>().TableSelected(engineObj.GetComponent<Engine>().curSelTable);
                    loadingObj.SetActive(true);
                }
            }
        }
    }

    public void ReturnHome()
    {
     //engineObj.GetComponent<UIManager>().backButton1.GetComponent<Button>().onClick.Invoke();
     fbPlayObj.SetActive(false);
    }

    public void ShareResult()
    {

    }

    public void SetGameState(int State, int yourCnt, int otherCnt)
    {
        myScore.text = yourCnt.ToString();
        oppocnt.text = otherCnt.ToString();

        win.SetActive(false);
        lose.SetActive(false);
        draw.SetActive(false);
        AudioSource[] audios = this.transform.gameObject.GetComponents<AudioSource>();
        if (State == 1)
        {
            int soundState = PlayerPrefs.GetInt("GAME_SOUND");
            if (soundState > 0) 
            {
                audios[0].Play();
            }
         
            win.SetActive(true);

            if (PlayerPrefs.GetInt("LossRepeated4TimeAdUsed") >= 3)
            {
                //				Debug.Log("Other");
                GoogleMobileAdsDemoScript.Instance.ShowInterstitialVideo();
                PlayerPrefs.SetInt("LossRepeated4TimeAdUsed", 0);
            }
            else
            {
                PlayerPrefs.SetInt("LossRepeated4TimeAdUsed", PlayerPrefs.GetInt("LossRepeated4TimeAdUsed") + 1);
                GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
            }
        }
		else if (State == -1)
        {
            int soundState = PlayerPrefs.GetInt("GAME_SOUND");
            if (soundState > 0)
            {
                audios[1].Play();
            }
            lose.SetActive(true);

//			Debug.Log(PlayerPrefs.GetInt("LossRepeated4TimeAdUsed"));

			if(PlayerPrefs.GetInt("LossRepeated4TimeAdUsed") >= 3)
			{
//			ssssss	Debug.Log("Other");
				GoogleMobileAdsDemoScript.Instance.ShowInterstitialVideo();
				PlayerPrefs.SetInt("LossRepeated4TimeAdUsed",0);
			}
			else
			{
				PlayerPrefs.SetInt("LossRepeated4TimeAdUsed",PlayerPrefs.GetInt("LossRepeated4TimeAdUsed") + 1);
				GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
			}
        }
        else if(State == 0)
        {
            draw.SetActive(true);
            if (PlayerPrefs.GetInt("LossRepeated4TimeAdUsed") >= 3)
            {
                //				Debug.Log("Other");
                GoogleMobileAdsDemoScript.Instance.ShowInterstitialVideo();
                PlayerPrefs.SetInt("LossRepeated4TimeAdUsed", 0);
            }
            else
            {
                PlayerPrefs.SetInt("LossRepeated4TimeAdUsed", PlayerPrefs.GetInt("LossRepeated4TimeAdUsed") + 1);
                GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        //GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
    }
}
