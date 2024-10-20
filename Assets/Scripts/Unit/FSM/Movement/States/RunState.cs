using System.Collections.Generic;

public sealed class RunState : ActionState
{
    public RunState(InputManager inputManager) : base(inputManager)
    {
    }

    public override void EnterState(IAnimationFSM fsm)
    {
        fsm.Visitor.Visit(this);
        fsm.Animator.SetBool("Run", true);
    }

    public override void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Run", false);
    }

    public override void UpdateState(IAnimationFSM fsm)
    {
        if (inputManager.RunValue == 0)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(WalkState)));
        else if (!((IMovementFSM)fsm).IsGrounded)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(FallState)));
    }
}