using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

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

    public static List<GameObject> Shuffle(List<GameObject> values)
    {
        System.Random rand = new System.Random();
        var shuffled = values.OrderBy(_ => rand.Next()).ToList();

        return shuffled;
    }

    void Start()
    {
        startGameTimer = GameTimerCoroutine();


    }

    void Update()
    {
        if (playerCollisions.Count > 0)
        {
            time = 3;
            StartCoroutine(startGameTimer);
        }
    }

    IEnumerator GameTimerCoroutine()
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
                SettingGame();
                curTime = 0;
                yield break;
            }
        }
    }

    private void SettingGame()
    {
        isPlaying = true;
        List<GameObject> shufflePlayer = Shuffle(playerCollisions);

        for (int i = 0; i < shufflePlayer.Count; i++) {
            if (i == shufflePlayer.Count % 2 - 1)
            {
                shufflePlayer[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.polic;
            }
            else
            {
                shufflePlayer[i].GetComponent<PlayerManager>().Pjob = PlayerManager.job.theif;
            }
            shufflePlayer[i].GetComponent<PlayerManager>().Pstatus = PlayerManager.status._playing;
        }

    }



    private void StartGyeondo()
    {
        
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


