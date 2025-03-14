namespace V2
{
    public class CannotBuffEnemiesCondition : IAbilityCondition
    {
        public bool Validate(IAbility ability, out string errorMessage)
        {
            var target = ability.Owner.GetTarget();
            if (target != null && ability.IsBeneficial() && !ability.Owner.IsAlly(target))
            {
                errorMessage = "No puedes lanzar habilidades beneficiosas a enemigos.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}