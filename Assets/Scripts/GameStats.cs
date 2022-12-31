using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    private static GameStats _ins;
    public static GameStats ins
    {
        get { return _ins; }
    }

    //score
    public float score;
    public float highScore;
    public float distanceModifier = 1.5f;

    //fish
    public int totalFish;
    public int fishThisSession;
    public float pointsPerFish = 10.0f;

    //internal cooldown
    private float lastScoreUpdate;
    private float scoreUpdateDelta = 2.0f;

    //action
    public Action<int> OnCollectFish;
    public Action<float> OnScoreChange;


    private void Awake()
    {
        _ins = this;
    }

    public void Update()
    {
        float s = GameManager.ins.playerMotor.transform.position.z * distanceModifier;
        s += fishThisSession * pointsPerFish;

        if (s > score)
        {
            score = s;
            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                OnScoreChange?.Invoke(score);
            }
        }


    }

    public void CollectFish()
    {
        fishThisSession++;
        OnCollectFish?.Invoke(fishThisSession);
    }

    public void ResetSession()
    {
        score = 0;
        fishThisSession = 0;
        OnScoreChange?.Invoke(score);
        OnCollectFish?.Invoke(fishThisSession);
    }

    public string ScoreToText()
    {
        return score.ToString("000000");
    }

    public string FishToText()
    {
        return fishThisSession.ToString("000");
    }
}
