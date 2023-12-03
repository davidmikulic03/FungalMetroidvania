using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float HorizontalMoveInput();
    public abstract bool JumpInput();
    public abstract void Enable();
}
