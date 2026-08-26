using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    private static Gamemanager instance;
    public static Gamemanager Instance => instance;
    [Header("매니저들")]
    [SerializeField] private CustomerSpawner customerSpawner;
    [SerializeField] private GetInLineManager getInLineManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private ClickManager clickManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private TimerManager timerManager;

    [Header("게임 오브젝트들")]
    [SerializeField] private Canvas uiCanvas;

    private void Awake()
    {
        instance = this;
    }

    public static CustomerSpawner GetCustomerSpawner => instance.customerSpawner;
    public static GetInLineManager GetInLineManager => instance.getInLineManager;
    public static UIController GetUIController => instance.uiController;
    public static ClickManager GetClickManager => instance.clickManager;
    public static ScoreManager GetScoreManager => instance.scoreManager;
    public static UpgradeManager GetUpgradeManager => instance.upgradeManager;
    public static SoundManager GetSoundManager => instance.soundManager;
    public static TimerManager GetTimerManager => instance.timerManager;


    public static Canvas GetUICanvas => instance.uiCanvas;

    public static void StopAllCoroutinesOnGameOver()
    {
        instance.customerSpawner.StopAllCoroutinesOnGameOver();
        instance.getInLineManager.StopAllCoroutines();
        instance.uiController.StopAllCoroutines();
        instance.clickManager.StopAllCoroutines();
        instance.scoreManager.StopAllCoroutines();
        instance.upgradeManager.StopAllCoroutines();
        instance.soundManager.StopAllCoroutines();
        instance.timerManager.StopAllCoroutines();

        Debug.Log("All coroutines stopped on game over.");
    }
}
