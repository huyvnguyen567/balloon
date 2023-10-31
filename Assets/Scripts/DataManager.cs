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

    public int currentLevel;
    public SpriteRenderer background;
    public GameObject cannonPrefab;

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
    private const string lastLoginDateKey = "LastLoginDate";
    public float loginCount = 0;
    private string lastLoginDate = "";



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Lấy dữ liệu từ PlayerPrefs.
        loginCount = PlayerPrefs.GetFloat(TaskType.DaysConsecutive.ToString(), 0);
        lastLoginDate = PlayerPrefs.GetString(lastLoginDateKey, "");

        // Lấy ngày hôm nay.
        string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd");

        if (lastLoginDate == currentDate)
        {
            // Người chơi đã đăng nhập trong ngày hôm nay, không tăng biến đếm.
        }
        else
        {
            // Người chơi chưa đăng nhập vào ngày hôm nay, tăng biến đếm.
            loginCount++;

            // Cập nhật ngày đăng nhập gần đây.
            lastLoginDate = currentDate;

            // Lưu dữ liệu vào PlayerPrefs.
            SaveTaskTypeData(TaskType.DaysConsecutive, loginCount);
            PlayerPrefs.SetString(lastLoginDateKey, lastLoginDate);
        }

        // In ra số lần đăng nhập.
        //Debug.Log("Số lần đăng nhập: " + loginCount);
    }
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", GameController.Instance.CurrentLevel);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetFloat("Score", score);
    }
    public void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
    }
    public void SaveCoin()
    {
        PlayerPrefs.SetInt("Coin", coin);
    }

    public void SaveDiamond()
    {
        PlayerPrefs.SetInt("Diamond", diamond);
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

    public void SaveTaskTypeData(TaskType type, float value)
    {
        PlayerPrefs.SetFloat(type.ToString(), value);
    }

    public void SaveBackgroundSprite()
    {
        PlayerPrefs.SetString("BackgroundSprite", background.sprite.name);
    }

    public void SaveCannonPrefab()
    {
        PlayerPrefs.SetString("CannonPrefab", cannonPrefab.name);
    }
    public void LoadGameData()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        score = PlayerPrefs.GetFloat("Score", 0);
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        coin = PlayerPrefs.GetInt("Coin", 500);
        diamond = PlayerPrefs.GetInt("Diamond", 999);
        numberOfGamesPlayed = (int)PlayerPrefs.GetFloat(TaskType.GamesPlayed.ToString(), 0);
        ballDestroyedCount = (int)PlayerPrefs.GetFloat(TaskType.BallBlasted.ToString(), 0);
        fireRateTime = PlayerPrefs.GetFloat("FireRateTime", 0.3f);
        fireBulletSpeed = PlayerPrefs.GetFloat("FireBulletSpeed", 22);
        fireDamage = PlayerPrefs.GetFloat("FireDamage", 1);
        upgradeFireSpeedCost = PlayerPrefs.GetInt("UpgradeFireSpeedCost", 1);
        upgradeFirePowerCost = PlayerPrefs.GetInt("UpgradeFirePowerCost", 1);
        string backgroundSpriteName = PlayerPrefs.GetString("BackgroundSprite", "BG1");
        string cannonPrefabName = PlayerPrefs.GetString("CannonPrefab", "Canon 1");
        Sprite loadedBackgroundSprite = Resources.Load<Sprite>(backgroundSpriteName);
        if (loadedBackgroundSprite != null)
        {
            background.sprite = loadedBackgroundSprite;
        }

        GameObject loadedCannonPrefab = Resources.Load<GameObject>(cannonPrefabName);
        if (loadedCannonPrefab != null)
        {
            cannonPrefab = loadedCannonPrefab;
        }
        
    }
}

    //public void LoadTaskTypeData()
    //{
    //    foreach (var item in themesData)
    //    {
    //        switch (item.requireTasks.taskType)
    //        {
    //            case TaskType.GamesPlayed:
    //                item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.GamesPlayed.ToString());
    //                break;
    //            case TaskType.PointsInOneGame:
    //                item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.PointsInOneGame.ToString());
    //                break;
    //            case TaskType.FireUpgradeSpeed:
    //                item.requireTasks.currentValue = PlayerPrefs.GetFloat(TaskType.FireUpgradeSpeed.ToString());
    //                break;
    //            case TaskType.FireUpgradePower:
    //                item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.FireUpgradePower.ToString());
    //                break;
    //            case TaskType.BallBlasted:
    //                item.requireTasks.currentValue = PlayerPrefs.GetFloat(TaskType.BallBlasted.ToString());
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}


