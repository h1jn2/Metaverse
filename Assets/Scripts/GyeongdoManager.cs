using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using Unity.Properties;

public class GyeondoManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerCollisions;
    private float playTime = 300f;
    private bool isPlaying;
    [SerializeField]
    private float time = 0;
    private int CountTime = 0;
    public bool isWatting;
    public bool isPoliceWin;
    public bool isGameEnd;
    
    public GameObject UIManager;

    private int thiefCount;

    public GameObject randomRespawn;
    private IEnumerator runningCoroutine = null;

    public static List<GameObject> Shuffle(List<GameObject> values)
    {
        System.Random rand = new System.Random();
        var shuffled = values.OrderBy(_ => rand.Next()).ToList();

        return shuffled;
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (playerCollisions.Count > 0 && !isPlaying)
        {
            StartTimer(5f);
            isWatting = true;
        }
        else
        {
            isWatting = false;
        }
        if (isPlaying)
        {
            StartTimer(playTime);
        }
        if (thiefCount != 0 && Character_Controller.catchCount == thiefCount)
        {
            isPoliceWin = true;
            SettingEndGame();
        }
    }

    private void StartTimer(float setTimer)
    {
        time += Time.deltaTime;
        CountTime = Mathf.FloorToInt(time);
        Debug.Log(CountTime);

        if (time > setTimer)
        {
            if (!isPlaying)
            {
                SettingStartGame();
            }
            else
            {
                SettingEndGame();
                isPoliceWin = false;
            }
            
        }
    }

    private void SettingStartGame()
    {
        Debug.Log("SettingStartGame()");

        List<GameObject> shufflePlayer = Shuffle(playerCollisions);

        for (int i = 0; i < shufflePlayer.Count; i++)
        {
            PhotonView Setpv = shufflePlayer[i].GetPhotonView();
            if (i % 2 == 0)
            {
                Setpv.RPC("SetJob_RPC",RpcTarget.All, PlayerManager.job.polic);
                //shufflePlayer[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.polic;
            }
            else
            {
                Setpv.RPC("SetJob_RPC",RpcTarget.All, PlayerManager.job.theif);
                //shufflePlayer[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.theif;
                thiefCount++;
            }
            Setpv.RPC("SetStatus_RPC",RpcTarget.All,PlayerManager.status._hideseek);
            Setpv.RPC("StartUI_RPC",RpcTarget.All,true);
            Setpv.RPC("SetUI_RPC",RpcTarget.All,shufflePlayer[i].GetComponent<PlayerManager>().Pjob.ToString(),shufflePlayer[i].GetComponent<PickUpItem>().itemCount.ToString(),"03");

            //shufflePlayer[i].GetComponent<PlayerManager>().Pstatus = PlayerManager.status._hideseek;
        }


        StartGyeondo();

    }

    private void StartGyeondo()
    {
        Debug.Log("StartGyeongdo()");
        isPlaying = true;
        time = 0;
        runningCoroutine = randomRespawn.GetComponent<RandomRespawn>().SpawnPrefabs();

        StartCoroutine(runningCoroutine);
    }

    public void SettingEndGame()
    {
        Debug.Log("SettingEndGame()");
        isGameEnd = true;
        
        for (int i = 0; i < playerCollisions.Count; i++)
        {
            PhotonView Setpv = playerCollisions[i].GetPhotonView();
            Setpv.RPC("SetJob_RPC",RpcTarget.All,PlayerManager.job.none);
            Setpv.RPC("SetStatus_RPC",RpcTarget.All,PlayerManager.status._none);
            Setpv.RPC("SetUI_RPC",RpcTarget.All,"Job","0","00");
            Setpv.RPC("StartUI_RPC",RpcTarget.All,false);
            Setpv.RPC("SetUI_GameResult", RpcTarget.All,isPoliceWin, isGameEnd);
            
            
            //playerCollisions[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.none;
            //playerCollisions[i].GetComponent<PlayerManager>().Pstatus = PlayerManager.status._none;
            playerCollisions[i].GetComponent<PickUpItem>().itemCount = 0;
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i++)
        {
            PhotonView ipv = items[i].GetPhotonView();
            ipv.RPC("Destroy_RPC",RpcTarget.All);
            //Destroy(items[i]);
        }
        time = 0;
        thiefCount = 0;
        Character_Controller.catchCount = 0;
        isPlaying = false;
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        playerCollisions.Clear();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isPlaying)
            {
                playerCollisions.Add(collision.gameObject);
                collision.gameObject.GetComponent<PlayerManager>().Pstatus = PlayerManager.status._ready;
            }
           
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isPlaying)
            {
                playerCollisions.Remove(collision.gameObject);
                collision.gameObject.GetComponent<PlayerManager>().Pstatus = PlayerManager.status._none;
            }
            
        }

    }
}


