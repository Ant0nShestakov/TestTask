using System.Collections.Generic;

public sealed class RunState : IUpdatedActionState
{
    private readonly InputManager _inputManager;

    public RunState(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    public void EnterState(IAnimationFSM fsm)
    {
        ((IMovementFSM)fsm).Visitor.Visit(this);
        fsm.Animator.SetBool("Run", true);
    }

    public void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Run", false);
    }

    public void UpdateState(IUpdatedAnimationFSM fsm)
    {
        if (_inputManager.RunValue == 0)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(WalkState)));
        else if (!((IMovementFSM)fsm).IsGrounded)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(FallState)));
    }
}