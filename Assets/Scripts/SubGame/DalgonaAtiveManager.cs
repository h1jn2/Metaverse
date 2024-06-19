using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DalgonaAtiveManager : MonoBehaviour
{
    public static bool isDalgona;
    // Start is called before the first frame update

    public void LoadSceneAdditive()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Additive);
        isDalgona = true;

    }

    public void UnLoadScenceAdditive()
    {
        SceneManager.UnloadSceneAsync("SampleScene");
        isDalgona = false;
    }
}
