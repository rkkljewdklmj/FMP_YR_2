using UnityEngine;

public class PlaceTowers : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] Camera mainCamera; // Assign your main camera in the Inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MOUSE CLICKED");

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 placePosition = hit.point;
                placePosition.y = 0.5f; // Adjust height if needed

                Instantiate(tower, placePosition, Quaternion.identity);
            }
        }
    }
}
