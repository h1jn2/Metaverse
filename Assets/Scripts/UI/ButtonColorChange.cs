using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonColorChange : MonoBehaviour
{
    public Button button1;
    public TMP_Text buttonText1;
    public Color targetColor1; // ù ��° ��ư�� Ŭ���Ǿ��� �� ��ǥ ����
    public float transitionDuration1 = 0.5f; // ù ��° ��ư ���� ��ȯ �ð�

    public Button button2;
    public TMP_Text buttonText2;
    public Color targetColor2; // �� ��° ��ư�� Ŭ���Ǿ��� �� ��ǥ ����
    public float transitionDuration2 = 0.5f; // �� ��° ��ư ���� ��ȯ �ð�

    private Color originalColor1; // ù ��° ��ư�� �⺻ ����
    private Color originalColor2; // �� ��° ��ư�� �⺻ ����
    private bool isTransitioning1 = false; // ù ��° ��ư ���� ��ȯ �� ����
    private bool isTransitioning2 = false; // �� ��° ��ư ���� ��ȯ �� ����

    void Start()
    {
        // ù ��° ��ư�� �⺻ ���� ����
        originalColor1 = buttonText1.color;

        // �� ��° ��ư�� �⺻ ���� ����
        originalColor2 = buttonText2.color;

        // ���� ��ȯ ���� (�ӽ÷� �� �� true�� ����)
        isTransitioning1 = true;
        isTransitioning2 = true;
    }

    void Update()
    {
        // ù ��° ��ư ���� ��ȯ
        if (isTransitioning1)
        {
            float t1 = Mathf.PingPong(Time.time / transitionDuration1, 1f);
            buttonText1.color = Color.Lerp(originalColor1, targetColor1, t1);
        }

        // �� ��° ��ư ���� ��ȯ
        if (isTransitioning2)
        {
            float t2 = Mathf.PingPong(Time.time / transitionDuration2, 1f);
            buttonText2.color = Color.Lerp(originalColor2, targetColor2, t2);
        }
    }
}
