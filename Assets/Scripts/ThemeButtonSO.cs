using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThemeButton", menuName = "Custom/ThemeButton")]
public class ThemeButtonSO : ScriptableObject
{
    public string themeName;
    public Sprite active;
    public Sprite background;
    public TaskSO requireTasks;

    public bool CanActiveTheme()
    {      
        if (!requireTasks.IsCompleted())
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
