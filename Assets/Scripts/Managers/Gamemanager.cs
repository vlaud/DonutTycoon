using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    private static Gamemanager instance;
    public static Gamemanager Instance => instance;
    [SerializeField] private CustomerSpawner customerSpawner;
    [SerializeField] private GetInLineManager getInLineManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private ClickManager clickManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private SoundManager soundManager;

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
}
