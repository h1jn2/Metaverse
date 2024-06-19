using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] dalgonaPrefabs;
    private GameObject currentDalgona;

    void Start()
    {
        SpawnDalgona();
    }

    void SpawnDalgona()
    {
        if (currentDalgona != null)
            Destroy(currentDalgona);

        if (dalgonaPrefabs.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, dalgonaPrefabs.Length);
        randomIndex = Mathf.Clamp(randomIndex, 0, dalgonaPrefabs.Length - 1);
        currentDalgona = Instantiate(dalgonaPrefabs[randomIndex], transform.position, Quaternion.identity);
    }

    public void GameClear()
    {
        Debug.Log("Game Clear");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
