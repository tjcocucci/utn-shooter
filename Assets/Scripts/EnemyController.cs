using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform;
    public float minDistanceToPlayer = 5;
    public int enemyKills = 0;
    private float timeForNextSpawn;
    public float timeBetweenSpawns = 5;
    public Enemy enemyPrefab;
    public GameObject spawnPlane;
    private Bounds planeBounds;
    private float enemyHeight;

    // Start is called before the first frame update
    void Start() {     // Assuming the planeObject has a MeshRenderer component
         planeBounds = spawnPlane.GetComponent<MeshRenderer>().bounds;
         enemyHeight = enemyPrefab.GetComponent<MeshRenderer>().bounds.extents.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeForNextSpawn)
        {
            Spawn();
            timeForNextSpawn = Time.time + timeBetweenSpawns;
        }
    }

    void Spawn()
    {
        Vector3 spawnPosition = Vector3.zero;
        while (true)
        {
            spawnPosition = new Vector3(
                Random.Range(planeBounds.min.x, planeBounds.max.x),
                enemyHeight,
                Random.Range(planeBounds.min.z, planeBounds.max.z)
            );
            Debug.Log(spawnPosition);
            if (Vector3.Distance(spawnPosition, playerTransform.position) > minDistanceToPlayer)
            {
                break;
            }
        }
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
