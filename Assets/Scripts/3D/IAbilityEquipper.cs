public interface IAbilityEquipper
{
    void EquipAbility(int slot, AbilityData abilityData);
    void UnequipAbility(int slot);
    void UseAbility(int slot);
}