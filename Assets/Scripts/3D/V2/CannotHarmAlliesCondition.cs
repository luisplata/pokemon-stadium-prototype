namespace V2
{
    public class CannotHarmAlliesCondition : IAbilityCondition
    {
        public bool Validate(IAbility ability, out string errorMessage)
        {
            var target = ability.Owner.GetTarget();
            if (target != null && ability.IsHarmful() && ability.Owner.IsAlly(target))
            {
                errorMessage = "No puedes lanzar habilidades dañinas a aliados.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}