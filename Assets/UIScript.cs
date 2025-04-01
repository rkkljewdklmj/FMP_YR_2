using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text Money;
    public  int currentMoney;

    private MoneySystem moneySystem;
    public void Start()
    {
        currentMoney = moneySystem.GetCurrentMoney();

        Money.text = currentMoney.ToString();
    }

    public void Update()
    {
        currentMoney = moneySystem.GetCurrentMoney();

        Debug.Log(currentMoney + "YAY");

        Money.text = currentMoney.ToString();
    }

}
