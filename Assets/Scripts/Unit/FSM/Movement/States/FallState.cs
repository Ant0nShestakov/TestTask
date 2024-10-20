using System.Collections.Generic;

public sealed class FallState : IUpdatedActionState
{
    public void EnterState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Fall", true);
    }

    public void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Fall", false);
    }

    public void UpdateState(IUpdatedAnimationFSM fsm)
    {
        if (((IMovementFSM)fsm).IsGrounded)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(WalkState)));
    }
}