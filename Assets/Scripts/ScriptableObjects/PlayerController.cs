using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public InputAction horizontalMovement;
    public InputAction jump;

    public override float HorizontalMoveInput()
    {
        return horizontalMovement.ReadValue<float>();
    }
    public override bool JumpInput()
    {
        return jump.ReadValue<float>() == 1f;
    }
    public override void Enable()
    {
        horizontalMovement.Enable();
        jump.Enable();
    }
}
