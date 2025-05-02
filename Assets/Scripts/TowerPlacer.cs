using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private PlaceTowers placeTowers;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private int towerCost = 50; // You can change this per tower

    private MoneySystem moneySystem;

    private void Start()
    {
        moneySystem = GameManager.Instance.moneySystem;
    }

    public void TryPlaceTower()
    {
        if (moneySystem == null)
        {
            Debug.LogError("MoneySystem reference not found.");
            return;
        }

        if (moneySystem.CanAfford(towerCost))
        {
            Vector3 position = GetPlacementPosition(); // You can modify this logic
            placeTowers.PlaceTower(towerPrefab, position);
            moneySystem.SubtractMoney(towerCost);
        }
        else
        {
            Debug.Log("Not enough money to place tower!");
        }
    }

    private Vector3 GetPlacementPosition()
    {
        // Placeholder logic — replace with real placement logic if needed
        return new Vector3(0, 2.2f, 0);
    }
}
