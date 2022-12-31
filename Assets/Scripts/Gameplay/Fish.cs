using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    private Animator anim;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickupFish();
        }
    }

    private void PickupFish()
    {
        anim?.SetTrigger("Pickup");
        GameStats.ins.CollectFish();
        //increment the fish count

        //increment score

        //play sound

        //trigger anim
    }

    public void OnShowChunk()
    {
        anim?.SetTrigger("Idle");
    }
}
