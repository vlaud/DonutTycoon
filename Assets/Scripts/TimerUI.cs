using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    /// <summary>
    /// 남은 시간을 표시할 이미지
    /// </summary>
    public Image progressBar;
    /// <summary>
    /// 남은 시간을 표시할 텍스트
    /// </summary>
    public TMP_Text timerText;
    private iTimerObserver target;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);

    /// <summary>
    /// 초기 시간
    /// </summary>
    [SerializeField] private float duration = 10f;
    /// <summary>
    /// 남은 시간
    /// </summary>
    [SerializeField] private float remainingTime = 0f;
    private Coroutine countdownCoroutine = null;
    private bool isReleased = false;
    public bool IsReleased => isReleased;

    public void SetUpTimerUI()
    {
        // Initialize the CoroutineRunner
    }
    
    /// <summary>
    /// 개체에 연결
    /// </summary>
    /// <param name="target">개체</param>
    /// <param name="duration">남은 시간</param>
    public void AttachTo(iTimerObserver target, float duration)
    {
        this.target = target;
        this.targetTransform = target.GetTransform();
        this.duration = duration;
        remainingTime = duration;
    }

    public void OnActive()
    {
        if (countdownCoroutine != null) return;

        Debug.Log($"Timer activated for target: {targetTransform.name},  {gameObject.name}");
        this.StartOrRestartCoroutine(ref countdownCoroutine, Countdown());
    }

    private IEnumerator Countdown()
    {
        while (remainingTime > Mathf.Epsilon)
        {
            if (target == null) yield break;
            // TimerUI 위치를 2d 좌표계에 맞게 변환
            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
            // 렉트 트랜스폼을 사용하여 UI의 위치를 설정합니다.
            transform.position = screenPos;

            remainingTime -= Time.deltaTime;
            // Update the timer UI image
            progressBar.fillAmount = remainingTime / duration;

            // Update the timer text
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();

            // Wait for the next frame
            yield return null;
        }
        Debug.Log($"ended countdown for target: {targetTransform.name},  {gameObject.name}");
        target.OnTimerFinished();
    }

    /// <summary>
    /// 타이머 UI를 초기화 후 풀에 반환합니다. 
    /// </summary>
    public void OnRelease()
    {
        this.StopCurrentCoroutine(ref countdownCoroutine);
        
        target = null;
        targetTransform = null;
        remainingTime = 0f;
        progressBar.fillAmount = 1f;
        timerText.text = string.Empty;
    }

    public void SetIsReleased(bool isReleased)
    {
        this.isReleased = isReleased;
    }
}
