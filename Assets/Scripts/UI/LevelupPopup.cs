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
    public TMP_Text levelText;
    public TMP_Text scoreText;

    private void OnEnable()
    {
        menuButton.onClick.AddListener(LoadScene);
     
    }
    void Start()
    {
        levelText.text = $"Level {GameController.Instance.CurrentLevel} Completed!";
        scoreText.text = $"Your Score: {(int)DataManager.Instance.score}";
    }
    public void LoadScene()
    {
        GameController.Instance.CurrentLevel++;
        DataManager.Instance.SaveLevel();
        DataManager.Instance.SaveScore();
        SceneManager.LoadScene(0);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
