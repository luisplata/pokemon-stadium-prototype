namespace V2
{
    public interface ISkillManager
    {
        void Configure(ICharacter3D character3D);
        void InterruptAbility();
        void EquipSkill(int slot, IAbility ability);
        void UnequipSkill(int slot);
        void ActivateSkill(int slot);
        ICharacter3D GetCharacter();
    }
}