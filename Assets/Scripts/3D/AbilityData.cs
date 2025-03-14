using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityData", menuName = "Abilities/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public GameObject abilityPrefab;
}