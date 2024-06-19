using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DalgonaUiManager : MonoBehaviour
{
    public Image tutorial;

    public Image ClearPanel;

    public TMP_Text ClearText;
    public DalgonaAtiveManager DalgonaSceneManager;
    public GameManager dalgonaManager;


    public void btn_click_start()
    {
        tutorial.gameObject.SetActive(false);
    }

    public void SetClear()
    {
        if (dalgonaManager.isClear)
        {
            ClearText.text = "GameClear";
        }
        else if(dalgonaManager.isOver)
        {
            ClearText.text = "GameOver";
        }
        ClearPanel.gameObject.SetActive(true);
        
    }

    public void btn_clicked_Exit()
    {
        DalgonaSceneManager.UnLoadScenceAdditive();
    }
}
