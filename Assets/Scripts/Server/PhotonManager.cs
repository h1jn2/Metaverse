using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> list_Prefabs;
    public UserData LocalDate;
    public Transform spawn_point;
    public GameObject obj_local;
    public static PhotonManager instance;
    public bool isLoading;
    public static AsyncOperation SceneLoingsync;
    public bool is_Master;


    //게임을 실행중 포톤매니저는 무조건 하나만 있어야되기때문에 싱클톤으로 실행
    private void Awake()
    {
        isLoading = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (instance== null)
        {
            instance = this;
            DontDestroyOnLoad(this);    
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

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
        InitPhoton();
    }

    //서버설정초기화
    private void InitPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("서버연결전 초기화");
            PhotonNetwork.ConnectUsingSettings();
            
        }
    }

    //방생성
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.CreateRoom("Metaverse", roomOptions, TypedLobby.Default);
    }
    
    //방입장
    private void JoinRoom()
    {
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom("Metaverse", roomOption, TypedLobby.Default);
    }

    //스테이지에 플레이어 생성
    private void m_CreatePlayer(UserData m_data)
    {
        if (m_data.gender == 0)
        {
            obj_local = PhotonNetwork.Instantiate(list_Prefabs[0].name, spawn_point.position, Quaternion.identity);
        }
        else if (m_data.gender == 1)
        {
            obj_local = PhotonNetwork.Instantiate(list_Prefabs[0].name, spawn_point.position, Quaternion.identity);
        }
    }

    //마스터 클라이언트가 방을 생성시 스테이지씬 로딩
    private void LoadArea()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("마스터 클라이언트가 아닙니다.");
            return;
        }

        SceneLoingsync = SceneManager.LoadSceneAsync("Scenes/World");
    }

    public GameObject SpawnItem(Vector3 position)
    {
        return PhotonNetwork.Instantiate(list_Prefabs[1].name, position, Quaternion.identity);
    }

    private void Spawn_item()
    {
        
    }
    

    #region ServerCallBacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("포톤 마스터 서버 접속완료");
        
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("포톤 로비 접속완료");
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable(){{"RoomState", "Waiting"}});
            LoadArea();
            OnStartCreatePlayer();
            is_Master = true;
        }
        if (!PhotonNetwork.IsMasterClient)
        {
            m_CreatePlayer(LocalDate);
            is_Master = false;
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("새인원 진입");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("플레이어 퇴장");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
    }

    #endregion
    

    #region  buttonMethod

    public void btn_click_Mainstart()
    {
        PhotonNetwork.JoinLobby();
    }

    public void btn_click_createRoom()
    {
        CreateRoom();
    }

    public void btn_click_joinroom()
    {
        JoinRoom();
    }

    public void btn_click_StageStart()
    {
        
    }

    #endregion
    
    #region 코루틴

    private Coroutine _coroutineCreatePlayer;
    private void OnStartCreatePlayer()
    {
        if(_coroutineCreatePlayer != null)
            StopCoroutine(_coroutineCreatePlayer);

        _coroutineCreatePlayer = StartCoroutine(IEnum_CreatePlayer());
    }

    
    IEnumerator IEnum_CreatePlayer()
    {
            
        int cnt=0;
        Debug.Log("코루틴 시작");
        while (SceneLoingsync.progress < 1f)
        {
            if (cnt > 10000)
            {
                Debug.LogError("스폰불가");
                yield break;
            }
            Debug.Log(SceneLoingsync.progress);
            cnt++;
            yield return null;
        }
        
        m_CreatePlayer(LocalDate);
        Debug.Log("생성");
        _coroutineCreatePlayer = null;
        yield break;
    }

    #endregion
}

[System.Serializable] 
public class UserData
{
    public int gender;
    public int type;
    public string userid;

    public UserData()
    {
        gender = 0;
        type = 0;
        userid = "";
    }
}
