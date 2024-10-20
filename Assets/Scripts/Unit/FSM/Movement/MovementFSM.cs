using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class MovementFSM : IMovementFSM
{
    private readonly Animator _animator;
    private readonly InputManager _inputManager;
    private readonly PhysicsController _physicsController;

    private readonly IMovementActionStateVisitor _fSMVisitor;

    private IUpdatedActionState _currentState;

    private Dictionary<Type, IActionState> _states;

    public bool IsGrounded => _physicsController.IsGrounded;

    public IReadOnlyDictionary<Type, IActionState> States => _states;

    public Animator Animator => _animator;

    public IMovementActionStateVisitor Visitor => _fSMVisitor;

    public MovementFSM(UnitView unitView, IMovementActionStateVisitor fSMVisitor)
    {
        _animator = unitView.GetComponent<Animator>();
        _inputManager = unitView.GetComponent<InputManager>();
        _physicsController = unitView.GetComponent<PhysicsController>();
        _fSMVisitor = fSMVisitor;

        Initialize();

        _currentState = (IUpdatedActionState)_states.GetValueOrDefault(typeof(WalkState));
    }

    private void Initialize()
    {
        _states = new Dictionary<Type, IActionState>
        {
            { typeof(WalkState), new WalkState(_inputManager) },
            { typeof(RunState), new RunState(_inputManager) },
            { typeof(FallState), new FallState() },
        };
    }

    public void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(IActionState actionState)
    {
        if (actionState is not IUpdatedActionState state)
            return;

        _currentState.ExitState(this);

        _currentState = state;

        _currentState.EnterState(this); 
    }
}