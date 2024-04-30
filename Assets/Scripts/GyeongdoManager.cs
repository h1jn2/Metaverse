using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GyeondoManager : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float curTime;

    private int second;
    private GameObject[] player;

    private bool isPlaying;

    void Start()
    {
    }

    void Update()
    {



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

    private void OnCollisionStay(Collision other)
    {

    }



}
