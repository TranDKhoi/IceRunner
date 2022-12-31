using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _ins;
    public static InputManager ins { get { return _ins; } }


    private RunnerInputAction actionScheme;


    //configuration
    [SerializeField] private float sqrSwipeDeadzone = 50.0f;


    #region properties
    private bool tap;
    public bool Tap
    {
        get { return tap; }
    }

    private Vector2 touchPosition;
    public Vector2 TouchPosition
    {
        get { return touchPosition; }
    }

    private bool swipeLeft;
    public bool SwipeLeft
    {
        get { return swipeLeft; }
    }

    private bool swipeRight;
    public bool SwipeRight
    {
        get { return swipeRight; }
    }

    private bool swipeUp;
    public bool SwipeUp
    {
        get { return swipeUp; }
    }

    private bool swipeDown;
    public bool SwipeDown
    {
        get { return swipeDown; }
    }

    private Vector2 startDrag;
    #endregion


    private void Awake()
    {
        _ins = this;
        DontDestroyOnLoad(gameObject);
        SetupControl();
    }
    private void LateUpdate()
    {
        ResetInput();
    }
    void ResetInput()
    {
        tap = swipeDown = swipeLeft = swipeRight = swipeUp = false;
    }

    private void SetupControl()
    {
        actionScheme = new RunnerInputAction();

        //register actions
        actionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
        actionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
        actionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
        actionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        Vector2 delta = touchPosition - startDrag;
        float sqrDistance = delta.sqrMagnitude;

        //begin swipe
        if (sqrDistance > sqrSwipeDeadzone)
        {
            float x = MathF.Abs(delta.x);
            float y = MathF.Abs(delta.y);

            if (x > y) // left or right
            {
                if (delta.x > 0)
                    swipeRight = true;
                else
                    swipeLeft = true;
            }
            else // up or down
            {
                if (delta.y > 0)
                    swipeUp = true;
                else
                    swipeDown = true;
            }
        }

        startDrag = Vector2.zero;
    }
    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        startDrag = touchPosition;
    }
    private void OnPosition(InputAction.CallbackContext ctx)
    {
        touchPosition = ctx.ReadValue<Vector2>();
    }
    private void OnTap(InputAction.CallbackContext ctx)
    {
        tap = true;
    }

    public void OnEnable()
    {
        actionScheme.Enable();
    }

    public void OnDisable()
    {
        actionScheme.Disable();
    }
}
