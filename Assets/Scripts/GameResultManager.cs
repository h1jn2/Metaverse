using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultManager : MonoBehaviour
{
    public enum Team
    {
        Police,
        Thief
    }

    public Team playerTeam; // 플레이어의 팀

    public GameObject policeTeamWinObject;
    public GameObject policeTeamLoseObject;
    public GameObject thiefTeamWinObject;
    public GameObject thiefTeamLoseObject;

    public Button winButton;
    public Button loseButton;

    // TMP 텍스트 참조
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;

    // 깜빡임 관련 설정
    public Color blinkColor = Color.red; // 깜빡일 색상
    public float blinkInterval = 0.5f; // 깜빡임 간격
    public float fadeInDuration = 0.5f; // 페이드 인 시간
    public float fadeOutDuration = 0.5f; // 페이드 아웃 시간

    private Color[] originalColors; // 텍스트의 원래 색상을 저장할 배열
    private bool isBlinking = false; // 깜빡임 중인지 여부

    void Start()
    {
        // 초기화 - 모든 GameObject 비활성화
        SetGameObjectActive(policeTeamWinObject, false);
        SetGameObjectActive(policeTeamLoseObject, false);
        SetGameObjectActive(thiefTeamWinObject, false);
        SetGameObjectActive(thiefTeamLoseObject, false);

        // 버튼 클릭 이벤트 연결
        winButton.onClick.AddListener(() => ShowGameResult(true));
        loseButton.onClick.AddListener(() => ShowGameResult(false));

        // 텍스트의 원래 색상 저장
        originalColors = new Color[] { text1.color, text2.color, text3.color, text4.color };

        // TMP 텍스트 깜빡거리기 시작
        StartBlink();
    }

    private void SetGameObjectActive(GameObject gameObject, bool isActive)
    {
        // GameObject 활성화/비활성화
        gameObject.SetActive(isActive);

        // 페이드 인/아웃 Coroutine 시작
        if (isActive)
        {
            StartCoroutine(FadeIn(gameObject.GetComponent<CanvasGroup>(), fadeInDuration));
        }
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float currentTime = 0f;
        float startAlpha = 0f;
        float endAlpha = 1f;

        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = startAlpha;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, currentTime / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    public void ShowGameResult(bool isWin)
    {
        // 초기화 - 모든 GameObject 비활성화
        SetGameObjectActive(policeTeamWinObject, false);
        SetGameObjectActive(policeTeamLoseObject, false);
        SetGameObjectActive(thiefTeamWinObject, false);
        SetGameObjectActive(thiefTeamLoseObject, false);

        if (isWin)
        {
            // 플레이어 팀이 승리했을 때
            if (playerTeam == Team.Police)
            {
                SetGameObjectActive(policeTeamWinObject, true);
            }
            else if (playerTeam == Team.Thief)
            {
                SetGameObjectActive(thiefTeamWinObject, true);
            }
        }
        else
        {
            // 플레이어 팀이 패배했을 때
            if (playerTeam == Team.Police)
            {
                SetGameObjectActive(policeTeamLoseObject, true);
            }
            else if (playerTeam == Team.Thief)
            {
                SetGameObjectActive(thiefTeamLoseObject, true);
            }
        }
    }

    void StartBlink()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    IEnumerator BlinkRoutine()
    {
        while (isBlinking)
        {
            // 텍스트 색상을 깜빡임 색으로 변경
            BlinkText(text1);
            BlinkText(text2);
            BlinkText(text3);
            BlinkText(text4);

            // 대기
            yield return new WaitForSeconds(blinkInterval);

            // 원래 색상으로 변경
            text1.color = originalColors[0];
            text2.color = originalColors[1];
            text3.color = originalColors[2];
            text4.color = originalColors[3];

            // 대기
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void BlinkText(TMP_Text text)
    {
        text.color = blinkColor;
    }

    void StopBlink()
    {
        isBlinking = false;
    }
}
