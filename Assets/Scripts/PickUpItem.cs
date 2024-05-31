using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            if (GetComponent<PlayerManager>().Pjob == PlayerManager.job.polic)
            {
                Item item = other.GetComponent<Item>();
                Destroy(other.gameObject);
            }
            else if (GetComponent<PlayerManager>().Pjob == PlayerManager.job.theif)
            {
                Item item = other.GetComponent<Item>();

                Destroy(other.gameObject);
            }
        }
    }
}
