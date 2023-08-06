using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player playerPrefab;
    public Level[] levels;
    public int currentLevelIndex;
    public Level currentLevel;
    public Player player;
    public bool playerIsAlive;
    private EnemyManager enemyManager;

    public event System.Action OnWin;

    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get { return _instance; }
    }

    private void OnEnable()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        foreach (Level level in levels)
        {
            level.gameObject.SetActive(false);
        }
    }

    void OnPlayerDeath()
    {
        playerIsAlive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerIsAlive = true;
        enemyManager = EnemyManager.Instance;
        enemyManager.OnLevelCleared += LoadLevel;
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex == levels.Length - 1 && playerIsAlive)
        {
            if (OnWin != null)
            {
                OnWin();
            }
        }
        else
        {
            StartLevel(levelIndex);
        }
    }

    public void StartLevel(int i)
    {
        if (i >= levels.Length)
        {
            Debug.LogError("Level index out of range!");
            return;
        }
        if (i == 0)
        {
            if (player != null)
            {
                Debug.Log("Destroying player");
                Destroy(player.gameObject);
            }
            player = Instantiate(playerPrefab, levels[i].playerSpawnPosition, Quaternion.identity);
        }

        if (currentLevel != null)
        {
            currentLevel.gameObject.SetActive(false);
        }   
        currentLevelIndex = i;
        currentLevel = levels[currentLevelIndex];
        currentLevel.gameObject.SetActive(true);
        Debug.Log("Level: " + currentLevelIndex);
    }

    public void RestartGame()
    {
        StartLevel(0);
        enemyManager.SetUp();
        enemyManager.RemoveEnemies();
    }
}
