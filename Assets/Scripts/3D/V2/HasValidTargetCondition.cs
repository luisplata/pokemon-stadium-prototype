namespace V2
{
    public class HasValidTargetCondition : IAbilityCondition
    {
        public bool Validate(IAbility ability, out string errorMessage)
        {
            if (ability.IsRequiresTarget() && ability.Owner.GetTarget() == null)
            {
                errorMessage = "No tienes un objetivo válido.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}