using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance { get; private set; }

    [SerializeField] private int startingMoney = 100;
    public int CurrentMoney { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CurrentMoney = startingMoney;
    }

    public bool CanAfford(int amount)
    {
        return CurrentMoney >= amount;
    }

    public void SubtractMoney(int amount)
    {
        CurrentMoney -= amount;
        MoneyUIUpdater.Instance?.UpdateMoneyUI(CurrentMoney);
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        MoneyUIUpdater.Instance?.UpdateMoneyUI(CurrentMoney);
    }
}
