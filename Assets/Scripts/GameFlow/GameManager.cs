using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameCamera
{
    Init = 0,
    Play = 1,
    Shop = 2,
    Respawn = 3,
}

public class GameManager : MonoBehaviour
{
    private static GameManager _ins;
    public static GameManager ins
    {
        get { return _ins; }
    }


    private GameState state;
    public PlayerMotor playerMotor;
    public WorldGeneration worldGeneration;
    public SceneChunkGeneration sceneGeneration;
    public GameObject[] cameras;

    private void Awake()
    {
        _ins = this;
        state = GetComponent<GameStateInit>();
        state.Construct();
    }

    private void Update()
    {
        state.UpdateState();
    }


    public void ChangeState(GameState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }

    public void ChangeCamera(GameCamera c)
    {
        foreach (var go in cameras) go.SetActive(false);
        cameras[(int)c].SetActive(true);
    }
}
