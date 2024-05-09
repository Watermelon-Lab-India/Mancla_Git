using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System;
using Photon;

public class NetworkLocalManager : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{

    public string AppId; // set in inspector
    private LoadBalancingClient lbc;
    string str = "";
    void Start()
    {
        this.lbc = new LoadBalancingClient();
        this.lbc.AppId = "61071f33-f2e1-4076-ba7b-4539022f441b";
        this.lbc.AddCallbackTarget(this);
//        this.lbc.ConnectToNameServer();
    }

    void Update()
    {
        LoadBalancingClient client = this.lbc;
        if (client != null)
        {
            client.Service();
        }
    }

    public void OnConnected()
    {
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        this.lbc.OpJoinRandomRoom();    // joins any open room (no filter)
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected(" + cause + ")");
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

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
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

    public void OnCreatedRoom()
    {
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        EnterRoomParams eParam = new EnterRoomParams();
        eParam.RoomOptions = new RoomOptions();
        eParam.RoomOptions.MaxPlayers = 2;
        this.lbc.OpCreateRoom(eParam);
    }

    public void OnLeftRoom()
    {
    }
    /// <summary>A callback of the RegionHandler, provided in OnRegionListReceived.</summary>
    /// <param name="regionHandler">The regionHandler wraps up best region and other region relevant info.</param>
    private void OnRegionPingCompleted(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionPingCompleted " + regionHandler.BestRegion);
        Debug.Log("RegionPingSummary: " + regionHandler.SummaryToCache);
        this.lbc.ConnectToRegionMaster(regionHandler.BestRegion.Code);
    }
}
