using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DalgonaAtiveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneAdditive()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Additive);

    }

    public void UnLoadScenceAdditive()
    {
        SceneManager.UnloadSceneAsync("SampleScene");
    }
}
