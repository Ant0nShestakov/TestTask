public interface IActionStateVisitor
{
    public void Visit(WalkState state);
    public void Visit(RunState state);
} 