using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationFSM
{
    public Animator Animator { get; }

    public IReadOnlyDictionary<Type, ActionState> States { get; }

    public IActionStateVisitor Visitor { get; }

    public void Update();

    public void SwitchState(ActionState actionState);
}