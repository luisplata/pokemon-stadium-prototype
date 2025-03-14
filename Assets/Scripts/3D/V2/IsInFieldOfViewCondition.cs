namespace V2
{
    public class IsInFieldOfViewCondition : IAbilityCondition
    {
        public bool Validate(IAbility ability, out string errorMessage)
        {
            var target = ability.Owner.GetTarget();
            if (target != null && !ability.Owner.IsInFieldOfView(target.GetGameObject().transform))
            {
                errorMessage = "El objetivo no está en tu campo de visión.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}