using UnityEngine;

public sealed class MovementHandler : Handler
{
    private readonly AbstractUnitModel _unit;
    private readonly UnitView _view;

    private readonly PhysicsController _characterController;
    private readonly Transform _transform;

    private Vector3 _direction;

    public MovementHandler(AbstractUnitModel unit, AbstractUnitController controller, InputManager inputManager) : base(inputManager)
    {
        _unit = unit;
        _characterController = controller.GetComponent<PhysicsController>();
        _view = controller.GetComponent<UnitView>();

        _transform = controller.transform;
    }

    public override void Update()
    {
        if (_characterController.IsGrounded)
        {
            SetMoveDirection();

            if (inputManager.JumpValue > 0)
                Jump();
        }

        _characterController.Move(_direction, _unit.CurrentMass);

        _view.AnimateMovement(inputManager.MoveVector);
    }

    private void SetMoveDirection()
    {
        Vector3 inputDirection = new(inputManager.MoveVector.x, 0, inputManager.MoveVector.y);
        inputDirection = _transform.TransformDirection(inputDirection);

        _direction = _unit.CurrentSpeed * inputDirection.normalized;
    }

    private void Jump() =>
        _characterController.Jump(_unit.CurrentJumpForce);
}
