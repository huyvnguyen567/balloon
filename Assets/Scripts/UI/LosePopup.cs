using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LosePopup : MonoBehaviour
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
        DataManager.Instance.SaveTaskTypeData(TaskType.GamesPlayed, DataManager.Instance.numberOfGamesPlayed);
        DataManager.Instance.SaveLevel();
        PlayerPrefs.DeleteKey("Score");
    }
    void Start()
    {
        float processPercent = GameController.Instance.ballSize1DestroyedCount / GameController.Instance.targetProcess * 100;
        Debug.Log(processPercent);
        levelText.text = $"Level {GameController.Instance.CurrentLevel}: {(int)processPercent}% Completed";
        scoreText.text = $"Your Score: {(int)DataManager.Instance.score}";
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {

    }
}
