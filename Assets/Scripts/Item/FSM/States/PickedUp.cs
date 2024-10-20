public class PickedUp : IActionState
{
    public void EnterState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool(nameof(PickedUp), true);
    }

    public void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool(nameof(PickedUp), false);
    }
}