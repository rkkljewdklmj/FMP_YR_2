using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private GameManager gameManager;
    private int startingMoney = gameManager.startingMoney;
    private int currentMoney;

    void Start()
    {
        // Set starting money when the game starts
        gameManager.SpendMoney
    }

    // Check if the player can afford a tower
    public bool CanAfford(int cost)
    {
        return currentMoney >= cost;
    }

    // Subtract money when the player places a tower
    public void SubtractMoney(int cost)
    {
        if (CanAfford(cost))
        {
            currentMoney -= cost;
            Debug.Log("Money deducted: " + cost);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    // Get the current amount of money
    public int GetCurrentMoney()
    {
        return currentMoney;
    }
}
