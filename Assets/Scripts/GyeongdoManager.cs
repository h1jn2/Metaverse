using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GyeondoManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerCollisions;
    [SerializeField]
    private float playTime = 1000f;
    private bool isPlaying;
    private float time = 0;

    public static List<GameObject> Shuffle(List<GameObject> values)
    {
        System.Random rand = new System.Random();
        var shuffled = values.OrderBy(_ => rand.Next()).ToList();

        return shuffled;
    }


    private void Update()
    {
        if (playerCollisions.Count > 0)
        {
            StartTimer(5f);
        }
    }

    private void StartTimer(float setTimer)
    {
        time += Time.deltaTime;

        if (time > setTimer)
        {
            if (!isPlaying)
            {
                SettingStartGame();
            }
            else
            {
                SettingEndGame();
            }
            
        }
    }

    private void SettingStartGame()
    {
        Debug.Log("SettingStartGame()");

        List<GameObject> shufflePlayer = Shuffle(playerCollisions);

        for (int i = 0; i < shufflePlayer.Count; i++)
        {
            if (i % 2 == 0)
            {
                shufflePlayer[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.polic;
            }
            else
            {
                shufflePlayer[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.theif;
            }
            shufflePlayer[i].GetComponent<PlayerManager>().Pstatus = PlayerManager.status._hideseek;
        }
        

        StartGyeondo();

    }

    private void StartGyeondo()
    {
        Debug.Log("StartGyeongdo()");

        isPlaying = true;
        time = 0;
        StartCoroutine(RandomRespawn.instance.spawnCoroutine);
        StartTimer(playTime);

    }

    private void SettingEndGame()
    {
        Debug.Log("SettingEndGame()");

        for (int i = 0; i < playerCollisions.Count; i++)
        {
            playerCollisions[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.none;
            playerCollisions[i].GetComponent<PlayerManager>().Pstatus = PlayerManager.status._none;
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i]);
        }
        time = 0;
        isPlaying = false;
        StopCoroutine(RandomRespawn.instance.spawnCoroutine);
        playerCollisions.Clear();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollisions.Add(collision.gameObject);
            collision.gameObject.GetComponent<PlayerManager>().Pstatus = PlayerManager.status._ready;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollisions.Remove(collision.gameObject);
            collision.gameObject.GetComponent<PlayerManager>().Pstatus = PlayerManager.status._none;
            
        }

    }
}


