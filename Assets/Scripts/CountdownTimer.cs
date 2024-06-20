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

    public float countdownSpeed = 1.0f; // ī��Ʈ�ٿ� �ӵ� ���� (1.0f�� �⺻ �ӵ�, 0.5f�� �� �� ���� �ӵ�)

    private int countdownTime = 5; //ī��Ʈ�ٿ� �ð� ����
    private float gameTimer = 180f; //���� �÷��� �ð� ����
    private bool gameStarted = false;

    void Start()
    {
        // �ʱ� ����
        countdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        // ��ư Ŭ�� ������ �߰�
        startButton.onClick.AddListener(StartCountdown);
    }

    void StartCountdown()
    {
        startButton.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        for (int i = countdownTime; i > 0; i--)
        {
            if (i <= 3)
            {
                countdownText.color = countdownWarningColor; // 3�� ������ �� ��� ����
            }
            else
            {
                countdownText.color = countdownStartColor; // �⺻ ����
            }

            countdownText.text = i.ToString();
            yield return StartCoroutine(FadeTextInAndOut());
        }

        countdownText.gameObject.SetActive(false);
        StartGame();
    }

    IEnumerator FadeTextInAndOut()
    {
        // �ؽ�Ʈ ���̵� ��
        for (float t = 0.01f; t < 1; t += Time.deltaTime / countdownSpeed)
        {
            countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, t);
            yield return null;
        }

        // 1�� ��� (ī��Ʈ�ٿ� �ӵ��� ���� ����)
        yield return new WaitForSeconds(1f / countdownSpeed);

        // �ؽ�Ʈ ���̵� �ƿ�
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
        timerText.color = timerStartColor; // Ÿ�̸� ���� ����
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
                timerText.color = timerWarningColor; // 10�� ������ �� ��� ����
            }

            yield return null;
        }

        GameOver();
    }

    void GameOver()
    {
        gameStarted = false;
        timerText.text = "00:00";
        // ���� ���� ���� �߰�
    }
}
