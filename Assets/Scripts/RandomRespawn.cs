using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRespawn : MonoBehaviour
{
    public static RandomRespawn instance;
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject prefab;
    public LayerMask terrainLayer;
    public IEnumerator spawnCoroutine;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
        spawnCoroutine = SpawnPrefabs();
    }

    public IEnumerator SpawnPrefabs()
    {
        while (true)
        {
            float range_X = rangeCollider.bounds.size.x;
            float range_Z = rangeCollider.bounds.size.z;
            Vector3 randomPosition = new Vector3(Random.Range(-range_X / 2, range_X / 2), 100f, Random.Range(-range_Z / 2, range_Z / 2));

            RaycastHit hit;
            if (Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
            {
                // 충돌 지점에 프리팹 생성
                Debug.Log("hit");
                Instantiate(prefab, hit.point, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("지형을 감지하지 못했습니다. 프리팹을 생성하지 않습니다." + randomPosition);
            }

            yield return new WaitForSeconds(5f);
        }
        
    }
}
