using UnityEngine;

public class ThrowSpell : Ability3D
{
    protected override void ConfigureSelf()
    {
    }

    public override void Ability()
    {
        Debug.Log("Lunch FireBall");
    }

    protected override bool RequiresTarget()
    {
        return false;
    }
}