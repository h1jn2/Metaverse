using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RandomRespawn : MonoBehaviour
{
    public static RandomRespawn instance;
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject prefab;
    public LayerMask terrainLayer;
    [SerializeField]
    private List<GameObject> item_prefabs;

    private PhotonView pv;
    

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        pv = this.gameObject.GetPhotonView(); 
        
    }

    public IEnumerator SpawnPrefabs()
    {
        while (PhotonManager.instance.is_Master)
        {
            float range_X = rangeCollider.bounds.size.x; 
            float range_Z = rangeCollider.bounds.size.z;

            Vector3 randomPosition = new Vector3(Random.Range(-range_X / 2, range_X / 2), 100f, Random.Range(-range_Z / 2, range_Z / 2));

            RaycastHit hit;
            if (Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
            { 
                if (item_prefabs.Count < 3)
                {
                    // 충돌 지점에 프리팹 생성
                    Debug.Log("hit");
                    //PhotonManager.instance.SpawnItem(new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z));
                    item_prefabs.Add(PhotonManager.instance.SpawnItem(new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z)));
                    yield return new WaitForSeconds(15f);
                    }
                    else
                    {
                        PhotonNetwork.Destroy(item_prefabs[0]);
                        item_prefabs.RemoveAt(0);
                    }
                
                }
                else 
                { 
                    Debug.LogWarning("지형을 감지하지 못했습니다. 프리팹을 생성하지 않습니다." + randomPosition);
            }                 
        }
    }
}
