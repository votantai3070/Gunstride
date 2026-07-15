using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerInputMobile : MonoBehaviour
{
    private PlayerLaneMovement movement;

    private Vector2 startPos;
    private bool tracking;

    private void Awake()
    {
        movement = GetComponent<PlayerLaneMovement>();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted()) return;

        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Touch.activeTouches.Count == 0) return;

        var touch = Touch.activeTouches[0];

        if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            startPos = touch.screenPosition;
            tracking = true;
        }
        else if (tracking && touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            Vector2 delta = touch.screenPosition - startPos;

            if (delta.magnitude < 40f)
            {
                Debug.Log("Tap");
            }
            else if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                if (delta.y > 0f)
                    movement?.ChangeLane(1); // Swipe Up
                else
                    movement?.ChangeLane(-1); // Swipe Down
            }

            tracking = false;
        }
    }

    public bool IsTracking() => tracking;
}