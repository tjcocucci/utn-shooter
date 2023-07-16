using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public int currentLevelIndex;
    public Level currentLevel;

    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
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
        currentLevelIndex = 0;
        currentLevel = levels[currentLevelIndex];
        currentLevel.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<EnemyManager>().OnLevelCleared += NextLevel;
    }

    void NextLevel(int levelIndex)
    {
        if (levelIndex == levels.Length - 1)
        {
            Debug.Log("You win!");
        }
        else
        {
            currentLevel.gameObject.SetActive(false);
            currentLevelIndex++;
            currentLevel = levels[currentLevelIndex];
            currentLevel.gameObject.SetActive(true);
            Debug.Log(
                "Cleared Level: "
                    + (currentLevelIndex)
                    + " - current Level: "
                    + (currentLevelIndex + 1)
            );
        }
    }
}
