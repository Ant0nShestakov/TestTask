using UnityEngine;

[CreateAssetMenu(fileName = "ItemStats", menuName = "ScriptableObjects/ItemStats")]
public class ItemStats : ScriptableObject
{
    [field: SerializeField, Min(0.1f)] public float Mass {  get; set; }
}