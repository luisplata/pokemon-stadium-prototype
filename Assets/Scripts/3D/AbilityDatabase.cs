using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDatabase", menuName = "Abilities/AbilityDatabase")]
public class AbilityDatabase : ScriptableObject
{
    public AbilityData[] abilities;
}