using UnityEngine;

public class ChickenDestroyer : MonoBehaviour
{
    public float raycastRadius = 30f; // The radius of the raycast from the camera center
    public float detectionDistance = 2f; // The distance at which the chicken can be detected
    public LayerMask chickenLayer; // LayerMask for the chicken GameObjects
    public string chickenLayerName = "ChickenLayer";

    private Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        chickenLayer = LayerMask.GetMask(chickenLayerName);
    }
    
    public void DestroyChicken()
    {
        // Create a ray from the camera center
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;
        // Cast a ray and check if it hits anything on the chicken layer
        if (Physics.Raycast(ray, out hit, detectionDistance, chickenLayer))
        {
            // If the chicken is hit, destroy the chicken GameObject
            Destroy(hit.collider.gameObject);
            updateChicken();
        }
    }

    void updateChicken()
    {
        GameManager.Instance.updateChickens();
    }
}