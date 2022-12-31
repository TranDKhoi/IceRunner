using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float verticalDistance = 25.0f;

    //this field is to fixed bug that i dont even know why this bug appear
    private int runningStateRunTime;

    public override void Construct()
    {
        motor.controller.enabled = false;
        motor.transform.position = new Vector3(0, verticalDistance, motor.transform.position.z);
        motor.controller.enabled = true;

        runningStateRunTime = 1;
        motor.verticalVelocity = 0.0f;
        motor.currentLane = 0;
        motor.anim?.SetTrigger("Respawn");
        GameManager.ins.ChangeCamera(GameCamera.Respawn);

    }

    public override void Destruct()
    {
        GameManager.ins.ChangeCamera(GameCamera.Play);
    }

    public override Vector3 ProcessMotion()
    {
        // Apply gravity
        motor.ApplyGravity();

        // Create our return vector
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (runningStateRunTime == 1)
        {
            if (motor.isGrounded)
                runningStateRunTime = 2;
        }
        else
        {
            if (motor.isGrounded)
                motor.ChangeState(GetComponent<RunningState>());
        }


        if (InputManager.ins.SwipeLeft)
            motor.ChangeLane(-1);

        if (InputManager.ins.SwipeRight)
            motor.ChangeLane(1);
    }
}
