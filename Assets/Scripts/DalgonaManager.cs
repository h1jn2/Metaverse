using UnityEngine;

public class DalgonaManager : MonoBehaviour
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
            Debug.LogError("dalgonaPrefabs �迭�� �������� �����ϴ�.");
            return;
        }

        int randomIndex = Random.Range(0, dalgonaPrefabs.Length);
        randomIndex = Mathf.Clamp(randomIndex, 0, dalgonaPrefabs.Length - 1);
        currentDalgona = Instantiate(dalgonaPrefabs[randomIndex], transform.position, Quaternion.identity);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
