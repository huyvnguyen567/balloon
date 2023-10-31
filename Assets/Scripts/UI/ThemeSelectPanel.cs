using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelectPanel : MonoBehaviour
{
    //[SerializeField] private int themeCount = 20;
    //[SerializeField] private List<ThemeButtonSO> themesData;
    [SerializeField] private GameObject themePrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Sprite inActive;
    private GameObject theme;

    private bool hasInstantiatedThemes = false;
    private void OnEnable()
    {

        if (!hasInstantiatedThemes)
        {
            InstantiateThemes();
            hasInstantiatedThemes = true; // Đánh dấu rằng đã tạo lần đầu
        }
        else
        {
            UpdateThemes();
        }
    }

    private void InstantiateThemes()
    {
        for (int i = 0; i < DataManager.Instance.themesData.Count; i++)
        {
            theme = Instantiate(themePrefab, content, false);
            int index = i;
            if (DataManager.Instance.themesData[i].CanActiveTheme())
            {
                theme.GetComponent<Image>().sprite = DataManager.Instance.themesData[i].active;
                theme.GetComponent<Button>().onClick.AddListener(delegate { DataManager.Instance.background.sprite = DataManager.Instance.themesData[index].background;
                    DataManager.Instance.SaveBackgroundSprite();
                });
            }
            else
            {
                theme.GetComponent<Image>().sprite = inActive;
                theme.GetComponent<Button>().onClick.AddListener(() => OnClickThemeLockButton(index));
            }
        }
    }

    private void UpdateThemes()
    {
        for (int i = 0; i < DataManager.Instance.themesData.Count; i++)
        {
            int index = i;
            if (DataManager.Instance.themesData[i].CanActiveTheme())
            {
                // Cập nhật các themes đã tồn tại
                theme = content.GetChild(i).gameObject;
                theme.GetComponent<Image>().sprite = DataManager.Instance.themesData[i].active;
                theme.GetComponent<Button>().onClick.RemoveAllListeners();
                theme.GetComponent<Button>().onClick.AddListener(delegate { DataManager.Instance.background.sprite = DataManager.Instance.themesData[index].background;
                    DataManager.Instance.SaveBackgroundSprite();
                });
            }
        }
    }
    void Start()
    {
        
    }

    public void OnClickThemeLockButton(int index)
    {
        ProcessTaskPopup processTaskPopup = UIManager.Instance.processTaskPopup.GetComponent<ProcessTaskPopup>();
        processTaskPopup.taskNameText.text = DataManager.Instance.themesData[index].requireTasks.taskName;
        processTaskPopup.processText.text = $"{DataManager.Instance.themesData[index].requireTasks.currentValue}/{DataManager.Instance.themesData[index].requireTasks.requiredValue}";
        processTaskPopup.processSlider.value = (float)DataManager.Instance.themesData[index].requireTasks.currentValue / DataManager.Instance.themesData[index].requireTasks.requiredValue;
        processTaskPopup.Show();
    }

    private void OnDisable()
    {
        //theme.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
