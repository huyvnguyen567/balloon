using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Custom/Task")]
public class TaskSO : ScriptableObject
{
    public string taskName;
    public TaskType taskType;
    public int requiredValue;
    private int currentValue;
    
    public int CurrentValue
    {
        get { return currentValue; }
        set { currentValue = value; }
    }
    public bool IsCompleted()
    {
        currentValue = PlayerPrefs.GetInt(taskType.ToString(), 0);
        return currentValue >= requiredValue;
    }
}
public enum TaskType
{
    GamesPlayed,
    BallBlasted,
    PointsInOneGame,
    FireUpgradeSpeed,
    FireUpgradePower,
    CoinUpgrade,
    DaysConsecutive
}
