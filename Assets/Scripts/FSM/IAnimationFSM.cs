using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationFSM
{
    public Animator Animator { get; }

    public IReadOnlyDictionary<Type, IActionState> States { get; }

    public void SwitchState(IActionState actionState);
}