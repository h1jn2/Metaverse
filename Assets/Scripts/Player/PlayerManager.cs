using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviourPun
{
    public PhotonView pv;
    public Canvas UI;
    public enum status
    {
        _none,
        _ready,
        _playing,
        _sugargame,
        _hideseek
    }
    public enum job
    {
        none,
        polic,
        theif
    }

    public status Pstatus;
    public job Pjob;

    void Start()
    {
        /*
        if (GetComponent<PhotonView>().IsMine)
        {
            this.gameObject.name += "(LocalPlayer)";
            
        }
        else
        {
            this.gameObject.name += "(OtherPlayer)";
            UI.gameObject.SetActive(false);
        }
        */
    }

    void Update()
    {
    }

    [PunRPC]
    public void SetJob_RPC(job input)
    {
        this.Pjob = input;
    }
    [PunRPC]
    public void SetStatus_RPC(status input)
    {
        this.Pstatus = input;
    }

    [PunRPC]
    public void SetUI_RPC(string JobInput, string ItemCount,string time)
    {
        this.transform.GetChild(5).GetComponent<GyeongdoUIManger>().SetGyeongdoUI(JobInput,ItemCount,time);
    }

    [PunRPC]
    public void SetWait_RPC(bool isWating)
    {
        if (pv.IsMine)
        {
            Debug.Log(isWating);
            this.transform.GetChild(5).GetComponent<GyeongdoUIManger>().SetCountDownUI(isWating);
        }
    }

    [PunRPC]
    public void StartUI_RPC(bool isStart)
    {
            if (pv.IsMine)
            {
                this.transform.GetChild(5).GetComponent<GyeongdoUIManger>().isStart = isStart;
            }
    }
    [PunRPC]
    public void SetUI_GameResult(bool _isPoliceWin, bool _isGameEnd)
    {
        if (pv.IsMine)
        {
            this.transform.GetChild(5).GetComponent<GyeongdoUIManger>().SetGameResultUI(_isPoliceWin, _isGameEnd);
        }
    }
}