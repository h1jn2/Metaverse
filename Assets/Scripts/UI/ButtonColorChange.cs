using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonColorChange : MonoBehaviour
{
    public Button button1;
    public TMP_Text buttonText1;
    public Color targetColor1; // 첫 번째 버튼이 클릭되었을 때 목표 색상
    public float transitionDuration1 = 0.5f; // 첫 번째 버튼 색상 전환 시간

    public Button button2;
    public TMP_Text buttonText2;
    public Color targetColor2; // 두 번째 버튼이 클릭되었을 때 목표 색상
    public float transitionDuration2 = 0.5f; // 두 번째 버튼 색상 전환 시간

    private Color originalColor1; // 첫 번째 버튼의 기본 색상
    private Color originalColor2; // 두 번째 버튼의 기본 색상
    private bool isTransitioning1 = false; // 첫 번째 버튼 색상 전환 중 여부
    private bool isTransitioning2 = false; // 두 번째 버튼 색상 전환 중 여부

    void Start()
    {
        // 첫 번째 버튼의 기본 색상 저장
        originalColor1 = buttonText1.color;

        // 두 번째 버튼의 기본 색상 저장
        originalColor2 = buttonText2.color;

        // 색상 전환 시작 (임시로 둘 다 true로 설정)
        isTransitioning1 = true;
        isTransitioning2 = true;
    }

    void Update()
    {
        // 첫 번째 버튼 색상 전환
        if (isTransitioning1)
        {
            float t1 = Mathf.PingPong(Time.time / transitionDuration1, 1f);
            buttonText1.color = Color.Lerp(originalColor1, targetColor1, t1);
        }

        // 두 번째 버튼 색상 전환
        if (isTransitioning2)
        {
            float t2 = Mathf.PingPong(Time.time / transitionDuration2, 1f);
            buttonText2.color = Color.Lerp(originalColor2, targetColor2, t2);
        }
    }
}
