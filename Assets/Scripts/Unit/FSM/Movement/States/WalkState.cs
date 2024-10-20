using System.Collections.Generic;

public sealed class WalkState : IUpdatedActionState
{
    private readonly InputManager _inputManager;

    public WalkState(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    public void EnterState(IAnimationFSM fsm)
    {
        ((IMovementFSM)fsm).Visitor.Visit(this);
        fsm.Animator.SetBool("Walk", true);
    }

    public void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool("Walk", false);
    }

    public void UpdateState(IUpdatedAnimationFSM fsm)
    {
        if (_inputManager.RunValue > 0)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(RunState)));
        else if (!((IMovementFSM)fsm).IsGrounded)
            fsm.SwitchState(fsm.States.GetValueOrDefault(typeof(FallState)));
    }
}