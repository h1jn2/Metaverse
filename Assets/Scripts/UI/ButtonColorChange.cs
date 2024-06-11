using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonColorChange : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;
    public Color targetColor; // 버튼이 클릭되었을 때 목표 색상
    public float transitionDuration = 0.5f; // 색상 전환 시간

    private Color originalColor; // 버튼의 기본 색상
    private bool isTransitioning = false; // 색상 전환 중 여부

    void Start()
    {
        // 버튼의 기본 색상 저장
        originalColor = buttonText.color;

        // 색상 전환 시작
        isTransitioning = true;
    }

    void Update()
    {
        // 색상 전환 중일 때만 보간하여 색상 변경
        if (isTransitioning)
        {
            float t = Mathf.PingPong(Time.time / transitionDuration, 1f);
            buttonText.color = Color.Lerp(originalColor, targetColor, t);
        }
    }
}
