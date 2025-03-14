using UnityEngine;
using System.Collections;

public class BigFireBall : GroundTargetAbility
{
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField] private GameObject castVFXPrefab;
    [SerializeField] private AOEFireballProjectile fireballPrefab;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float castTime = 2f;

    private GameObject indicator;
    private GameObject _castVFX;

    private void Start()
    {
        OnSelectingPointToCast += ShowIndicator;
        OnPointToCast += StartCasting;
        OnAbilityActivated += SpawnFireball;

        if (indicatorPrefab != null)
        {
            indicator = Instantiate(indicatorPrefab);
            indicator.SetActive(false);
        }
    }

    private void ShowIndicator(Vector3 position)
    {
        if (indicator != null)
        {
            indicator.transform.position = position;
            indicator.SetActive(true);
        }
    }

    private void StartCasting(Vector3 position)
    {
        indicator.SetActive(false);

        if (castVFXPrefab != null)
        {
            position += Vector3.up * 0.1f;
            _castVFX = Instantiate(castVFXPrefab, position, castVFXPrefab.transform.rotation);
        }

        StartAbility();
    }

    private void SpawnFireball(Vector3 position)
    {
        if (_castVFX != null)
        {
            _castVFX.SetActive(false);
        }

        StartCoroutine(ExecuteFireball(position));
    }

    private IEnumerator ExecuteFireball(Vector3 position)
    {
        yield return new WaitForSeconds(castTime);

        if (fireballPrefab != null)
        {
            AOEFireballProjectile fireball = Instantiate(fireballPrefab, position, Quaternion.identity);
            fireball.Launch(Vector3.zero, 0, damage);
            Destroy(fireball.gameObject, 5);
        }
    }

    public override void InterruptAbility()
    {
        base.InterruptAbility();
        if (indicator != null) indicator.SetActive(false);
    }

    protected override bool RequiresTarget()
    {
        return false;
    }
}
