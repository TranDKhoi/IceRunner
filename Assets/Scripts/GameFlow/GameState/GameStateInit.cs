using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{

    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI fishCountText;

    public override void Construct()
    {
        GameManager.ins.ChangeCamera(GameCamera.Init);
        hiscoreText.text = "Highscore: " + SaveManager.ins.save.HighScore.ToString();
        fishCountText.text = "Fish: " + SaveManager.ins.save.Fish.ToString();
        menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }
    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStatePlay>());
        GameStats.ins.ResetSession();
    }
    public void OnShopClick()
    {
        brain.ChangeState(GetComponent<GameStateShop>());
    }
}
