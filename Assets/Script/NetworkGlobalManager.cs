using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;
using Photon.Realtime;
using Photon.Pun;
using System;
using Photon;

public class NetworkGlobalManager : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks, IInRoomCallbacks
    {
    public string AppId; // set in inspector
    private LoadBalancingClient lbc;
    string str = "";
    public Text textIII;
    string roomName, playerName;

    void Start()
        {
        this.lbc = new LoadBalancingClient();
        this.lbc.EventReceived += OnEvent;
        }

    void Awake()
        {

        }

    void Update()
        {
        LoadBalancingClient client = this.lbc;
        if (client != null)
            {
            client.Service();
            }

        if (client != null)
            {
            if (str != client.State.ToString())
                {
                string str = client.State.ToString();
                if (client.CurrentRoom != null)
                    {
                    //                    UnityEngine.Debug.Log("----" + client.CurrentRoom.PlayerCount);
                    }
                }
            }
        }

    bool requestSentState = false;

    public void SendAcceptChallenge(bool state)
        {

        byte eventCode = 3; // make up event codes at will

        JsonObject jObj = new JsonObject();
        jObj.Add("targetId", this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId);
        jObj.Add("state", state);
        this.lbc.OpRaiseEvent(eventCode, jObj.ToString(), RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
        }

    private void OnEvent(ExitGames.Client.Photon.EventData eData)
    {
        //            string boardState = (string)BoardState;
        Debug.Log("llll-----------" + (string)eData.CustomData);

        if (eData.Code == (byte)1)
        {
            JsonObject jObj = (JsonObject)SimpleJson.SimpleJson.DeserializeObject((string)eData.CustomData);

            string targetId = Convert.ToString(jObj["targetId"]);
            string targetName = Convert.ToString(jObj["myName"]);
            string targetFBId = Convert.ToString(jObj["myFBId"]);
            if (targetId == playerName && !this.gameObject.GetComponent<Engine>().isPlayingGame)
            {
                if(!this.gameObject.GetComponent<UIManager>().gameUI.activeSelf)
                    {
                    this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();
                    this.gameObject.GetComponent<UIFaceBookManager>().chPlayerName = targetName;
                    this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId = targetFBId;
                    this.gameObject.GetComponent<UIManager>().ShowChallengeWindow();
                    }
                else
                    {
                    this.gameObject.GetComponent<UIFaceBookManager>().chPlayerName = targetName;
                    this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId = targetFBId;
                    RejectChallenge();
                    }
            }
        }
        else if(eData.Code == (byte)2)
            {
            JsonObject jObj = (JsonObject)SimpleJson.SimpleJson.DeserializeObject((string)eData.CustomData);
            string targetId = Convert.ToString(jObj["targetId"]);
            if (targetId == playerName && !this.gameObject.GetComponent<Engine>().isPlayingGame)
                {
                this.gameObject.GetComponent<UIManager>().OpponentRejectRequest();
                Debug.Log("isconnecting false-----------------------------------------");
                this.gameObject.GetComponent<NetworkingManager>().isConnecting = false;
                this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();
                }
            }
        else if (eData.Code == (byte)3)
            {
            JsonObject jObj = (JsonObject)SimpleJson.SimpleJson.DeserializeObject((string)eData.CustomData);
            string targetId = Convert.ToString(jObj["targetId"]);
            if (targetId == playerName && !this.gameObject.GetComponent<Engine>().isPlayingGame)
                {
                bool state = Convert.ToBoolean(jObj["state"]);
                requestSentState = false;
                if(state)
                    {
                    this.gameObject.GetComponent<Engine>().OpponentAcceptedChallenge();
                    }
                else
                    {
                    this.gameObject.GetComponent<UIManager>().OpponentRejectRequest();
                    Debug.Log("isconnecting false-----------------------------------------");
                    this.gameObject.GetComponent<NetworkingManager>().isConnecting = false;
                    this.gameObject.GetComponent<NetworkingManager>().LeaveRoom();
                    }
                }
            }
        else if (eData.Code == (byte)4)
            {
            JsonObject jObj = (JsonObject)SimpleJson.SimpleJson.DeserializeObject((string)eData.CustomData);
            string targetId = Convert.ToString(jObj["targetId"]);
            if (targetId == playerName && !this.gameObject.GetComponent<Engine>().isPlayingGame)
                {
                string roomId = Convert.ToString(jObj["roomId"]);
                this.gameObject.GetComponent<UIManager>().ChallengedRoomCreated(roomId);
                }
            }
        }

    public void SendJoinRoomRequest(string roomId)
        {
        byte eventCode = 4; // make up event codes at will

        JsonObject jObj = new JsonObject();
        jObj.Add("targetId", this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId);
        jObj.Add("roomId", roomId);
        this.lbc.OpRaiseEvent(eventCode, jObj.ToString(), RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
        }

    public void ConnectToServerWithPlayerId(string playerId)
    {
        this.lbc = new LoadBalancingClient();
        this.lbc.EventReceived += OnEvent;
        this.lbc.AppId = "61071f33-f2e1-4076-ba7b-4539022f441b";
        playerName = playerId;
        this.lbc.AuthValues = new AuthenticationValues(playerId);
        //        this.lbc.
        this.lbc.AddCallbackTarget(this);
        //        this.lbc.UserId = playerId;
        this.lbc.ConnectToNameServer();
    }

    public IEnumerator GetOnlineFriendList(string[] friendList)
    {
        if (this.lbc.CurrentRoom == null)
        {
            yield return new WaitForSeconds(4f);
        }

        List<string> fList = new List<string>();
        if(this.lbc.CurrentRoom != null)
        {
            foreach (int key in this.lbc.CurrentRoom.Players.Keys)
            {
                Player plInfo;
                this.lbc.CurrentRoom.Players.TryGetValue(key, out plInfo);
                Debug.Log(plInfo + key.ToString() + "---- room -----" + this.lbc.CurrentRoom);
                for (int i = 0; i < friendList.Length; i++)
                {
                    if (friendList[i] == plInfo.NickName)
                    {
                        fList.Add(plInfo.NickName);
                    }
                }
            }

            if (fList.Count > 0)
            {
                this.gameObject.GetComponent<UIFaceBookManager>().facebookPlay.GetComponent<UIFaceBookPlay>().ShowOnlinePlayer(fList);
            }
        }

        yield return new WaitForSeconds(0f);
    }

    public void GetFriendsList(string[] friendList)
    {
        StartCoroutine(GetOnlineFriendList(friendList));
    }
    /*
        public List<string> GetFriendsList(string[] friendList)
        {
            Debug.Log(this.lbc.CurrentRoom.Name + " ---- " + this.lbc.CurrentRoom.PlayerCount);

            List<string> fList = new List<string>();

            foreach (int key in this.lbc.CurrentRoom.Players.Keys)
            {
                Player plInfo;
                this.lbc.CurrentRoom.Players.TryGetValue(key, out plInfo);
                Debug.Log(plInfo.UserId);
                for (int i = 0; i < friendList.Length; i++)
                {
                    if (friendList[i] == plInfo.UserId)
                    {
                        fList.Add(plInfo.UserId);
                    }
                }
            }

            return fList;
        }
        */
    public void OnConnected()
    {
        Debug.Log("connected to server");
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        this.lbc.AuthValues.UserId = playerName;
        EnterRoomParams eParam = new EnterRoomParams();
        eParam.RoomName = "globalRoom";
        eParam.RoomOptions = new RoomOptions();
        eParam.RoomOptions.MaxPlayers = 100;
        //        eParam.PlayerProperties.

        this.lbc.OpJoinOrCreateRoom(eParam);    // joins any open room (no filter)
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected(" + cause + ")");
        //this.lbc.OpRejoinRoom("globalRoom");

        ConnectToServerWithPlayerId(playerName);
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionListReceived");
        regionHandler.PingMinimumOfRegions(this.OnRegionPingCompleted, null);
    }

    RoomInfo[] arryRoominfo;

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        arryRoominfo = new RoomInfo[roomList.Count];
        int i = 0;
        foreach (RoomInfo info in roomList)
        {
            arryRoominfo[i] = info;
            i++;
        }
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
    }

    public void OnJoinedLobby()
    {
    }

    public void OnLeftLobby()
    {
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("new player entered" + newPlayer);
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {

    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

    }


    public void OnPlayerLeftRoom(Player otherPlayer)
    {

    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {

    }

    public void OnCreatedRoom()
    {
        Debug.Log("oncreated room" + this.lbc.CurrentRoom.Players);
        this.lbc.LocalPlayer.NickName = playerName;
        this.lbc.CurrentRoom.AddPlayer(this.lbc.LocalPlayer);
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom" + this.lbc.CurrentRoom.Players);
        this.lbc.LocalPlayer.NickName = playerName;
        this.lbc.CurrentRoom.AddPlayer(this.lbc.LocalPlayer);        
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        this.lbc.OpCreateRoom(new EnterRoomParams());
    }

    public void OnLeftRoom()
    {

    }
    float requestTime = 0f;
    public void SendGamePlayRequest(string targetId)
    {
        byte eventCode = 1; // make up event codes at will
        requestSentState = true;
        requestTime = Time.time;
        JsonObject jObj = new JsonObject();
        jObj.Add("targetId", targetId);
        jObj.Add("myName", this.gameObject.GetComponent<UIManager>().playerInfo.playerName);
        jObj.Add("myFBId", playerName);
        //        this.lbc.OpRaiseEvent(eventCode, targetId, RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
        this.lbc.OpRaiseEvent(eventCode, jObj.ToString(), RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
    }

    public void RejectChallenge()
    {
        SendAcceptChallenge(false);
/*
        byte eventCode = 2; // make up event codes at will

        JsonObject jObj = new JsonObject();
        jObj.Add("targetId", this.gameObject.GetComponent<UIFaceBookManager>().chPlayerId);
        //        this.lbc.OpRaiseEvent(eventCode, targetId, RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
        this.lbc.OpRaiseEvent(eventCode, jObj.ToString(), RaiseEventOptions.Default, ExitGames.Client.Photon.SendOptions.SendReliable);
        */
        }

    /// <summary>A callback of the RegionHandler, provided in OnRegionListReceived.</summary>
    /// <param name="regionHandler">The regionHandler wraps up best region and other region relevant info.</param>
    private void OnRegionPingCompleted(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionPingCompleted " + regionHandler.BestRegion);
        Debug.Log("RegionPingSummary: " + regionHandler.SummaryToCache);
//        this.lbc.ConnectToRegionMaster(regionHandler.BestRegion.Code);
//        textIII.text += "regionhandler ---" + regionHandler.BestRegion.Code + " ---";
          this.lbc.ConnectToRegionMaster("us");
        }
}
