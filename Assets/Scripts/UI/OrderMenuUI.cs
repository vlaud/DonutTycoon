using UnityEngine;

public class OrderMenuUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text description;
    public void ShowOrderMenu()
    {
        gameObject.SetActive(true);
    }

    public void HideOrderMenu()
    {
        gameObject.SetActive(false);
    }

    public void SetDescription(string text)
    {
        description.text = text;
    }
}
