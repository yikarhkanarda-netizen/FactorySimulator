using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        MoneyManager.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable()
    {
        MoneyManager.OnMoneyChanged -= UpdateMoneyText;
    }

    private void UpdateMoneyText(int newAmount)
    {
        moneyText.text = $"$: {newAmount}";
    }
}
