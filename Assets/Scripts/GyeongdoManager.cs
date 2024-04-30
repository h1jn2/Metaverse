using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GyeondoManager : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float curTime;

    private int second;
    [SerializeField]
    private List<GameObject> playerCollisions;
    private int player_cnt;

    private bool isPlaying;
    private PlayerManager pm;
    private IEnumerator startGameTimer;

    void Start()
    {
        startGameTimer = GameStartTimer();
    }

    void Update()
    {
        if (playerCollisions.Count > 0)
        {
            time = 3;
            StartCoroutine(startGameTimer);
        }
    }

    IEnumerator GameStartTimer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            second = (int)curTime % 60;

            Debug.Log(second);

            yield return null;

            if (curTime <= 0)
            {
                Debug.Log("경도 시작");
                StartGyeondo();
                curTime = 0;
                yield break;
            }
        }
    }

    private void StartGyeondo()
    {
        isPlaying = true;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollisions.Add(collision.gameObject);
            
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollisions.Remove(collision.gameObject);

        }
    }


}
