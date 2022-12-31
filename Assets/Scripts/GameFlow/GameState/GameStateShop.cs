using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
{

    public GameObject menuUI;
    public TextMeshProUGUI fishCountText;
    public TextMeshProUGUI currentHatName;


    //shop item
    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;

    private void Start()
    {
        hats = Resources.LoadAll<Hat>("Hat/");
        PopulateShop();
    }

    public override void Construct()
    {
        GameManager.ins.ChangeCamera(GameCamera.Shop);
        fishCountText.text = SaveManager.ins.save.Fish.ToString();
        menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    private void PopulateShop()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatPrefab, hatContainer) as GameObject;

            //button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));
            //icon
            go.transform.GetChild(1).GetComponent<Image>().sprite = hats[i].Icon;
            //name
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hats[i].ItemName;
            //price
            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[i].ItemPrice;
        }
    }

    private void OnHatClick(int i)
    {
        Debug.Log(i);
        currentHatName.text = hats[i].ItemName;
    }

    public void OnHomeClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
