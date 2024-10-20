using System.Collections.Generic;

public sealed class FallState : ActionState
{
    public FallState(InputManager inputManager) : base(inputManager)
    {
    }

    public override void EnterState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Fall", true);
    }

    public override void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Fall", false);
    }

    public override void UpdateState(IAnimationFSM fsm)
    {
        if (((IMovementFSM)fsm).IsGrounded)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(WalkState)));
    }

}