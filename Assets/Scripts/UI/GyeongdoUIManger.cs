using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GyeongdoUIManger : MonoBehaviour
{
    private PhotonView pv;

    public TMP_Text jobText;
    public TMP_Text itemCountText;
    public TMP_Text timerText;
    public TMP_Text MinText;
    public TMP_Text SecText;
    public bool isStart;
    public bool isWatting;
    
    private float time =0;
    private float sectime =0;
    private int CountTime = 0;
    private int CountMin = 0;
    private int CountSec = 0;
    private int WaittingCount;

    public GameObject policeWinUI;
    public GameObject thiefWinUI;
    public GameObject OkButton;
    public GameObject inGameUI;
    public GameObject resultUI;

    private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        Debug.Log("jobText: " + (jobText != null));
        Debug.Log("itemCountText: " + (itemCountText != null));
        Debug.Log("timerText: " + (timerText != null));
        policeWinUI.SetActive(false);
        thiefWinUI.SetActive(false);
        OkButton.SetActive(false);
    }

    private void Update()
    {
        
        if (isWatting && !isStart)
        {
            time += Time.deltaTime;
            WaittingCount = 5-Mathf.FloorToInt(time);
        }
        else
        {
            time = 0f;
            WaittingCount = 0;
        }
        if (isStart)
        {
            time += Time.deltaTime;
            sectime += Time.deltaTime;
            CountTime = 300-Mathf.FloorToInt(time);
            CountMin = CountTime / 60;
            
            if (sectime >= 60)
            {
                sectime = 0;
                CountSec = 0;
            }
            else
            {
                CountSec = 60 - Mathf.FloorToInt(sectime);
            }
            //CountTime = Mathf.FloorToInt(time);
            //SecText.text = (60-CountTime).ToString();
            SecText.text = CountSec.ToString();
            MinText.text = CountMin.ToString();
        }
        else
        {
            time = 0f;
            CountTime = 0;
        }
    }

    public void SetGyeongdoUI(string job, string itemCount, string time)
    {
        if (isStart)
        {
            inGameUI.SetActive(true);
            jobText.text = job;
            itemCountText.text = itemCount;
            timerText.text = time;
        }
    }

    public void SetGameResultUI(bool _isPoliceWin, bool _isGameEnd)
    {
        if (_isGameEnd)
        {
            if (_isPoliceWin)
            {
                policeWinUI.SetActive(true);
                thiefWinUI.SetActive(false);
                OkButton.SetActive(true);
                inGameUI.SetActive(false);
            }
            else
            {
                policeWinUI.SetActive(false);
                thiefWinUI.SetActive(true);
                OkButton.SetActive(true);
                inGameUI.SetActive(false);
            }
        }
    }

    public void onClick_OK_Button()
    {
        resultUI.SetActive(false);
    }
}
