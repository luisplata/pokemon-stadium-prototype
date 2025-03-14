using System;
using System.Collections.Generic;
using UnityEngine;

namespace V2
{
    [CreateAssetMenu(menuName = "Custom/Ability configuration")]
    public class AbilitiesConfiguration : ScriptableObject
    {
        [SerializeField] private Ability[] powerUps;
        private Dictionary<string, Ability> idToPowerUp;

        private void Awake()
        {
            idToPowerUp = new Dictionary<string, Ability>(powerUps.Length);
            foreach (var powerUp in powerUps)
            {
                idToPowerUp.Add(powerUp.Id, powerUp);
            }
        }

        public Ability GetAbilityPrefabById(string id)
        {
            if (!idToPowerUp.TryGetValue(id, out var powerUp))
            {
                throw new Exception($"Ability with id {id} does not exit");
            }

            return powerUp;
        }
    }
}