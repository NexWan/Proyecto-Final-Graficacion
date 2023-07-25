using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenSpawner : MonoBehaviour
{
    public GameObject chickenPrefab;
    public int numberOfChickens = 10;
    public float spawnRadius = 20f;
    public float obstacleCheckRadius = 5f;

    void Start()
    {
        SpawnChickens();
    }

    public void SpawnChickens()
    {
        for (int i = 0; i < numberOfChickens; i++)
        {
            Vector3 randomSpawnPoint = GetRandomPointOnNavMesh(transform.position, spawnRadius);
            GameObject newChicken = Instantiate(chickenPrefab, randomSpawnPoint, Quaternion.identity);
            newChicken.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float radius)
    {
        int maxAttempts = 10;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
            {
                // Check for obstacles around the random point
                Collider[] colliders = Physics.OverlapSphere(randomPoint, obstacleCheckRadius);
                bool foundObstacle = false;
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Obstacle"))
                    {
                        foundObstacle = true;
                        break;
                    }
                }

                if (!foundObstacle)
                {
                    return hit.position;
                }
            }

            attempts++;
        }

        // If no valid point is found after maxAttempts, return center position
        return center;
    }
}