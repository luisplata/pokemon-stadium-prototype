namespace V2
{
    public class HasEnoughResourceCondition : IAbilityCondition
    {
        public bool Validate(IAbility ability, out string errorMessage)
        {
            if (ability.Owner.CurrentMana() < ability.ManaCost())
            {
                errorMessage = "No tienes suficiente maná.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}