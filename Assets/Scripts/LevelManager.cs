using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public int currentLevelIndex;
    public Level currentLevel;
    private bool playerIsAlive;

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
        StartLevel(currentLevelIndex);
    }

    void OnPlayerDeath()
    {
        playerIsAlive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerIsAlive = true;
        FindObjectOfType<EnemyManager>().OnLevelCleared += NextLevel;
    }

    public void NextLevel(int levelIndex)
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
            currentLevel.gameObject.SetActive(false);
            StartLevel(levelIndex + 1);
        }
    }

    public void StartLevel(int i)
    {
        currentLevelIndex = i;
        currentLevel = levels[currentLevelIndex];
        currentLevel.gameObject.SetActive(true);
        Debug.Log("Level: " + currentLevelIndex);
    }
}
