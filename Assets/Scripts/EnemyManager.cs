using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform playerTransform;
    public int enemyKills;
    public Enemy enemyPrefab;
    private float enemyHeight;
    public event System.Action<int> OnLevelCleared;
    private int currentLevelIndex;
    private Level currentLevel;
    private int numberOfSpawnedEnemies;
    private LevelManager levelManager;
    private Bounds planeBounds;
    private float timeForNextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.Instance;
        currentLevel = levelManager.currentLevel;
        currentLevelIndex = levelManager.currentLevelIndex;
        timeForNextSpawn = Time.time + currentLevel.timeBetweenSpawns;
        enemyHeight = enemyPrefab.GetComponent<MeshRenderer>().bounds.extents.y;
        planeBounds = currentLevel.spawnBounds;
        enemyKills = 0;
        numberOfSpawnedEnemies = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (
            Time.time > timeForNextSpawn
            && numberOfSpawnedEnemies < currentLevel.totalNumberOfEnemies
        )
        {
            Spawn();
            numberOfSpawnedEnemies++;
            timeForNextSpawn = Time.time + currentLevel.timeBetweenSpawns;
        }
    }

    void Spawn()
    {
        Vector3 spawnPosition = Vector3.zero;

        bool foundUsableSpawnPosition = false;
        for (int i = 0; i < 500; i++)
        {
            spawnPosition = new Vector3(
                Random.Range(planeBounds.min.x, planeBounds.max.x),
                enemyHeight * 4,
                Random.Range(planeBounds.min.z, planeBounds.max.z)
            );
            if (
                playerTransform != null
                && Vector3.Distance(spawnPosition, playerTransform.position)
                    > currentLevel.minSpawnDistanceToPlayer
            )
            {
                foundUsableSpawnPosition = true;
                break;
            }
        }
        if (!foundUsableSpawnPosition)
        {
            Debug.Log("Could not find a spawn position");
            return;
        }
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        SetUpEnemyProperties(enemy);
    }

    void SetUpEnemyProperties(Enemy enemy)
    {
        enemy.speed = currentLevel.enemySpeed;
        enemy.damage = currentLevel.enemyDamage;
        enemy.totalHealth = currentLevel.enemyHealth;
        enemy.timeBetweenShots = currentLevel.enemyTimeBetweenShots;
        enemy.playerTransform = playerTransform;
        enemy.OnObjectDied += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        enemyKills++;
        if (enemyKills == currentLevel.totalNumberOfEnemies)
        {
            Invoke("NextLevel", 1);
        }
    }

    void NextLevel()
    {
        if (OnLevelCleared != null)
        {
            OnLevelCleared(currentLevelIndex);
            if (currentLevelIndex < levelManager.levels.Length - 1)
            {
                Start();
            }
        }
    }
}
