using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterPrivateCodeDialogController : MonoBehaviour
{

    public GameObject inputField;
    public GameObject confirmationText;
    public GameObject joinButton;
    private Button join;
    private InputField field;
    public GameObject GameConfiguration;
    public GameObject failedDialog;
    void OnEnable()
    {
        if (field != null)
            field.text = "";
        if (confirmationText != null)
            confirmationText.SetActive(false);
        if (join != null)
            join.interactable = false;
    }

    // Use this for initialization
    void Start()
    {
        field = inputField.GetComponent<InputField>();
        join = joinButton.GetComponent<Button>();
        join.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (field.text.Length > 0)
        {
            if (field.text.Length < 8)
            {
                confirmationText.SetActive(true);
                join.interactable = false;
            }
            else
            {
                confirmationText.SetActive(false);
                join.interactable = true;
            }
        }

       
    }

    public void onValueChanged()
    {

        
    }

    public void JoinByRoomID()
    {
        
        //GameManager.Instance.JoinedByID = true;
        //GameManager.Instance.payoutCoins = 0;
        //string roomID = field.text;
        //Debug.Log(roomID + " : RoomId");

        //RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        //Debug.Log("Rooms count: " + rooms.Length);

        //if (rooms.Length == 0)
        //{
        //    Debug.Log("no rooms!");
        //    failedDialog.SetActive(true);
        //}
        //else
        //{
        //    bool foundRoom = false;
        //    for (int i = 0; i < rooms.Length; i++)
        //    {
        //        if (rooms[i].Name.Equals(roomID))
        //        {
        //            foundRoom = true;
        //            if (rooms[i].CustomProperties.ContainsKey("pc"))
        //            {
        //                GameManager.Instance.payoutCoins = int.Parse(rooms[i].CustomProperties["pc"].ToString());

        //                if (rooms[i].CustomProperties.ContainsKey("rt"))
        //                {
        //                    Debug.Log("JESTEM RT max plyer : "+ rooms[i].MaxPlayers);
        //                    GameManager.Instance.mode = (MyGameMode)rooms[i].CustomProperties["rt"];
        //                }
        //                else
        //                {
        //                    Debug.Log("Jestem BRAK");
        //                }
        //                PlayFabManager._instance.TotalPlayers = rooms[i].MaxPlayers;
        //                //if (GameManager.Instance.myPlayerData.GetCoins() >= GameManager.Instance.payoutCoins)
        //                {
        //                    PhotonNetwork.JoinRoom(roomID);
        //                }
        //                GameConfiguration.GetComponent<GameConfigrationController>().startGame();
        //            }
        //            else
        //            {
        //                GameManager.Instance.payoutCoins = int.MaxValue;
        //                GameConfiguration.GetComponent<GameConfigrationController>().startGame();
        //            }
        //        }
        //    }
        //    if (!foundRoom)
        //    {
        //        failedDialog.SetActive(true);
        //    }
        //}




    }
}
