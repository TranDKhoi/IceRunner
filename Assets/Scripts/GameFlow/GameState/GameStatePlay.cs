using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatePlay : GameState
{

    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI fishCount;
    [SerializeField] private TextMeshProUGUI score;


    private static bool isFirstTime;
    public static bool IsFirstTime
    {
        get { return isFirstTime; }
        set { isFirstTime = value; }
    }


    public override void Construct()
    {
        menuUI.SetActive(true);
        GameStats.ins.OnCollectFish += OnCollectFish;
        GameStats.ins.OnScoreChange += OnScoreChange;
        GameManager.ins.playerMotor.ResumePlayer();
        if (!IsFirstTime)
        {
            GameManager.ins.ChangeCamera(GameCamera.Play);

            IsFirstTime = true;

        }
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
        GameStats.ins.OnCollectFish -= OnCollectFish;
        GameStats.ins.OnScoreChange -= OnScoreChange;
    }

    private void OnCollectFish(int amount)
    {
        fishCount.text = GameStats.ins.FishToText();
    }
    private void OnScoreChange(float amount)
    {
        score.text = GameStats.ins.ScoreToText();
    }
}
