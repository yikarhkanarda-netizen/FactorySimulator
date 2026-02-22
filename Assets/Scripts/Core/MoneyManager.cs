using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int Money { get; private set; }

    public static event Action<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
            Instance = this;
    }

    public void AddMoney(int amount, Vector3 pos)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Cannot add negative money.");
            return;
        }
        Money += amount;

        OnMoneyChanged?.Invoke(Money);
        PopUpManager.Instance.SpawnGoldPopUp(amount, pos);
        Debug.Log($"Money added: {amount}. Total money: {Money}");
    }

    public bool SpendMoney(int amount, Vector3 pos)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Cannot spend negative money.");
            return false;
        }
        if (Money >= amount)
        {
            Money -= amount;

            OnMoneyChanged?.Invoke(Money);
            PopUpManager.Instance.SpawnGoldPopUp(-amount, pos);
            Debug.Log($"Money spent: {amount}. Remaining money: {Money}");
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough money to spend.");
            return false;
        }
    }
}
