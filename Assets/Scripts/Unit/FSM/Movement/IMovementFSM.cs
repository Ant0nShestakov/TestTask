public interface IMovementFSM : IUpdatedAnimationFSM
{
    public bool IsGrounded { get; }

    public IMovementActionStateVisitor Visitor { get; }
}