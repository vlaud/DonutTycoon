using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// 현재 점수
    /// </summary>
    [SerializeField] private int currentScore = 0;

    /// <summary>
    /// 업그레이드 시 손님 점수 증가량
    /// </summary>
    [SerializeField] private int upgradeScore = 1;

    /// <summary>
    /// 다음 업그레이드를 위해 필요한 점수
    /// </summary>
    [SerializeField] private int requiredUpgradeScore = 10;

    [SerializeField] TMPro.TMP_Text currentScoreText;
    [SerializeField] TMPro.TMP_Text upgradeScoreText;
    [SerializeField] TMPro.TMP_Text requiredScoreText;

    private void Start()
    {
        UpdateScoreText();
        UpdateUpgradeScoreText();
        UpdateRequiredScoreText();
    }

    /// <summary>
    /// * 점수 증가 * 손님을 받을 때마다 점수를 증가시킴
    /// 
    /// </summary>
    public void AddScore()
    {
        currentScore += upgradeScore;
        UpdateScoreText();
    }

    /// <summary>
    /// * 점수 업그레이드 * 업그레이드 시 손님 점수를 증가시킴
    /// </summary>
    public void UpgradeScore()
    {
        if (currentScore < requiredUpgradeScore) return; // 업그레이드에 필요한 점수가 부족하면 리턴

        currentScore -= requiredUpgradeScore; // 점수 차감
        upgradeScore += 1; // 손님 점수 증가량 증가
        requiredUpgradeScore += 10; // 다음 업그레이드에 필요한 점수 증가
        
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        UpdateScoreText();
        UpdateUpgradeScoreText();
        UpdateRequiredScoreText();
    }

    /// <summary>
    ///  점수 UI 업데이트
    /// </summary>
    private void UpdateScoreText()
    {
        currentScoreText.text = currentScore.ToString();
    }

    private void UpdateUpgradeScoreText()
    {
        upgradeScoreText.text = upgradeScore.ToString();
    }

    private void UpdateRequiredScoreText()
    {
        requiredScoreText.text = requiredUpgradeScore.ToString();
    }
}
