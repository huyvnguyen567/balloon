using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelupPopup : MonoBehaviour
{
    public Button menuButton;
    public Button replayButton;
    public Button okButton;
    public TMP_Text levelText;
    public TMP_Text scoreText;

    private void OnEnable()
    {
        okButton.onClick.AddListener(LoadScene);
        DataManager.Instance.numberOfGamesPlayed++;
        levelText.text = $"Level {GameController.Instance.CurrentLevel} Completed!";
        scoreText.text = $"Your Score: {(int)DataManager.Instance.score}";
        GameController.Instance.CurrentLevel++;
        DataManager.Instance.SaveLevel();
        DataManager.Instance.SaveScore();
        DataManager.Instance.SaveTaskTypeData(TaskType.GamesPlayed, DataManager.Instance.numberOfGamesPlayed);
    }
    void Start()
    {
      
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
