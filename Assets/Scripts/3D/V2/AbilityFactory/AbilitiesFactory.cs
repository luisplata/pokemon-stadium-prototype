using UnityEngine;

namespace V2
{
    public class AbilitiesFactory
    {
        private readonly AbilitiesConfiguration abilitiesConfiguration;

        public AbilitiesFactory(AbilitiesConfiguration abilitiesConfiguration)
        {
            this.abilitiesConfiguration = abilitiesConfiguration;
        }

        public Ability Create(string id)
        {
            var prefab = abilitiesConfiguration.GetAbilityPrefabById(id);

            return Object.Instantiate(prefab);
        }
    }
}