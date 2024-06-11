using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonColorChange : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;
    public Color targetColor; // ��ư�� Ŭ���Ǿ��� �� ��ǥ ����
    public float transitionDuration = 0.5f; // ���� ��ȯ �ð�

    private Color originalColor; // ��ư�� �⺻ ����
    private bool isTransitioning = false; // ���� ��ȯ �� ����

    void Start()
    {
        // ��ư�� �⺻ ���� ����
        originalColor = buttonText.color;

        // ���� ��ȯ ����
        isTransitioning = true;
    }

    void Update()
    {
        // ���� ��ȯ ���� ���� �����Ͽ� ���� ����
        if (isTransitioning)
        {
            float t = Mathf.PingPong(Time.time / transitionDuration, 1f);
            buttonText.color = Color.Lerp(originalColor, targetColor, t);
        }
    }
}
