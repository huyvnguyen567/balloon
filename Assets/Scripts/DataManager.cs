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
            //PlayerPrefs.SetInt(TaskType.PointsInOneGame.ToString(), 0);
            PlayerPrefs.SetInt(TaskType.DaysConsecutive.ToString(), 3);
        }
    }
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", GameController.Instance.CurrentLevel);
        PlayerPrefs.Save();
    }

    public void LoadLevel()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            GameController.Instance.CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            GameController.Instance.CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        }
    }
    public void SaveScore()
    {
        PlayerPrefs.SetFloat("Score", score);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetFloat("Score");

        }
        else
        {
            score = PlayerPrefs.GetFloat("Score", 0);
        }
    }

    public void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
        }
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
    }

    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");

        }
        else
        {
            coin = PlayerPrefs.GetInt("Coin", 1000);
        }
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
        //if (PlayerPrefs.HasKey(TaskType.GamesPlayed.ToString()))
        //{
        //    numberOfGamesPlayed = (int)PlayerPrefs.GetFloat(TaskType.GamesPlayed.ToString());
        //}
        //if (PlayerPrefs.HasKey(TaskType.PointsInOneGame.ToString()))
        //{
        //    pointsInOneGame = (int)PlayerPrefs.GetFloat(TaskType.PointsInOneGame.ToString());
        //}
        //if (PlayerPrefs.HasKey(TaskType.FireUpgradeSpeed.ToString()))
        //{
        //    dps = PlayerPrefs.GetFloat(TaskType.FireUpgradeSpeed.ToString());
        //}
        //if (PlayerPrefs.HasKey(TaskType.FireUpgradePower.ToString()))
        //{
        //    firePower = (int)PlayerPrefs.GetFloat(TaskType.FireUpgradePower.ToString());
        //}
    }
}
