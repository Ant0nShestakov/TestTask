using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemFSM : IAnimationFSM
{
    private readonly Animator _animator;

    private Dictionary<Type, IActionState> _states;
    private IActionState _currentState;

    public Animator Animator => _animator;

    public IReadOnlyDictionary<Type, IActionState> States => _states;

    public ItemFSM(Animator animator)
    {
        _animator = animator;

        Initialize();

        _currentState = _states.GetValueOrDefault(typeof(Dropped));
        _currentState.EnterState(this);
    }

    private void Initialize()
    {
        _states = new Dictionary<Type, IActionState>
        {
            { typeof(PickedUp), new PickedUp() },
            { typeof(Dropped), new Dropped() },
        };
    }

    public void SwitchState(IActionState actionState)
    {
        _currentState.ExitState(this);

        _currentState = actionState ?? throw new NullReferenceException();

        _currentState.EnterState(this);
    }
}