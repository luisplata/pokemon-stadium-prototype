using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour, IAbilityEquipper
{
    private Dictionary<int, Ability3D> equippedAbilities = new Dictionary<int, Ability3D>();
    private ICharacter3D character;

    public void Configure(ICharacter3D character)
    {
        this.character = character;
    }

    public void EquipAbility(int slot, AbilityData abilityData)
    {
        if (equippedAbilities.ContainsKey(slot))
        {
            UnequipAbility(slot);
        }

        GameObject abilityInstance = Instantiate(abilityData.abilityPrefab, transform);
        Ability3D ability = abilityInstance.GetComponent<Ability3D>();

        if (ability != null)
        {
            equippedAbilities[slot] = ability;
            ability.Configure(character);
        }
    }

    public void UnequipAbility(int slot)
    {
        if (equippedAbilities.TryGetValue(slot, out Ability3D ability))
        {
            Destroy(ability.gameObject);
            equippedAbilities.Remove(slot);
        }
    }

    public void UseAbility(int slot)
    {
        if (equippedAbilities.TryGetValue(slot, out Ability3D ability))
        {
            ability.StartAbility();
        }
    }
}