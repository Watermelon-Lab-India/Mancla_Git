using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGamePlayManager : MonoBehaviour
{
    public Text oppoName, gameStateDisp, yourturn, otherTurn, mychatTxt, otherChatTxt, timeOut, RoomCodeTxt;

    public GameObject myChatObject, otherChatObject;

    float mychatStart, otherChatStart;

	// Use this for initialization
	void Start () {
		
	}

    public void Mychat(string txchatText)
    {
        myChatObject.SetActive(true);
        mychatTxt.text = txchatText;
        mychatStart = Time.time;
    }

    public void OtherChat(string chatText)
    {
        otherChatObject.SetActive(true);
        otherChatTxt.text = chatText;
        otherChatStart = Time.time;
    }

    public void SetWaitingState()
    {
        gameStateDisp.gameObject.SetActive(true);
        yourturn.gameObject.SetActive(false);
        otherTurn.gameObject.SetActive(false);
    }

    public void PrepareStartGame()
    {
        gameStateDisp.gameObject.SetActive(false);
        yourturn.gameObject.SetActive(false);
        otherTurn.gameObject.SetActive(false);
    }

    public void SetMyTurn()
    {
        gameStateDisp.gameObject.SetActive(false);
        yourturn.gameObject.SetActive(true);
        otherTurn.gameObject.SetActive(false);
    }

    public void SetOtherTurn()
    {
        gameStateDisp.gameObject.SetActive(false);
        yourturn.gameObject.SetActive(false);
        otherTurn.gameObject.SetActive(true);
        if(Engine.gamePlayState == 1)
        {
            if(oppoName.text != "CPU")
            {
                otherTurn.GetComponent<Text>().text = oppoName.text + "'s turn.";
            }
            else
            {
                otherTurn.GetComponent<Text>().text = "CPU's turn.";
            }
        }
        else if(Engine.gamePlayState == 2)
        {
            otherTurn.GetComponent<Text>().text = "Opponent's turn.";
        }
        else if(Engine.gamePlayState == 3)
        {
            otherTurn.GetComponent<Text>().text = oppoName.text + "'s turn.";
        }
    }

    public GameObject engineObj;
    bool soundCurstate = false;
    private void OnEnable()
    {
        UIManager._instance.selectBoardGroup.SetActive(false);
        UIManager._instance.selectBoardGroupfB.SetActive(false);
        UIManager._instance.PrivatePlayScreen.SetActive(false);
        UIManager._instance.backButton3.SetActive(false);
        timeOut.gameObject.SetActive(false);

        if(engineObj.GetComponent<AudioSource>().enabled)
        {
            soundCurstate = true;
            engineObj.GetComponent<AudioSource>().enabled = false;
        }    
    }

    private void OnDisable()
    {
        if(soundCurstate)
        {
            engineObj.GetComponent<AudioSource>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update () {
		if(myChatObject.activeSelf && Time.time - mychatStart >= 5f)
        {
            myChatObject.gameObject.SetActive(false);
        }

        if (otherChatObject.activeSelf && Time.time - otherChatStart >= 5f)
        {
            otherChatObject.gameObject.SetActive(false);
        }
    }
}
