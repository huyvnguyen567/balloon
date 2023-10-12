using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelectPanel : MonoBehaviour
{
    //[SerializeField] private int themeCount = 20;
    [SerializeField] private List<ThemeButtonSO> themes;
    [SerializeField] private GameObject themePrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private Sprite inActive;
    private GameObject theme;
    void Start()
    {
        for (int i = 0; i < themes.Count; i++)
        {
            theme = Instantiate(themePrefab, content.transform, false);
            if (themes[i].CanActiveTheme())
            {
                theme.GetComponent<Image>().sprite = themes[i].active;
                int index = i;
                theme.GetComponent<Button>().onClick.AddListener(delegate { GameController.Instance.background.sprite = themes[index].background; });
            }
            else
            {
                theme.GetComponent<Image>().sprite = inActive;
                int index = i;
                theme.GetComponent<Button>().onClick.AddListener(() => OnClickThemeLockButton(index));
            }
        }      
    }

    public void OnClickThemeLockButton(int index)
    {
        ProcessTaskPopup processTaskPopup = UIManager.Instance.processTaskPopup.GetComponent<ProcessTaskPopup>();
        processTaskPopup.taskNameText.text = themes[index].requireTasks.taskName;
        processTaskPopup.processText.text = $"{themes[index].requireTasks.CurrentValue}/{themes[index].requireTasks.requiredValue}";
        processTaskPopup.processSlider.value = (float)themes[index].requireTasks.CurrentValue / themes[index].requireTasks.requiredValue;
        processTaskPopup.Show();
    }
}
