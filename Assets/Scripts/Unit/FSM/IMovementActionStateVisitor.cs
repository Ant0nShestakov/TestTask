public interface IMovementActionStateVisitor
{
    public void Visit(WalkState state);
    public void Visit(RunState state);
} 