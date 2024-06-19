using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GyeongdoUIManger : MonoBehaviour
{
    private PhotonView pv;

    public TextMeshProUGUI jobText;
    public TextMeshProUGUI itemCountText;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        Debug.Log("jobText: " + (jobText != null));
        Debug.Log("itemCountText: " + (itemCountText != null));
        Debug.Log("timerText: " + (timerText != null));
    }

    public void SetGyeongdoUI(string job, string itemCount, string time)
    {
        jobText.text = job;
        itemCountText.text = itemCount;
        timerText.text = time;

    }
}
