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
    public HatLogic hatLogic;


    //shop item
    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;

    private void Start()
    {
        hats = Resources.LoadAll<Hat>("Hat/");
        PopulateShop();
        currentHatName.text = hats[SaveManager.ins.save.CurrentHatIndex].ItemName;
    }

    public override void Construct()
    {
        GameManager.ins.ChangeCamera(GameCamera.Shop);
        fishCountText.text = SaveManager.ins.save.Fish.ToString("000");
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
            if (SaveManager.ins.save.UnlockedHatFlag[i] == 0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[i].ItemPrice.ToString();
            else
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

        }
    }

    private void OnHatClick(int i)
    {
        if (SaveManager.ins.save.UnlockedHatFlag[i] == 1)
        {
            SaveManager.ins.save.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectedHat(i);
            SaveManager.ins.Save();
        }
        else if (hats[i].ItemPrice <= SaveManager.ins.save.Fish)
        {
            SaveManager.ins.save.Fish -= hats[i].ItemPrice;
            SaveManager.ins.save.UnlockedHatFlag[i] = 1;
            SaveManager.ins.save.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectedHat(i);
            fishCountText.text = SaveManager.ins.save.Fish.ToString("000");
            SaveManager.ins.Save();
            hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            //not enough fish
        }
    }

    public void OnHomeClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
