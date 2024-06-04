using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int itemCount;

    private void Update()
    {
        if (itemCount == 5)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Item"))
        {
            if (GetComponent<PlayerManager>().Pjob == PlayerManager.job.theif)
            {
                Item item = other.GetComponent<Item>();

                Destroy(other.gameObject);
                itemCount++;
                Debug.Log(itemCount);

            }
        }
    }
}
