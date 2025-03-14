using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace V2
{
    public class SkillManager : MonoBehaviour, ISkillManager
    {
        public const int MaxSkillSlots = 4;
        private readonly IAbility[] skillSlots = new IAbility[MaxSkillSlots];

        public event Action<int, IAbility> OnSkillEquipped;
        public event Action<int> OnSkillUnequipped;

        private ICharacter3D character;
        private IAbility currentAbility;

        public void Configure(ICharacter3D character3D)
        {
            character = character3D;
        }

        public void EquipSkill(int slot, IAbility ability)
        {
            if (slot < 0 || slot >= MaxSkillSlots) return;
            skillSlots[slot] = ability;
            ability.SetOwner(this);
            OnSkillEquipped?.Invoke(slot, ability);
        }

        public void UnequipSkill(int slot)
        {
            if (slot < 0 || slot >= MaxSkillSlots || skillSlots[slot] == null) return;
            skillSlots[slot] = null;
            OnSkillUnequipped?.Invoke(slot);
        }

        public void ActivateSkill(int slot)
        {
            slot--;
            Debug.Log($"Slill {slot}");
            if (slot < 0 || slot >= MaxSkillSlots || skillSlots[slot] == null)
            {
                return;
            }

            currentAbility = skillSlots[slot];
            currentAbility?.StartAbility().Forget();
        }

        public void InterruptAbility()
        {
            currentAbility?.InterruptAbility();
        }

        public ICharacter3D GetCharacter()
        {
            return character;
        }
    }
}