using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelectPanel : MonoBehaviour
{
    //[SerializeField] private int themeCount = 20;
    [SerializeField] private List<ThemeButtonSO> themes;
    [SerializeField] private GameObject themePrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Sprite inActive;
    private GameObject theme;

    private void OnEnable()
    {
        foreach(var item in themes)
        {
            if(item.requireTasks.taskType == TaskType.GamesPlayed)
            {
                item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.GamesPlayed.ToString());
            }
            if(item.requireTasks.taskType == TaskType.PointsInOneGame)
            {
                item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.PointsInOneGame.ToString());
            }
            if (item.requireTasks.taskType == TaskType.FireUpgradeSpeed)
            {
                item.requireTasks.currentValue = PlayerPrefs.GetFloat(TaskType.FireUpgradeSpeed.ToString());
            }
            if (item.requireTasks.taskType == TaskType.FireUpgradePower)
            {
                item.requireTasks.currentValue = (int)PlayerPrefs.GetFloat(TaskType.FireUpgradePower.ToString());
            }
            if (item.requireTasks.taskType == TaskType.BallBlasted)
            {
                item.requireTasks.currentValue = PlayerPrefs.GetFloat(TaskType.BallBlasted.ToString());
            }
        }
    }
    void Start()
    {
        for (int i = 0; i < themes.Count; i++)
        {
            theme = Instantiate(themePrefab, content, false);
            int index = i;
            if (themes[i].CanActiveTheme())
            {
                theme.GetComponent<Image>().sprite = themes[i].active;
                theme.GetComponent<Button>().onClick.AddListener(delegate { GameController.Instance.background.sprite = themes[index].background; });
            }
            else
            {
                theme.GetComponent<Image>().sprite = inActive;
                theme.GetComponent<Button>().onClick.AddListener(() => OnClickThemeLockButton(index));
            }
        }      
    }

    public void OnClickThemeLockButton(int index)
    {
        ProcessTaskPopup processTaskPopup = UIManager.Instance.processTaskPopup.GetComponent<ProcessTaskPopup>();
        processTaskPopup.taskNameText.text = themes[index].requireTasks.taskName;
        processTaskPopup.processText.text = $"{themes[index].requireTasks.currentValue}/{themes[index].requireTasks.requiredValue}";
        processTaskPopup.processSlider.value = (float)themes[index].requireTasks.currentValue / themes[index].requireTasks.requiredValue;
        processTaskPopup.Show();
    }
}
