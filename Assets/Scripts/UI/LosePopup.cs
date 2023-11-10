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
        //BallSpawner.Instance.gameObject.SetActive(false);
        PlayerPrefs.DeleteKey("Score");
    }
    void Start()
    {
        float processPercent = 0;
        if ((DataManager.Instance.currentLevel % 5 == 0))
        {
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            if (boss != null)
            {
                processPercent = boss.GetComponent<Boss>().bossDamageTaken / boss.GetComponent<Boss>().initHealth * 100;
            }
        }
        else
        {
            processPercent = GameController.Instance.ballSize1DestroyedCount / GameController.Instance.targetProcess * 100;
        }
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
