using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleScript : MonoBehaviour
{
    public Transform player; // Reference to the player
    public GameObject floor; // Reference to the plane (floor)
    public int points = 1; // Points per collectible

    private Vector3 areaSize;

    private void Start()
    {
        // Calculate the area size based on the floor's bounds
        if (floor != null)
        {
            Renderer floorRenderer = floor.GetComponent<Renderer>();
            if (floorRenderer != null)
            {
                Vector3 floorSize = floorRenderer.bounds.size;
                areaSize = new Vector3(floorSize.x, 0, floorSize.z);
            }
            else
            {
                Debug.LogWarning("Floor does not have a Renderer component.");
            }
        }
        else
        {
            Debug.LogWarning("Floor GameObject not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points
            ScoreManager.instance.AddPoints(points);

            // Move to a new random position within the calculated area
            transform.position = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 floorCenter = floor.transform.position;
        Vector3 randomPos = new Vector3(
            Random.Range(floorCenter.x - areaSize.x / 2, floorCenter.x + areaSize.x / 2),
            transform.position.y,
            Random.Range(floorCenter.z - areaSize.z / 2, floorCenter.z + areaSize.z / 2)
        );
        return randomPos;
    }
}
