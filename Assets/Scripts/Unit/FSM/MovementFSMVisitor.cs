public sealed class MovementFSMVisitor : IActionStateVisitor
{
    private readonly AbstractUnitModel _unitModel;

    public MovementFSMVisitor(AbstractUnitModel unitModel)
    {
        _unitModel = unitModel; 
    }

    public void Visit(WalkState state)
    {
        ((UnitModel)_unitModel).SetWalkSpeed();
    }

    public void Visit(RunState state)
    {
        ((UnitModel)_unitModel).SetRunSpeed();
    }
}