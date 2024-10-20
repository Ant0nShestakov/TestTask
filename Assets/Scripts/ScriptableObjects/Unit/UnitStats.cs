using UnityEngine;

public abstract class UnitStats : ScriptableObject
{
    [field: SerializeField, Min(0.1f)] public float WalkingSpeed { get; private set; }
    [field: SerializeField, Min(0.01f)] public float RunSpeedModificator { get; private set; }

    [field: SerializeField, Min(0.1f)] public float Mass { get; private set; }

    [field: SerializeField, Min(0.1f)] public float JumpForce { get; private set; }

    public float CalculateRunSpeed() => WalkingSpeed * RunSpeedModificator;
}
