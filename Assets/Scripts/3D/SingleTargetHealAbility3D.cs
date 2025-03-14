using UnityEngine;

public class SingleTargetHealAbility3D : Ability3D
{
    [SerializeField] private float healAmount = 50f; // Cantidad de vida a curar

    protected override void ConfigureSelf()
    {
        Debug.Log($"{NameOfSpell} ready to heal a single target!");
    }

    public override void Ability()
    {
        GameObject target = Character.GetTarget();

        if (target == null)
        {
            Debug.Log("No valid target found for healing!");
            return;
        }

        IHealable healableTarget = target.GetComponent<IHealable>();

        if (healableTarget != null)
        {
            healableTarget.ReceiveHeal(healAmount);
            Debug.Log($"{NameOfSpell} healed {target.name} for {healAmount} HP!");
        }
        else
        {
            Debug.Log("Target is not healable!");
        }
    }

    protected override bool RequiresTarget()
    {
        return true; // Necesita un objetivo específico
    }
}