using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviourPun, IPunObservable, IPunInstantiateMagicCallback
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Pstatus);
            stream.SendNext(Pjob);
        }
        else if (stream.IsReading)
        {
            stream.ReceiveNext();
            stream.ReceiveNext();
        }
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        info.Sender.TagObject = gameObject;
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

    public void InvokeProperties()
    {
        Pstatus = Pstatus;
        Pjob = Pjob;
    }
}
