using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum ItemType
{
    Treasure,
}

public class Item : MonoBehaviourPun
{
    public ItemType itemType;
    private PhotonView pv;

    private void Awake()
    {
        pv = this.gameObject.GetPhotonView();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }

    [PunRPC]
    public void Destroy_RPC()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}
