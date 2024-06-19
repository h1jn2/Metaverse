using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string Scene_2; // 다음으로 이동할 씬의 이름
    public float transitionDuration = 1.0f; // 전환 시간

    private Button button;

    void Start()
    {
        button = GetComponent<Button>(); // 버튼 컴포넌트 가져오기

        // 버튼에 클릭 이벤트 리스너 추가
        button.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        // 다음 씬을 비동기적으로 로드
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        // 다음 씬을 비동기적으로 로드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene_2, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false; // 씬 활성화를 지연시킴

        // 전환 시간 동안 대기
        float elapsedTime = 0;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 로드된 씬을 활성화
        asyncLoad.allowSceneActivation = true;
    }
}
