using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public Button startButton;
    public TMP_Text countdownText;
    public TMP_Text timerText;

    public Color countdownStartColor = Color.white;
    public Color countdownWarningColor = Color.red;
    public Color timerStartColor = Color.white;
    public Color timerWarningColor = Color.red;

    public float countdownSpeed = 1.0f; // 카운트다운 속도 조정 (1.0f가 기본 속도, 0.5f는 두 배 빠른 속도)

    private int countdownTime = 5; //카운트다운 시간 변경
    private float gameTimer = 180f; //게임 플레이 시간 변경
    private bool gameStarted = false;

    void Start()
    {
        // 초기 설정
        countdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        // 버튼 클릭 리스너 추가
        startButton.onClick.AddListener(StartCountdown);
    }

    void StartCountdown()
    {
        startButton.gameObject.SetActive(false); // 버튼 비활성화
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        for (int i = countdownTime; i > 0; i--)
        {
            if (i <= 3)
            {
                countdownText.color = countdownWarningColor; // 3초 이하일 때 경고 색상
            }
            else
            {
                countdownText.color = countdownStartColor; // 기본 색상
            }

            countdownText.text = i.ToString();
            yield return StartCoroutine(FadeTextInAndOut());
        }

        countdownText.gameObject.SetActive(false);
        StartGame();
    }

    IEnumerator FadeTextInAndOut()
    {
        // 텍스트 페이드 인
        for (float t = 0.01f; t < 1; t += Time.deltaTime / countdownSpeed)
        {
            countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, t);
            yield return null;
        }

        // 1초 대기 (카운트다운 속도에 따라 조정)
        yield return new WaitForSeconds(1f / countdownSpeed);

        // 텍스트 페이드 아웃
        for (float t = 1.0f; t > 0; t -= Time.deltaTime / countdownSpeed)
        {
            countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, t);
            yield return null;
        }
    }

    void StartGame()
    {
        gameStarted = true;
        timerText.gameObject.SetActive(true);
        timerText.color = timerStartColor; // 타이머 시작 색상
        StartCoroutine(GameTimerCoroutine());
    }

    IEnumerator GameTimerCoroutine()
    {
        while (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTimer / 60);
            int seconds = Mathf.FloorToInt(gameTimer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (gameTimer <= 10)
            {
                timerText.color = timerWarningColor; // 10초 이하일 때 경고 색상
            }

            yield return null;
        }

        GameOver();
    }

    void GameOver()
    {
        gameStarted = false;
        timerText.text = "00:00";
        // 게임 종료 로직 추가
    }
}
