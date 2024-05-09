using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerProfileEditor : MonoBehaviour {

	public Text playerName, winCnt, totalCnt, loseCnt, rank, highestRank;
	public Image characterImage;
    public GameObject playerInfoObject;
    // Use this for initialization
    void Start ()
    {
		
	}
    
	//set player profile information when the player click the profile button on the fires page
	public void SetPlayerProfileInfo()
	{
		playerName.text = playerInfoObject.GetComponent<UIManager> ().playerInfo.playerName;
		winCnt.text = playerInfoObject.GetComponent<UIManager> ().playerInfo.winCount.ToString();
		totalCnt.text = (playerInfoObject.GetComponent<UIManager>().playerInfo.winCount + playerInfoObject.GetComponent<UIManager>().playerInfo.matchLoseCount).ToString();
		loseCnt.text = playerInfoObject.GetComponent<UIManager> ().playerInfo.matchLoseCount.ToString();
		rank.text = playerInfoObject.GetComponent<UIManager> ().playerInfo.rank.ToString();
		highestRank.text = playerInfoObject.GetComponent<UIManager> ().playerInfo.highestRank.ToString();

		string characterName = playerInfoObject.GetComponent<UIManager> ().playerInfo.characterName;

        if (playerInfoObject.GetComponent<UIManager>().playerInfo.logInWithFB == 1)
            characterImage.sprite = playerInfoObject.GetComponent<UIManager>().characterImage_facebook.sprite;
        else
            characterImage.sprite = playerInfoObject.GetComponent<UIManager> ().characters [int.Parse (characterName)];
	}

    private void OnEnable()
    {
        SetPlayerProfileInfo();
    }

    private void OnDisable()
    {
       // GoogleMobileAdsDemoScript.Instance.OnBannerAdsStop();
    }

    // Update is called once per frame
    void Update () {
		
	}
}