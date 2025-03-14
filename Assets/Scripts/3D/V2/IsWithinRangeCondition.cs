using UnityEngine;

namespace V2
{
    public class IsWithinRangeCondition : IAbilityCondition
    {
        public bool Validate(IAbility ability, out string errorMessage)
        {
            var target = ability.Owner.GetTarget();
            if (target != null && Vector3.Distance(ability.Owner.GetGameObject().transform.position,
                    target.GetGameObject().transform.position) > ability.MaxRange)
            {
                errorMessage = "El objetivo está fuera de rango.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}