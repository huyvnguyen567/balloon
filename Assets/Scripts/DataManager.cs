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

    public int diamond = 1700;
    public float highScore;
    public float score;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerPrefs.SetInt(TaskType.PointsInOneGame.ToString(), 0);
            PlayerPrefs.SetInt(TaskType.DaysConsecutive.ToString(), 3);
        }
    }
    public void SaveLevel()
    {
        // Lưu thông tin cấp độ hiện tại vào PlayerPrefs
        PlayerPrefs.SetInt("CurrentLevel", GameController.Instance.CurrentLevel);
        // Lưu PlayerPrefs
        PlayerPrefs.Save();
    }

    public void LoadLevel()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            // Lấy thông tin cấp độ từ PlayerPrefs
            GameController.Instance.CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");

        }
        else
        {
            GameController.Instance.CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        }
    }
    public void SaveScore()
    {
        // Lưu thông tin cấp độ hiện tại vào PlayerPrefs
        PlayerPrefs.SetFloat("Score", score);
        // Lưu PlayerPrefs
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            // Lấy thông tin cấp độ từ PlayerPrefs
            score = PlayerPrefs.GetFloat("Score");

        }
        else
        {
            score = PlayerPrefs.GetFloat("Score", 0);
        }
    }

    // Hàm để so sánh điểm với high score và cập nhật nếu cần
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

  
}
