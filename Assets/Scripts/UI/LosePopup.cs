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
    public TMP_Text levelText;
    public TMP_Text scoreText;

    private void OnEnable()
    {
        menuButton.onClick.AddListener(LoadScene);

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
        DataManager.Instance.SaveLevel();
        PlayerPrefs.DeleteKey("Score");
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
