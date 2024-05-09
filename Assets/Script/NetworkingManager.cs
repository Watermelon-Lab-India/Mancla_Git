using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System;
using Photon;
using UnityEngine.UI;
public class NetworkingManager :  MonoBehaviourPunCallbacks{

    public bool isConnecting = false;
    string specRoomName;
    public bool gametype = false;

    public bool acceptType = false;

	// Use this for initialization
	void Start () {
		
	}

    public void LeaveRoom()
    {
        isConnecting = false;
        if (PhotonNetwork.CurrentRoom == null)
            return;
        PhotonNetwork.LeaveRoom();
    }

    public void ConnectToSerververSpecial(int tableId, string playerName, string roomId)
    {
        isConnecting = true;
        PhotonNetwork.NickName = playerName;
        Debug.Log("connect room called ---" + playerName);
        specRoomName = roomId;
        gametype = true;
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CreateRoom(specRoomName, new RoomOptions() { MaxPlayers = 2, PublishUserId = true, IsVisible = false, PlayerTtl = 0, EmptyRoomTtl = 0 }, TypedLobby.Default);
            }
            else
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.CreateRoom(specRoomName, new RoomOptions() { MaxPlayers = 2, PublishUserId = true, IsVisible = false, PlayerTtl = 0, EmptyRoomTtl = 0 }, TypedLobby.Default);
            }
        }
        else
        {
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        } 
        
    }


    public void ConnectToServerver(int tableId, string playerName)
    {
        gametype = false;
        isConnecting = true;
        PhotonNetwork.NickName = playerName;
        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "--master ---" + " ---";
        Debug.Log("connect room called ---");
        if(PhotonNetwork.IsConnectedAndReady)
        {
			Debug.Log("Connected ready");
            if(PhotonNetwork.IsMasterClient)
            {
                if (UIManager._instance.game_Mode == 1)
                {
                    IfCOnnectionInCodeMode();
                }
                else
                {
                    Debug.Log("Master Client");
                    PhotonNetwork.JoinRandomRoom();
                }
            }
            else
            {
                PhotonNetwork.LeaveRoom();
                if (UIManager._instance.game_Mode == 1)
                {
                    IfCOnnectionInCodeMode();
                }
                else
                {
                    Debug.Log("Leave nd join");
                    PhotonNetwork.JoinRandomRoom();
                }
            }
        }
        else
        {
            Debug.Log(" connction using game mode : " + UIManager._instance.game_Mode);
            Debug.Log("connect using setting");
           
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void IfCOnnectionInCodeMode()
    {
        //PhotonNetwork.JoinLobby();
        Debug.Log(PhotonNetwork.IsMasterClient + " on connected game mode : " + UIManager._instance.game_Mode);
        if (PlayerPrefs.GetInt("privateRoom", 0) == 1)
        {
            if (UIManager._instance.game_Mode == 1)
            {
                PlayerPrefs.SetInt("privateRoom", 0);
                gametype = true;
            }
        }

        int tableId = this.gameObject.GetComponent<Engine>().curSelTable;
        if (tableId >= 0)
        {
            if (gametype)
            {
                Debug.Log("---room called 123123----");
                PhotonNetwork.CreateRoom(specRoomName, new RoomOptions() { MaxPlayers = 2, PublishUserId = true, IsVisible = false, PlayerTtl = 0, EmptyRoomTtl = 0 });
            }
            else
            {
                if (acceptType)
                {
                    Debug.Log("---room called ----" + specRoomName);
                    acceptType = false;
                    PhotonNetwork.JoinRoom(specRoomName);
                }
                else
                {
                    Debug.Log("---room called 123123----" + specRoomName);
                    PhotonNetwork.JoinRandomRoom();
                }
            }
        }
        else
        {
            this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
        }
    }

    public void JoinChallengedPlayer(string roomName, string playerName)
    {        
        isConnecting = true;
        PhotonNetwork.NickName = playerName;
        acceptType = true;
        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "join challenge player ---" + playerName + " ---";
        Debug.Log("connect room called ---" + playerName);
        specRoomName = roomName;
//        gametype = true;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.JoinRoom(specRoomName);
            }
            else
            {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.JoinRoom(specRoomName);
            }
        }
        else
        {
            Debug.Log("connect called ---" + roomName);
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void CreatePrivateRoom()
    {
        string roomName111 = "";
        for (int i = 0; i < 8; i++)
        {
            roomName111 = roomName111 + UnityEngine.Random.Range(0, 10);
        }
        specRoomName = roomName111;
        Debug.LogError("Room code : "+specRoomName);
        this.gameObject.GetComponent<UIManager>().gameUI.GetComponent<UIGamePlayManager>().RoomCodeTxt.text = specRoomName;
        //PhotonNetwork.CreateRoom(roomName111, new RoomOptions() { MaxPlayers = 2, PublishUserId = true, IsVisible = true, PlayerTtl = 0, EmptyRoomTtl = 0 }, TypedLobby.Default);
    }

    public void ConnectToServerWithPlayerId(string playerId)
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.AuthValues != null)
            {
                PhotonNetwork.AuthValues.UserId = playerId;
            }
            else
            {
                PhotonNetwork.AuthValues = new AuthenticationValues(playerId);
            }
            PhotonNetwork.JoinRoom("globalroom");
        }
        else
        {
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.AuthValues = new AuthenticationValues(playerId);
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public List<string> GetFriendsList(string[] friendList)
    {
        UnityEngine.Debug.Log(PhotonNetwork.CurrentRoom.Name + " ---- " + PhotonNetwork.CurrentRoom.PlayerCount);

        List<string> fList = new List<string>();

        foreach(int key in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            Player plInfo;
            PhotonNetwork.CurrentRoom.Players.TryGetValue(key,out plInfo);
            Debug.Log(plInfo.UserId);            
            for(int i = 0; i < friendList.Length; i ++)
            {
                if(friendList[i] == plInfo.UserId)
                {
                    fList.Add(plInfo.UserId);
                }
            }
        }
        return fList;
    }

    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        for(int i = 0; i < friendList.Count; i ++)
        {
            FriendInfo rInfo = friendList[i];
            Debug.Log(rInfo.Room + "-----" + rInfo.IsInRoom + "-----" + rInfo.IsOnline + "----" + rInfo.UserId);
        }
        Debug.Log("friend find result returned");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby called");
    }

    RoomInfo[] arryRoominfo;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        arryRoominfo = new RoomInfo[roomList.Count];
        int i = 0;
        foreach (RoomInfo info in roomList)
        {
            arryRoominfo[i] = info;
            Debug.LogError(info.Name);
            i++;
        }
    }

    public override void OnConnectedToMaster()
    {
        //        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "master connected ---" + PhotonNetwork.BestRegionSummaryInPreferences + " ---";
        
        Debug.Log("isconnectiong --- master----");
        if (isConnecting)
        {
            //PhotonNetwork.JoinLobby();
            Debug.Log(PhotonNetwork.IsMasterClient + " on connected game mode : " + UIManager._instance.game_Mode);
            if (PlayerPrefs.GetInt("privateRoom", 0)==1)
            {
                if (UIManager._instance.game_Mode == 1)
                {
                    PlayerPrefs.SetInt("privateRoom", 0);
                    gametype = true;
                   // CreatePrivateRoom();
                }
            }

            int tableId = this.gameObject.GetComponent<Engine>().curSelTable;
            if(tableId >= 0)
            {
                ExitGames.Client.Photon.Hashtable exroomList = new ExitGames.Client.Photon.Hashtable() { { "table", tableId } };

                int maxPlayers = 2;
                byte maxPlByte = Convert.ToByte(maxPlayers);
                Debug.Log("master server connected called ---");
                //                PhotonNetwork.JoinRandomRoom(exroomList, maxPlByte);
                if (gametype)
                {
                    Debug.Log("---room called 123123----");
                    PhotonNetwork.CreateRoom(specRoomName, new RoomOptions() { MaxPlayers = 2, PublishUserId = true, IsVisible = false, PlayerTtl = 0, EmptyRoomTtl = 0 });
                }
                else
                {
                    if(acceptType)
                    {
                        Debug.Log("---room called ----" + specRoomName);
                        acceptType = false;
                        PhotonNetwork.JoinRoom(specRoomName);
                    }
                    else
                    {
                        Debug.Log("---room called 123123----" + specRoomName);
                        PhotonNetwork.JoinRandomRoom();
                    }
                }
            }
            else
            {
                this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "join room failed---" + " ---";
        Debug.Log(returnCode + "join room failed called ---"+ message);
        if (UIManager._instance.game_Mode == 1)
        {
            StartCoroutine("DisplayWarning", message);
            if (trycount >= 5)
            {
                int tableId = this.gameObject.GetComponent<Engine>().curSelTable;

                if (tableId >= 0)
                {
                    // join spec room failed
                    this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
                }
                else
                {
                    this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
                }
            }
        }
        else
        {
            int tableId = this.gameObject.GetComponent<Engine>().curSelTable;

            if (tableId >= 0)
            {
                // join spec room failed
                this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
            }
            else
            {
                this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
            }
        }
        Debug.Log("join room failed trycount ---" + trycount);
        
    }

    public int CheckPlayersInROOMWithCode()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            foreach (var key in PhotonNetwork.CurrentRoom.Players)
            {
                Debug.Log(key.Value.NickName);
            }
            Debug.LogError("===============================");
            return PhotonNetwork.CurrentRoom.Players.Count;
        }
        else
        {
            return 0;
        }
    }

    int trycount = 0;
    IEnumerator DisplayWarning(string mesg)
    {
        if (trycount < 5)
        {
            yield return new WaitForSeconds(3f);
            Debug.Log(trycount + " try again : " + specRoomName);
            PhotonNetwork.JoinRoom(specRoomName);
            trycount++;
        }
        if (trycount >= 5)
        {
            UIManager._instance.txtwarning.gameObject.SetActive(true);
            UIManager._instance.txtwarning.text = mesg;
            if (PlayWithRoomCode._instance != null)
            {
                PlayWithRoomCode._instance.joinRoomFaileddd();
            }
            yield return new WaitForSeconds(5f);
            UIManager._instance.txtwarning.text = string.Empty;
            UIManager._instance.txtwarning.gameObject.SetActive(false);
            trycount = 0;
        }
    }

    bool decideState = false;
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "join random failed---" + " ---";

        int tableId = this.gameObject.GetComponent<Engine>().curSelTable;

        if(tableId >= 0)
        {
            ExitGames.Client.Photon.Hashtable exroomList = new ExitGames.Client.Photon.Hashtable() { { "table", tableId } };
            string[] roomPropsInLobby = { "map" };

            RoomOptions roomOps = new RoomOptions() { CustomRoomProperties = exroomList, CustomRoomPropertiesForLobby = roomPropsInLobby };          

            int decide = UnityEngine.Random.Range(0, 1);
            Debug.LogError("create room called ---" + decide+"==="+ decideState);
            if (decide == 0 && decideState == false)
                {
                Debug.LogError("JoinRandomRoom ---");
                decideState = true;
                PhotonNetwork.JoinRandomRoom();
                }
            else
                {
                Debug.LogError("create Room ---");
                roomOps.MaxPlayers = 2;
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
                decideState = false;
                }
//            PhotonNetwork.CreateRoom(null, roomOps, TypedLobby.Default);
//            PhotonNetwork.CreateRoom("mancala", new RoomOptions() { MaxPlayers = 2, PublishUserId = true, IsVisible = true, PlayerTtl = 0, EmptyRoomTtl = 0 });
        }
        else
        {
            this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "----join master failed---";
        isConnecting = false;
        this.gameObject.GetComponent<Engine>().OnNetWorkDisconnected();
        this.gameObject.GetComponent<UIManager>().ShowWiFiAnimation();
    }

    public override void OnJoinedRoom()
    {
        trycount = 0;
        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "join room success ---"+ " ---";
        Debug.Log("join room successed called ---" + PhotonNetwork.CurrentRoom.Name + "---count ---" + PhotonNetwork.CurrentRoom.PlayerCount);
        if (UIManager._instance.game_Mode == 1 && gametype)
        {
            gametype = false;
        }
        if (!isConnecting)
            return;

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            if(gametype)
            {
                this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId = specRoomName;
                isConnecting = true;
                this.gameObject.GetComponent<Engine>().SendJoinRequest(specRoomName);
                this.gameObject.GetComponent<Engine>().WaitingOppentesFaceBook();
            }
            else
            {
                this.gameObject.GetComponent<UIManager>().challengeWindow.SetActive(false);
                if (UIManager._instance.game_Mode == 1)
                {
                    this.gameObject.GetComponent<Engine>().WaitingOppentesWithCode();
                }
                else
                {
                    this.gameObject.GetComponent<Engine>().WaitingOppentes();
                }
            }

        }
        else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
          
            //set other player state that he is ready to play game;
            //            this.gameObject.GetComponent<UIManager>().playerInfo.coins -= this.gameObject.GetComponent<Engine>().coinsArray[this.gameObject.GetComponent<Engine>().curSelTable];
            this.gameObject.GetComponent<UIManager>().challengeWindow.SetActive(false);
            this.gameObject.GetComponent<UIManager>().SavePlayerInfo();
            this.gameObject.GetComponent<Engine>().SetOpponentInformation(PhotonNetwork.CurrentRoom.GetPlayer(1).NickName);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(this.gameObject.GetComponent <Engine>().isPlayingGame && !this.gameObject.GetComponent<Engine>().b.gameOver())
        {
            this.gameObject.GetComponent<UIManager>().OpponentLeaveRoom();
        }
//        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        this.gameObject.transform.parent.Find("Text").gameObject.GetComponent<Text>().text += "player entered ---" + newPlayer.NickName + " ---";
        Debug.Log("onplayer entered room called ---" + newPlayer.NickName);


        if (PhotonNetwork.IsMasterClient && isConnecting)
        {
            if (UIManager._instance.game_Mode==1)
            {
                int playerC = CheckPlayersInROOMWithCode();
                Debug.Log("total player : "+playerC);
                if (playerC == 2)
                {
                    this.GetComponent<Engine>().SetGamePlayCode();
                }
            }

            int starter = 1;
            this.gameObject.GetComponent<Engine>().OpponentEnteredGame(starter, newPlayer.NickName);

            PhotonView photonView = PhotonView.Get(this);

            photonView.RPC("DecideStartPlayer", RpcTarget.Others, 1 - starter, this.gameObject.GetComponent<UIManager>().playerInfo.playerName);
        }
    }

    public void SendPlayMatchRequest(string userId)
    {
        PhotonView photonView = PhotonView.Get(this);

        Debug.Log("called match request ---" + userId);

        photonView.RPC("ReceiveMatchRequest", RpcTarget.Others, userId);
    }

    public void SendMyMoveToOtherPlayer(int moveStoneNumber)
    {
        PhotonView photonView = PhotonView.Get(this);

        Debug.Log("called send move ---" + moveStoneNumber);   

        photonView.RPC("ReceiveMoveStoneInfo", RpcTarget.Others, moveStoneNumber);
    }

    public void SendMyChat(string chText)
    {
        PhotonView photonView = PhotonView.Get(this);

        Debug.Log("called send chat ---" + chText);

        photonView.RPC("RecieveChatMessage", RpcTarget.Others, chText);
    }

    public void SendMyEmotion(int id)
    {
        PhotonView photonView = PhotonView.Get(this);

        Debug.Log("called send emotion---" + id);

        photonView.RPC("ReceiveEmotionMessage", RpcTarget.Others, id);
    }

    [PunRPC]
    public void ReceiveEmotionMessage(int id)
    {
        this.gameObject.GetComponent<UIManager>().ReceiveEmotionMessage(id);
    }

    [PunRPC]
    public void ReceiveMatchRequest(string userId)
    {
        Debug.Log("request arrived" + userId + "--- " + PhotonNetwork.AuthValues.UserId);
        if (PhotonNetwork.AuthValues == null || (PhotonNetwork.AuthValues.UserId != null && PhotonNetwork.AuthValues.UserId != userId))
        {
            return;
        }
        else if (PhotonNetwork.AuthValues.UserId == userId)
        {
            this.gameObject.GetComponent<UIFaceBookManager>().chPlayerName = userId;
            this.gameObject.GetComponent<UIManager>().ShowChallengeWindow();
        }
        else
        {
            return;
        }
    }

    [PunRPC]
    public void RecieveChatMessage(string chatText)
    {
        this.gameObject.GetComponent<UIManager>().ReceiveChatMessage(chatText);
    }

    [PunRPC]
    public void DecideStartPlayer(int starterId, string otherPlayerName)
    {        
        this.gameObject.GetComponent<Engine>().OpponentEnteredGame(starterId, otherPlayerName);
    }

    [PunRPC]
    public void ReceiveMoveStoneInfo(int stoneId)
    {
        Debug.Log("recevied move ---" + stoneId);
        this.gameObject.GetComponent<Engine>().OpponentSelectedBowl(stoneId);
    }

   public void DisconnectCurrentConnection()
    {

    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            //            UnityEngine.Debug.Log("leave room caled");
            //            PhotonNetwork.LeaveRoom();
            if(PhotonNetwork.CurrentRoom != null)
                PhotonNetwork.LeaveRoom();
            if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.Name != "globalroom")
            {
                Debug.Log("isconnecting false-----------------------------------------");
                //               PhotonNetwork.LeaveRoom();
                isConnecting = false;
            }
//            PhotonNetwork.Disconnect();
        }
	}
}
