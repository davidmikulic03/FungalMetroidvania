using UnityEngine;

public abstract class Controller : ScriptableObject
{
    public abstract float HorizontalMoveInput();
    public abstract bool JumpInput();
    public abstract void Enable();
}
