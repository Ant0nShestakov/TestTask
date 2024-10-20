using System.Collections.Generic;

public sealed class WalkState : ActionState
{
    public WalkState(InputManager inputManager) : base(inputManager)
    {
    }

    public override void EnterState(IAnimationFSM fsm)
    {
        fsm.Visitor.Visit(this);
        fsm.Animator.SetBool("Walk", true);
    }

    public override void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Walk", false);
    }

    public override void UpdateState(IAnimationFSM fsm)
    {
        if (inputManager.RunValue > 0)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(RunState)));
        else if (!((IMovementFSM)fsm).IsGrounded)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(FallState)));
    }
}