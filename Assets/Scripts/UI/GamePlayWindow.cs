using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
        if(DataManager.Instance.currentLevel % 5 == 0)
        {
            Boss.UpdateProcessEvent.AddListener(() => UpdateSlider());
        }
        else
        {
            BallFissionable.UpdateProcessEvent.AddListener(() => UpdateSlider());
        }
        //BallFissionable.UpdateScoreEvent.AddListener(() => UpdateScoreText());
        BallFissionable.UpdateScoreEvent.AddListener(() => PlayScoreChangeAnimation());
        currentLevelText.text = GameController.Instance.CurrentLevel.ToString();
        nextLevelText.text = (GameController.Instance.CurrentLevel + 1).ToString();

        //ManagerAds.Ins.ShowBanner();
    }

    public void UpdateSlider()
    {
        if (processSlider != null)
        {
            if ((DataManager.Instance.currentLevel % 5 == 0))
            {
                GameObject boss = GameObject.FindGameObjectWithTag("Boss");
                if (boss != null)
                {
                    processSlider.value = (float)boss.GetComponent<Boss>().bossDamageTaken / boss.GetComponent<Boss>().initHealth;

                }
            }
            else
            {
                processSlider.value = (float)GameController.Instance.ballSize1DestroyedCount / GameController.Instance.targetProcess;
            }
            if (processSlider.value == 1)
            {
                GameController.Instance.SwitchGameState(GameController.GameState.Win);
                //BallSpawner.Instance.gameObject.SetActive(false);
            }
        }
    }
    public void UpdateScoreText()
    {
        int currentScore = (int)DataManager.Instance.score;
        scoreText.text = currentScore.ToString();
    }
    public void PlayScoreChangeAnimation()
    {
        int currentScore = (int)DataManager.Instance.score;
        int previousScore = (int)DataManager.Instance.previousScore;

        // Kiểm tra xem điểm có thay đổi không trước khi tạo tween.
        if (currentScore != previousScore)
        {
            // Tạo một tween để thay đổi giá trị Text hiển thị điểm một cách mượt mà.
            DOTween.To(() => previousScore, x => previousScore = x, currentScore, 1.0f)
                .OnUpdate(() => {
                    // Cập nhật Text hiển thị điểm trong mỗi frame.
                    scoreText.text = previousScore.ToString();
                })
                .OnComplete(() => {
                    // Xử lý hoàn thành tween (nếu cần).
                });
        }
    }
}


