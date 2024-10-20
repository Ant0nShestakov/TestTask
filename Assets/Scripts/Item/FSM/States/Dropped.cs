public class Dropped : IActionState
{
    public void EnterState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool(nameof(Dropped), true);
    }

    public void ExitState(IAnimationFSM fsm)
    {
        fsm.Animator.SetBool(nameof(Dropped), false);
    }
}