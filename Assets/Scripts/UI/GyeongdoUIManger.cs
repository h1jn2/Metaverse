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
    public TMP_Text CountText;
    public bool isStart;
    public bool isWatting;
    public bool isPause;
    
    private float time =0;
    private float sectime =0;
    private int CountTime = 0;
    private int CountMin = 0;
    private int CountSec = 0;
    private int WaittingCount;

    public CanvasGroup policeWinUI;
    public CanvasGroup thiefWinUI;
    //public GameObject OkButton;
    public Canvas inGameUI;
    public Canvas resultUI;
    public Canvas CountUI;
    public Canvas PauseUI;

    private void Awake()
    {
        Debug.Log("jobText: " + (jobText != null));
        Debug.Log("itemCountText: " + (itemCountText != null));
        Debug.Log("timerText: " + (timerText != null));
        resultUI.gameObject.SetActive(false);
        //policeWinUI.gameObject.SetActive(false);
        //thiefWinUI.gameObject.SetActive(false);
        //OkButton.SetActive(false);
    }

    private void Update()
    {
        
        if (isWatting && !isStart)
        {  
            time += Time.deltaTime;
            WaittingCount = 5-Mathf.FloorToInt(time);
            CountText.text = WaittingCount.ToString();

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
        else if(!isWatting)
        {
            time = 0f;
            CountTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            PauseUI.gameObject.SetActive(true);
            isPause = true;
        }
    }

    public void SetGyeongdoUI(string job, string itemCount, string time)
    {
        if (isStart)
        {
            Debug.Log("인게임 UI");
            inGameUI.gameObject.SetActive(true);
            CountUI.gameObject.SetActive(false);
            jobText.text = job;
            itemCountText.text = itemCount;
            timerText.text = time;
        }
    }

    public void SetCountDownUI(bool isWating)
    {
        isWatting = isWating;
        CountUI.gameObject.SetActive(isWating);
    }
    public void SetGameResultUI(bool _isPoliceWin, bool _isGameEnd)
    {
        Debug.LogWarning("경도 끝");
        if (_isGameEnd)
        {
            resultUI.gameObject.SetActive(true);
            if (_isPoliceWin)
            {
                policeWinUI.gameObject.SetActive(true);
                thiefWinUI.gameObject.SetActive(false);
                //OkButton.SetActive(true);
                inGameUI.gameObject.SetActive(false);
            }
            else
            {
                policeWinUI.gameObject.SetActive(false);
                thiefWinUI.gameObject.SetActive(true);
                //OkButton.SetActive(true);
                inGameUI.gameObject.SetActive(false);
            }
        }
    }

    public void onClick_OK_Button()
    {
        resultUI.gameObject.SetActive(false);
    }

    public void BtnClickQuit()
    {
        Application.Quit();
    }
    public void BtnClickMain()
    {
        PhotonManager.instance.LeaveRoom();
    }

    public void BtnClickReturn()
    {
        PauseUI.gameObject.SetActive(false);
        isPause = false;
    }
}
