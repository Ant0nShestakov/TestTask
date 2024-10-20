using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Settings/Physics")]
public class PhysicsSettings : ScriptableObject 
{
    [field:SerializeField, Min(0.1f)] public float MinMass {  get; private set; }

    [field: SerializeField, Min(0.1f)] public float MiddleMass { get; private set; }
    [field: SerializeField, Min(0.01f)] public float MiddleMassDebuff { get; private set; }

    [field: SerializeField, Min(0.1f)] public float CapMass { get; private set; }
    [field: SerializeField, Min(0.01f)] public float CapMassDebuff { get; private set; }

    public void CalculateDirection(ref Vector3 direction, float mass)
    {
        if (mass <= MinMass)
            return;

        if (mass <= MiddleMass)
        {
            direction *= MiddleMassDebuff;
            return;
        }

        direction *= CapMassDebuff;
    }
}
