using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;

    private void Update()
    {
        transform.position = Vector3.forward * player.transform.position.z;
        material.SetVector("_offset", new Vector2(0, -transform.position.z * 0.15f));
    }
}
