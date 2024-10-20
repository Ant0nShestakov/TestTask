public abstract class ActionState
{
    protected readonly InputManager inputManager;

    public ActionState(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    public abstract void EnterState(IAnimationFSM fsm);
    public abstract void ExitState(IAnimationFSM fsm);
    public abstract void UpdateState(IAnimationFSM fsm);
}
