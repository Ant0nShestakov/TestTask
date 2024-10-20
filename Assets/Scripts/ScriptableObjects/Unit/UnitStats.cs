using UnityEngine;

public abstract class UnitStats : ScriptableObject
{
    [field: SerializeField] public float WalkingSpeed { get; private set; }
    [field: SerializeField] public float RunSpeedModificator { get; private set; }

    [field: SerializeField] public float Mass { get; private set; }

    [field: SerializeField] public float JumpForce { get; private set; }

    public float CalculateRunSpeed() => WalkingSpeed * RunSpeedModificator;
}
