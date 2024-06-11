using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class SeesawManager : MonoBehaviour
{
    public GameObject SeeSaw;
    public Animator SeesawAnim;
    
    
    private void Awake()
    {
        SeesawAnim = this.gameObject.GetComponent<Animator>();
    }

    private void ChangeStatus()
    {
        
    }
}
