using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string name;
    public Sprite sprite;
    public float chance;
    public int cost;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<ItemData> items = new List<ItemData>();
    public LevelSO levelData;
    public List<ThemeButtonSO> themesData;
    public List<CanonButtonSO> cannonsData;

    public float highScore;
    public float score;
    public float previousScore;

    public int diamond = 1700;
    public int coin = 1000;

    [Header("Upgrade")]
    public float fireRateTime = 0.2f;
    public float fireBulletSpeed = 10f;
    public float fireDamage = 1f;
    public int upgradeFireSpeedCost = 1;
    public int upgradeFirePowerCost = 1;

    //[Header("Task")]
    public int numberOfGamesPlayed;
    public int ballDestroyedCount;
    //public float dps;
    //public int pointsInOneGame;
    //public int firePower;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerPrefs.SetInt(TaskType.DaysConsecutive.ToString(), 3);
        }
    }
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", GameController.Instance.CurrentLevel);
    }

    public void LoadLevel()
    {
        GameController.Instance.CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    public void SaveScore()
    {
        PlayerPrefs.SetFloat("Score", score);
    }

    public void LoadScore()
    {
        score = PlayerPrefs.GetFloat("Score", 0);
    }

    public void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("Coin", coin);
    }

    public void LoadCoin()
    {
        coin = PlayerPrefs.GetInt("Coin", 1000);
    }
    public void LoadNumberOfGamesPlayed()
    {
        numberOfGamesPlayed = (int)PlayerPrefs.GetFloat(TaskType.GamesPlayed.ToString(), 0);
    }

    public void LoadBallDestroyedCount()
    {
        ballDestroyedCount = (int)PlayerPrefs.GetFloat(TaskType.BallBlasted.ToString(), 0);
    }

    public void SaveUpgradeData()
    {
        PlayerPrefs.SetFloat("FireRateTime", fireRateTime);
        PlayerPrefs.SetFloat("FireBulletSpeed", fireBulletSpeed);
        PlayerPrefs.SetFloat("FireDamage", fireDamage);
        PlayerPrefs.SetInt("UpgradeFireSpeedCost", upgradeFireSpeedCost);
        PlayerPrefs.SetInt("UpgradeFirePowerCost", upgradeFirePowerCost);
        PlayerPrefs.Save();
    }

    public void LoadUpgradeData()
    {
        if (PlayerPrefs.HasKey("FireRateTime"))
        {
            fireRateTime = PlayerPrefs.GetFloat("FireRateTime");
        }
        if (PlayerPrefs.HasKey("FireBulletSpeed"))
        {
            fireBulletSpeed = PlayerPrefs.GetFloat("FireBulletSpeed");
        }
        if (PlayerPrefs.HasKey("FireDamage"))
        {
            fireDamage = PlayerPrefs.GetFloat("FireDamage");
        }
        if (PlayerPrefs.HasKey("UpgradeFireSpeedCost"))
        {
            upgradeFireSpeedCost = PlayerPrefs.GetInt("UpgradeFireSpeedCost");
        }
        if (PlayerPrefs.HasKey("UpgradeFirePowerCost"))
        {
            upgradeFirePowerCost = PlayerPrefs.GetInt("UpgradeFirePowerCost");
        }
    }

    public void SaveTaskTypeData(TaskType type, float value)
    {
        PlayerPrefs.SetFloat(type.ToString(), value);
    }

    public void LoadTaskTypeData()
    {
        foreach (var item in themesData)
        {
            switch (item.requireTasks.taskType)
            {
                case TaskType.GamesPlayed:
                    item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.GamesPlayed.ToString());
                    break;
                case TaskType.PointsInOneGame:
                    item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.PointsInOneGame.ToString());
                    break;
                case TaskType.FireUpgradeSpeed:
                    item.requireTasks.currentValue = PlayerPrefs.GetFloat(TaskType.FireUpgradeSpeed.ToString());
                    break;
                case TaskType.FireUpgradePower:
                    item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.FireUpgradePower.ToString());
                    break;
                case TaskType.BallBlasted:
                    item.requireTasks.currentValue = PlayerPrefs.GetFloat(TaskType.BallBlasted.ToString());
                    break;
                default:
                    break;
            }
        }
    }
}
