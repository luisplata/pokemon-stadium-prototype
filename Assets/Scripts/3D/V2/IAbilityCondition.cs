namespace V2
{
    public interface IAbilityCondition
    {
        bool Validate(IAbility ability, out string errorMessage);
    }
}