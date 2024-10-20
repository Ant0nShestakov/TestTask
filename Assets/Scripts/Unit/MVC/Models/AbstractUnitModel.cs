using System;

public abstract class AbstractUnitModel
{
    protected readonly UnitStats stats;

    protected float currentMass;

    public float CurrentSpeed { get; protected set; }
    public float CurrentJumpForce { get => stats.JumpForce; }
    public float CurrentMass { get => currentMass; }


    public AbstractUnitModel(UnitStats stats)
    {
        this.stats = stats;

        currentMass = stats.Mass;
        CurrentSpeed = stats.WalkingSpeed;
    }
}