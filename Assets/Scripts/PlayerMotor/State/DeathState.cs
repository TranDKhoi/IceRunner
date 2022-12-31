using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 knockBackForce = new Vector3(0, 4, -3);
    private Vector3 currentKnockback;
    public override void Construct()
    {
        currentKnockback = knockBackForce;
        motor.anim?.SetTrigger("Death");
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 m = currentKnockback;

        currentKnockback = new Vector3(0,
            currentKnockback.y -= motor.gravity * Time.deltaTime,
            currentKnockback.z += 2.0f * Time.deltaTime);

        if (currentKnockback.z > 0)
        {
            currentKnockback.z = 0;
            GameManager.ins.ChangeState(GameManager.ins.GetComponent<GameStateDeath>());
        }

        return m;
    }
}
