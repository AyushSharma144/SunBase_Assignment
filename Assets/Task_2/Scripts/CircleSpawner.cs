using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    //defining variables related to object size,position and type
    public int ObjectCount;

    [SerializeField]
    private GameObject CirclePrefab;
    [SerializeField]
    private Vector2 ScaleRange;
    [SerializeField]
    private float MinDistance;

    private Camera cam;
    private List<Vector3> spawnedPositions = new List<Vector3>();

    private void Start()
    {
        cam = Camera.main;
        SpawnCircles();
    }

    private void SpawnCircles()
    {
        //spawning prefabs, and changing their position and scale randomly
        for (int i = 0; i < ObjectCount; i++)
        {
            Transform circle = Instantiate(CirclePrefab).transform;
            circle.position = GetValidPosition();
            circle.localScale = Vector2.one * Random.Range(ScaleRange.x, ScaleRange.y);
            // saving the location of all spawned objects in a list
            spawnedPositions.Add(circle.position);
        }
    }
    //a function that randomises location based on other objects
    private Vector3 GetValidPosition()
    {
        Vector3 potentialPosition;
        do
        {
            potentialPosition = GenerateRandomPosition();
        }
        while (IsOverlapping(potentialPosition));

        return potentialPosition;
    }
    //a function that returns random vector value under the viewport range
    private Vector3 GenerateRandomPosition()
    {
        Vector3 randomViewportPos = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 0);
        Vector3 worldPos = cam.ViewportToWorldPoint(randomViewportPos);
        worldPos.z = 0;
        return worldPos;
    }
    //a function that checks for overlappig between objects
    private bool IsOverlapping(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < MinDistance)
            {
                return true;
            }
        }
        return false;
    }
}
