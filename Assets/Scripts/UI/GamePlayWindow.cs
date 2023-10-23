using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayWindow : MonoBehaviour
{
    public Slider processSlider;
    public TMP_Text scoreText;
    public TMP_Text currentLevelText;
    public TMP_Text nextLevelText;

    private void Start()
    {
        //UpdateSlider();
        UpdateScoreText();
        BallFissionable.UpdateProcessEvent.AddListener(() => UpdateSlider());
        BallFissionable.UpdateScoreEvent.AddListener(() => UpdateScoreText());
        currentLevelText.text = GameController.Instance.CurrentLevel.ToString();
        nextLevelText.text = (GameController.Instance.CurrentLevel + 1).ToString();

    }
    public void UpdateSlider()
    {
        if (processSlider != null)
        {
            processSlider.value = (float)GameController.Instance.ballSize1DestroyedCount / GameController.Instance.targetProcess;
            if(processSlider.value == 1)
            {
                GameController.Instance.SwitchGameState(GameController.GameState.Win);
            }
        }
    }
    public void UpdateScoreText()
    {
        int currentScore = (int)DataManager.Instance.score;
        scoreText.text = currentScore.ToString();
    }
}
