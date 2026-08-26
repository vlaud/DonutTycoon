using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private OrderMenuUI orderMenuUI;
    [SerializeField] private AudioClip clickSoundClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orderMenuUI.HideOrderMenu();
    }

    public void ShowOrderMenu(string text)
    {
        orderMenuUI.ShowOrderMenu();
        orderMenuUI.SetDescription(text);
    }

    public void HideOrderMenu()
    {
        orderMenuUI.HideOrderMenu();
    }

    public void Confirm()
    {
        var customer = Gamemanager.GetClickManager.LastClickedTarget.GetComponent<Customer>();
        customer.ChangeState(CustomerState.Leave);
        Gamemanager.GetSoundManager.PlayOneShot(clickSoundClip);

        // 점수 추가
        Gamemanager.GetScoreManager.AddScore();
    }

    public void Cancel()
    {
        Gamemanager.GetClickManager.NotifyClickMissed();
    }
}
