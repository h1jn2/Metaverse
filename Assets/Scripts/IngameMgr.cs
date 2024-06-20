using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMgr : MonoBehaviour
{
    public static IngameMgr single { get; private set; }

    [SerializeField] private CanvasGroup panelScene1;
    [SerializeField] private CanvasGroup panelScene2;


    public void Awake()
    {
        single = this;
    }

    public void OpenScene2()
    {
        FadeManager.Out(panelScene1);
        FadeManager.In(panelScene2);
    }

    public void OpenScene1()
    {
        FadeManager.Out(panelScene2);
        FadeManager.In(panelScene1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("게임종료");
    }
}
