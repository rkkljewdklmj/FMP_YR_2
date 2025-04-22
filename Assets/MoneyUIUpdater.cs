using TMPro;
using UnityEngine;

public class MoneyUIUpdater : MonoBehaviour
{
    public static MoneyUIUpdater Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }




    private void Start()
    {
        if (MoneySystem.Instance != null)
        {
            UpdateMoneyUI(MoneySystem.Instance.CurrentMoney);
        }
    }

    public void UpdateMoneyUI(int amount)
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + amount;
        }
    }
}
