using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> list_Prefabs;
    public Transform tf_Respawn_Point;
    public UserData LocalPlayer_Data;
    public GameObject obj_local;

    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Clear();
        if (pool != null && list_Prefabs != null)
        {
            foreach (var prefab in list_Prefabs)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
        }
        InitServer();
    } 
    private void InitServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("포톤서버불러오기");
        }
    }
    private void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom("Metaverse", roomOptions, TypedLobby.Default);
    }

    private void CreatePlayer(UserData m_data)
    {
        if (m_data.gender == 0)
        {
            obj_local = PhotonNetwork.Instantiate(list_Prefabs[0].name, tf_Respawn_Point.position, Quaternion.identity);
        }
        else if (m_data.gender == 1)
        {
            obj_local = PhotonNetwork.Instantiate(list_Prefabs[1].name, tf_Respawn_Point.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("성별선택불가");
        }
    }

    #region override server callbacks

    //마스트서버 콜백
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //로비 콜백
    public override void OnJoinedLobby()
    {
        JoinRoom();
    }
    public override void OnLeftLobby()
    {
        Debug.Log("로비퇴장");
    }
    //룸 콜백
    public override void OnCreatedRoom()
    {
        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    public override void OnJoinedRoom()
    {
        CreatePlayer(LocalPlayer_Data);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }

    public override void OnLeftRoom()
    {
        
    }
    
    //플레이어 콜백
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }

    #endregion
}
[System.Serializable]
public class UserData
{
    public int gender;
    public string userid;

    public UserData()
    {
        gender = 0;
        userid = "";
    }
}
