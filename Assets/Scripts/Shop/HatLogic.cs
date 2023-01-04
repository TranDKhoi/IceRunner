using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatLogic : MonoBehaviour
{
    [SerializeField] private Transform hatContainer;
    private List<GameObject> hatsModels = new List<GameObject>();
    private Hat[] hats;

    private void Awake()
    {
        hats = Resources.LoadAll<Hat>("Hat");
        SpawnHats();

        SelectedHat(SaveManager.ins.save.CurrentHatIndex);
    }

    private void SpawnHats()
    {
        for (int i = 0; i < hats.Length; i++)
            hatsModels.Add(Instantiate(hats[i].Model, hatContainer));
        DisableAllHats();
    }

    public void DisableAllHats()
    {
        for (int i = 0; i < hatsModels.Count; i++)
        {
            hatsModels[i].SetActive(false);
        }
    }

    public void SelectedHat(int index)
    {
        DisableAllHats();
        hatsModels[index].SetActive(true);
    }
}
