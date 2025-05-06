using UnityEngine;

public class PlaceTowers : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask placeableLayer; // Ground layer
    [SerializeField] LayerMask unplaceableLayer; // Blocked layers (towers and paths)
    [SerializeField] float towerRadius = 1f;
    [SerializeField] float towerRange = 5f;
    [SerializeField] private PlaceTowers placeTowers;       // Reference to your PlaceTowers script
    [SerializeField] private MoneySystem moneySystem;

    [SerializeField] private GameObject ghostTower;
    private GameObject towerToPlace;
    private GameObject rangeIndicator;
    private Tower tower;



    void Update()
    {
        if (ghostTower == null) return;

        // Move the ghost tower based on mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeableLayer))
        {
            Vector3 placePosition = hit.point;
            placePosition.y = 2.2f; // Adjust the height as needed
            ghostTower.transform.position = placePosition;

            // Redraw the line renderer to match the position of the ghost tower
            if (rangeIndicator != null)
            {
                UpdateRangeIndicatorPosition();
            }

            // Check if placement is valid
            bool isValid = !Physics.CheckSphere(placePosition, towerRadius, unplaceableLayer);
            SetGhostColor(isValid);
        }


        // Place tower on click
        if (Input.GetMouseButtonDown(0) && ghostTower != null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeableLayer))
            {
                Vector3 placePosition = hit.point;
                placePosition.y = 2.2f;

                if (!Physics.CheckSphere(placePosition, towerRadius, unplaceableLayer))
                {

                 
                    
                    PlaceTower(towerToPlace, placePosition);
                }
            }
        }
    }
    public void TryPlaceTower(GameObject towerPrefab)
    {
        MonoBehaviour towerScript = towerPrefab.GetComponent<MonoBehaviour>();

        int towerCost = 0;

        // Try to get a cost from Tower or TowerTwo
        if (towerPrefab.GetComponent<Tower>() != null)
        {
            towerCost = towerPrefab.GetComponent<Tower>().cost;
        }
        else if (towerPrefab.GetComponent<TowerTwo>() != null)
        {
            towerCost = towerPrefab.GetComponent<TowerTwo>().cost;
        }
        else
        {
            Debug.LogError("Tower prefab does not have a recognized Tower script.");
            return;
        }

        if (MoneySystem.Instance.CanAfford(towerCost))
        {
            Vector3 position = GetPlacementPosition();
            placeTowers.PlaceTower(towerPrefab, position);
            MoneySystem.Instance.SubtractMoney(towerCost);
        }
        else
        {
            Debug.Log("Not enough money to place tower!");
        }
    }

    private Vector3 GetPlacementPosition()
    {
        // You must replace this with your real placement logic
        // For now, this is just a placeholder
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return new Vector3(hit.point.x,2.2f, hit.point.z); // Adjust Y as needed
        }

        return Vector3.zero; // Default if raycast fails
    }


    public void PlaceTower(GameObject towerToPlace, Vector3 placePosition)
    {
        GameObject instantiatedTower = Instantiate(towerToPlace, placePosition, Quaternion.identity);

        Tower towerScript = instantiatedTower.GetComponent<Tower>();
        if (towerScript != null)
        {
            towerScript.disablefire = false;
        }

        TowerTwo towerTwoScript = instantiatedTower.GetComponent<TowerTwo>();
        if (towerTwoScript != null)
        {
            towerTwoScript.disablefire = false;
        }

        Destroy(ghostTower);
        Destroy(rangeIndicator);

        ghostTower = null;
        this.towerToPlace = null;
    }



    public void SelectTowerFromButton(GameObject towerPrefab)
    {
        float defaultRange = 5f;
        SelectTower(towerPrefab, defaultRange);
    }

    public void SelectTower(GameObject towerPrefab, float range)
    {
        // Destroy old ghost if it exists
        if (ghostTower != null)
        {
            if (ghostTower.scene.IsValid()) // Only destroy if instantiated in the scene
            {
                Destroy(ghostTower);
            }
            ghostTower = null;
        }

        towerToPlace = towerPrefab;

        // Create ghost tower preview
        ghostTower = Instantiate(towerPrefab);
        ghostTower.layer = LayerMask.NameToLayer("Ignore Raycast");

        // Disable collider to prevent interactions with ghost
        Collider ghostCollider = ghostTower.GetComponent<Collider>();
        if (ghostCollider != null)
        {
            ghostCollider.enabled = false;
        }

       

        //DisableScripts(ghostTower); // Disable unnecessary scripts

        // Create and attach range indicator
        CreateRangeIndicator(ghostTower);
    }

    private void DisableScripts(GameObject obj)
    {

        //tower.firePoint = null;
        //tower.disablefire = true;

        /*
        MonoBehaviour[] scripts = obj.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (!(script is Transform))
                script.enabled = false; // Disable all scripts
        }
        */
    }

    private void SetGhostColor(bool isValid)
    {
        Color color = isValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);

        foreach (Renderer renderer in ghostTower.GetComponentsInChildren<Renderer>())
        {
            if (renderer == null) continue;

            // Check if this is an instantiated object in the scene
            if (ghostTower.scene.IsValid())
            {
                renderer.material.color = color;  // Safe to modify instance material
            }
            else
            {
                //renderer.sharedMaterial.color = color;  // Modify prefab material (if needed)
            }
        }
    }

    private void CreateRangeIndicator(GameObject ghost)
    {
        // Create the range indicator as a separate object
        rangeIndicator = new GameObject("RangeIndicator");

        // Add a LineRenderer component to create the circular range indicator
        LineRenderer lr = rangeIndicator.AddComponent<LineRenderer>();
        lr.loop = true;
        lr.positionCount = 50;
        lr.startWidth = 0.5f;  // Increased width for visibility
        lr.endWidth = 0.5f;

        // Create a circular shape for the range indicator
        for (int i = 0; i < 50; i++)
        {
            float angle = i * Mathf.PI * 2f / 50;
            Vector3 position = new Vector3(Mathf.Cos(angle) * towerRange, 0.8f, Mathf.Sin(angle) * towerRange);
            lr.SetPosition(i, position);
        }

        // Set the material to an unlit, colored material to make it visible
        Material rangeMaterial = new Material(Shader.Find("Unlit/Color"));
        rangeMaterial.SetColor("_Color", new Color(0, 1, 1, 0.5f));  // Cyan color with transparency

        lr.material = rangeMaterial;
        lr.enabled = true;  // Ensure LineRenderer is enabled

        // Debug: Confirm that range indicator was created and is not null
        Debug.Log("Range Indicator Created: " + (rangeIndicator != null ? "Yes" : "No"));
    }

    private void UpdateRangeIndicatorPosition()
    {
        if (rangeIndicator == null) return;

        LineRenderer lr = rangeIndicator.GetComponent<LineRenderer>();

        // Update the positions of the line renderer to match the ghost tower's position
        for (int i = 0; i < 50; i++)
        {
            float angle = i * Mathf.PI * 2f / 50;
            Vector3 position = new Vector3(Mathf.Cos(angle) * towerRange, 0.8f, Mathf.Sin(angle) * towerRange);

            // Offset position to the ghost tower's position
            position += ghostTower.transform.position;

            lr.SetPosition(i, position);
        }
    }
}
