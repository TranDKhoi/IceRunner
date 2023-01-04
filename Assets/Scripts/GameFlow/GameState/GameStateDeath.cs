using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameStateDeath : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI totalFish;
    [SerializeField] private TextMeshProUGUI currentFish;

    //Completion circle fields
    [SerializeField] private Image completionCircle;
    public float timeToDecision = 2.5f;
    private float deathTime;

    public override void Construct()
    {
        GameManager.ins.playerMotor.PausePlayer();

        deathTime = Time.time;
        menuUI.SetActive(true);
        completionCircle.gameObject.SetActive(true);

        if (SaveManager.ins.save.HighScore < (int)GameStats.ins.score)
        {
            SaveManager.ins.save.HighScore = (int)GameStats.ins.score;
            currentScore.color = Color.green;
        }
        else
            currentScore.color = Color.white;

        SaveManager.ins.save.Fish += GameStats.ins.fishThisSession;
        SaveManager.ins.Save();


        highScore.text = "Highscore: " + SaveManager.ins.save.HighScore;
        currentScore.text = GameStats.ins.ScoreToText();
        totalFish.text = "Total fish: " + SaveManager.ins.save.Fish;
        currentFish.text = GameStats.ins.FishToText();

    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecision;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            completionCircle.gameObject.SetActive(false);
        }
    }

    public void ToMenu()
    {
        GameStatePlay.IsFirstTime = false;

        brain.ChangeState(GetComponent<GameStateInit>());
        GameManager.ins.playerMotor.ResetPlayer();
        Camera.main.transform.position = Vector3.zero;
        GameManager.ins.worldGeneration.ResetWorld();
        GameManager.ins.sceneGeneration.ResetWorld();

    }

    public void ResumeGame()
    {
        GameManager.ins.playerMotor.RespawnPlayer();
        brain.ChangeState(GetComponent<GameStatePlay>());
    }

    public void TryResumeGame()
    {
        AdManager.ins.ShowAd();
        AdManager.ins.OnAdDone += () =>
        {
            completionCircle.gameObject.SetActive(false);
            ResumeGame();
        };

    }

}
