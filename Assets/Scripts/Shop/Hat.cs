using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hat")]
public class Hat : ScriptableObject
{
    public string ItemName;
    public string ItemPrice;
    public Sprite Icon;
    public GameObject Model;
}
