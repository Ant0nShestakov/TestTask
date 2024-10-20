using UnityEngine;

[CreateAssetMenu(fileName = "PorterStats", menuName = "ScriptableObjects/UnitStats/Porter")]
public class PorterStats : UnitStats
{
    [field:SerializeField] public int CarryCapacity {  get; private set; }
}
