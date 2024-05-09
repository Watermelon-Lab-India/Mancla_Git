using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayWithRoomCode : MonoBehaviour
{
    public GameObject TransPanel, CreatedRoom, Loadinggm, Joinpanel;
    public Text txtroomcode, txtwarning;
    public static PlayWithRoomCode _instance;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        CreatedRoom.SetActive(false);
        Joinpanel.SetActive(false);
        Loadinggm.SetActive(false);
        txtwarning.gameObject.SetActive(false);
        TransPanel.SetActive(false);
        txtroomcode.text = "";
    }

    // if (rooms[i].Name.Equals(roomID))

    public void joinRoomFaileddd()
    {
        Loadinggm.SetActive(false);
        TransPanel.SetActive(false);
    }

    public void CreateRoomCode()
    {
        UIManager._instance.SetRoomCode();
        Invoke("displayCode", 0.1f);
    }

    void displayCode()
    {
        txtroomcode.text = UIManager._instance.gameUI.GetComponent<UIGamePlayManager>().RoomCodeTxt.text;
        //  StartCoroutine("CheckPlayers");
    }

    IEnumerator CheckPlayers()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(2f);
            int pcount = UIManager._instance.GetComponent<NetworkingManager>().CheckPlayersInROOMWithCode();
            if (pcount == 2)
            {
                StopCoroutine("CheckPlayers");
                break;
            }
        }
    }

    public void OnCloseCreatedRoom()
    {
        UIManager._instance.GetComponent<Engine>().isWaitingOtherJoinRoom = false;
        UIManager._instance.GetComponent<NetworkingManager>().LeaveRoom();
    }

    public void OnCloseJoinRoom()
    {

    }



    public void OnShareRoomCode()
    {
        NativeShare share = new NativeShare();
        string shareText = "I want to play Mancala with you! \nRoom Code: " + txtroomcode.text + "\nStart Game > Play with Friends > Select Board > Join Room > Enter Room code.\nPlease install:\n";
        shareText += "Android : https://play.google.com/store/apps/details?id=com.water.mancalabestboardgame";
        shareText += "\nIOS : https://apps.apple.com/us/app/mancala-and-friends/id1041732970";
        shareText += "\nBelieve me this is awesome game!";
        share.SetSubject("Share via");
        share.SetText(shareText);
        share.Share();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TransPanel.SetActive(false);
            CreatedRoom.SetActive(false);
            OnCloseCreatedRoom();
        }
    }
}
