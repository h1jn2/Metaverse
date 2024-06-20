using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string Scene_2; // �������� �̵��� ���� �̸�
    public float transitionDuration = 1.0f; // ��ȯ �ð�

    public Button button; // public���� �����Ͽ� �ν����Ϳ��� �Ҵ� ����

    void Start()
    {
        if (button != null)
        {
            // ��ư�� Ŭ�� �̺�Ʈ ������ �߰�
            button.onClick.AddListener(ChangeScene);
        }
        else
        {
            
        }
    }

    void ChangeScene()
    {
        // ���� ���� �񵿱������� �ε�
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        // ���� ���� �񵿱������� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene_2, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false; // �� Ȱ��ȭ�� ������Ŵ

        // ��ȯ �ð� ���� ���
        float elapsedTime = 0;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ε�� ���� Ȱ��ȭ
        asyncLoad.allowSceneActivation = true;
    }
}
