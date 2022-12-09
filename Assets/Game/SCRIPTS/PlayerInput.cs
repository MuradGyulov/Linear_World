using UnityEngine;
using YG;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    protected static float move { get; private set; }
    protected static bool fire { get; private set; }
    public static UnityEvent JumpEvent = new UnityEvent();

    private bool isDesctop = false;

    private void Start()
    {
        isDesctop = YandexGame.EnvironmentData.isDesktop;
    }

    private void Update()
    {
        if (isDesctop)
        {
            move = Input.GetAxisRaw("Horizontal");
            if (Input.GetMouseButton(0)) { fire = true; } else { fire = false; }
            if (Input.GetKeyDown(KeyCode.W)) { JumpEvent.Invoke(); }
        }
    }

    public void ButtonMoveLeft() { move = -1; }
    public void ButtonMoveRight() { move = 1; }
    public void StopMovement() { move = 0; }
    public void ButtonOpenFire() { fire = true; }
    public void ButtonCeaseFire() { fire = false; }
    public void ButtonJump() { JumpEvent.Invoke(); }
}