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

    public Team playerTeam; // �÷��̾��� ��

    public GameObject policeTeamWinObject;
    public GameObject policeTeamLoseObject;
    public GameObject thiefTeamWinObject;
    public GameObject thiefTeamLoseObject;

    public Button winButton;
    public Button loseButton;

    // TMP �ؽ�Ʈ ����
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;

    // ������ ���� ����
    public Color blinkColor = Color.red; // ������ ����
    public float blinkInterval = 0.5f; // ������ ����
    public float fadeInDuration = 0.5f; // ���̵� �� �ð�
    public float fadeOutDuration = 0.5f; // ���̵� �ƿ� �ð�

    private Color[] originalColors; // �ؽ�Ʈ�� ���� ������ ������ �迭
    private bool isBlinking = false; // ������ ������ ����

    void Start()
    {
        // �ʱ�ȭ - ��� GameObject ��Ȱ��ȭ
        SetGameObjectActive(policeTeamWinObject, false);
        SetGameObjectActive(policeTeamLoseObject, false);
        SetGameObjectActive(thiefTeamWinObject, false);
        SetGameObjectActive(thiefTeamLoseObject, false);

        // ��ư Ŭ�� �̺�Ʈ ����
        winButton.onClick.AddListener(() => ShowGameResult(true));
        loseButton.onClick.AddListener(() => ShowGameResult(false));

        // �ؽ�Ʈ�� ���� ���� ����
        originalColors = new Color[] { text1.color, text2.color, text3.color, text4.color };

        // TMP �ؽ�Ʈ �����Ÿ��� ����
        StartBlink();
    }

    private void SetGameObjectActive(GameObject gameObject, bool isActive)
    {
        // GameObject Ȱ��ȭ/��Ȱ��ȭ
        gameObject.SetActive(isActive);

        // ���̵� ��/�ƿ� Coroutine ����
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
        // �ʱ�ȭ - ��� GameObject ��Ȱ��ȭ
        SetGameObjectActive(policeTeamWinObject, false);
        SetGameObjectActive(policeTeamLoseObject, false);
        SetGameObjectActive(thiefTeamWinObject, false);
        SetGameObjectActive(thiefTeamLoseObject, false);

        if (isWin)
        {
            // �÷��̾� ���� �¸����� ��
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
            // �÷��̾� ���� �й����� ��
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
            // �ؽ�Ʈ ������ ������ ������ ����
            BlinkText(text1);
            BlinkText(text2);
            BlinkText(text3);
            BlinkText(text4);

            // ���
            yield return new WaitForSeconds(blinkInterval);

            // ���� �������� ����
            text1.color = originalColors[0];
            text2.color = originalColors[1];
            text3.color = originalColors[2];
            text4.color = originalColors[3];

            // ���
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
