using UnityEngine;

public class AreaHealAbility3D : GroundTargetAbility
{
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private float healAmount = 30f;
    [SerializeField] private float healRadius = 5f;

    private GameObject indicator;

    private void Start()
    {
        OnSelectingPointToCast += UpdateIndicatorPosition;

        if (indicatorPrefab != null)
        {
            indicator = Instantiate(indicatorPrefab);
            indicator.SetActive(false);
        }
    }

    private void UpdateIndicatorPosition(Vector3 position)
    {
        Debug.Log($"🔵 Mostrando indicador de curación en {position}");

        if (indicator != null)
        {
            indicator.transform.position = position;
            indicator.SetActive(true);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnSelectingPointToCast -= UpdateIndicatorPosition;
    }

    public override void Ability()
    {
        OnAbilityActivated(_targetPosition);
    }

    protected override bool RequiresTarget()
    {
        return false;
    }

    private new void OnAbilityActivated(Vector3 position)
    {
        Debug.Log($"💚 Lanzando curación en área en {position}");
        ApplyHealing(position);

        if (indicator != null)
        {
            indicator.SetActive(false);
        }
    }

    private void ApplyHealing(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, healRadius);

        foreach (Collider collider in colliders)
        {
            IHealable healableEntity = collider.GetComponent<IHealable>();
            if (healableEntity != null)
            {
                healableEntity.ReceiveHeal(healAmount);
                Debug.Log($"💚 {collider.name} ha sido curado por {healAmount} HP!");
            }
        }
    }

    public override void InterruptAbility()
    {
        base.InterruptAbility();
        if (indicator != null)
        {
            indicator.SetActive(false);
        }
    }
}