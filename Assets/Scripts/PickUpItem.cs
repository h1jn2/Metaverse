using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int itemCount;
    private GameObject gyeongdoMng;

    private void Start()
    {
        gyeongdoMng = GameObject.Find("Plane.001");
    }

    private void Update()
    {
        if (itemCount == 5)
        {
            gyeongdoMng.GetComponent<GyeondoManager>().SettingEndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            if (GetComponent<PlayerManager>().Pjob == PlayerManager.job.theif)
            {
                Destroy(other.gameObject);
                itemCount++;
            }
        }
    }
}
