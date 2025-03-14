namespace V2
{
    public class IsNotMovingCondition : IAbilityCondition
    {
        public bool Validate(IAbility ability, out string errorMessage)
        {
            if (ability.Owner.IsMoving())
            {
                errorMessage = "No puedes lanzar habilidades mientras te mueves.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}