using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] dalgonaPrefabs;
    private GameObject currentDalgona;
    public bool isStart;
    public bool isClear;
    public bool isOver;
    public DalgonaUiManager UiManager;

    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
        isStart = false;
        isClear = false;
        isOver = false;
    }

    public void btn_clicked_Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
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
        isStart = true;
    }

    public void GameClear()
    {
        isClear = true;
        Destroy(currentDalgona);
        UiManager.SetClear();
    }

    public void GameOver()
    {
        isOver = true;
        Destroy(currentDalgona);
        UiManager.SetClear();
        Debug.Log("Game Over");
    }
}
