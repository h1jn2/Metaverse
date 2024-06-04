using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviourPun
{
    public PhotonView pv;
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

    }

    void Update()
    {
        if (pv.IsMine)
        {

        }
    }
}