using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class MovementFSM : IMovementFSM
{
    private readonly Animator _animator;
    private readonly InputManager _inputManager;
    private readonly PhysicsController _physicsController;

    private readonly IActionStateVisitor _fSMVisitor;

    private ActionState _currentState;

    private Dictionary<Type, ActionState> _states;

    public bool IsGrounded => _physicsController.IsGrounded;

    public IReadOnlyDictionary<Type, ActionState> States => _states;

    public Animator Animator => _animator;

    public IActionStateVisitor Visitor => _fSMVisitor;

    public MovementFSM(UnitView unitView, IActionStateVisitor fSMVisitor)
    {
        _animator = unitView.GetComponent<Animator>();
        _inputManager = unitView.GetComponent<InputManager>();
        _physicsController = unitView.GetComponent<PhysicsController>();
        _fSMVisitor = fSMVisitor;

        Initialize();

        _currentState = _states.GetValueOrDefault(typeof(WalkState));
    }

    private void Initialize()
    {
        _states = new Dictionary<Type, ActionState>
        {
            { typeof(WalkState), new WalkState(_inputManager) },
            { typeof(RunState), new RunState(_inputManager) },
            { typeof(FallState), new FallState(_inputManager) },
        };
    }

    public void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(ActionState actionState)
    {
        _currentState.ExitState(this);

       _currentState = actionState ?? throw new NullReferenceException();

        _currentState.EnterState(this); 
    }
}