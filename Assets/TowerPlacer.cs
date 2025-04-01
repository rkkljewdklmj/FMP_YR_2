using UnityEngine;


public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private PlaceTowers placeTowers;
    [SerializeField] private MoneySystem moneySystem; // Reference to the MoneySystem

    public void TryPlaceTower(GameObject towerPrefab, int cost)
    {
        if (moneySystem.CanAfford(cost)) // Check if the player has enough money
        {
            Vector3 position = GetPlacementPosition(); // Get valid position
            placeTowers.PlaceTower(towerPrefab, position); // Call PlaceTower method
            moneySystem.SubtractMoney(cost); // Subtract cost
        }
        else
        {
            Debug.Log("Not enough money to place tower!");
        }
    }

    private Vector3 GetPlacementPosition()
    {
        // Implement logic to determine where the tower should be placed.
        // Placeholder: Return a fixed position for now.
        return new Vector3(0, 0.5f, 0);
    }
}
