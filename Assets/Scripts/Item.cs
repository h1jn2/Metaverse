using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    PoliceItem,
    ThiefItem
}

public class Item : MonoBehaviour
{
    public ItemType itemType;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }
}
