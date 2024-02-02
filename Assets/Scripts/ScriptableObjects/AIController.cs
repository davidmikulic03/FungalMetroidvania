using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : Controller
{
    public override float HorizontalMoveInput()
    {
        return -1f;
    }
    public override bool JumpInput()
    {
        return true;
    }
    public override void Enable()
    {
        
    }
}
