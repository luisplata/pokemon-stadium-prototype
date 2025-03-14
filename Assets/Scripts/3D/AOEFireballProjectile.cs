using UnityEngine;
using System.Collections;

public class AOEFireballProjectile : Projectile3D
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private float explosionDelay = 1.5f; // Esperar antes de aplicar daño

    protected override bool ShouldHit(Collider other) => false; // No colisiona

    protected override void ApplyDamage(Collider target)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            ICharacter3D enemy = collider.GetComponent<ICharacter3D>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"🔥 AOE Fireball damaged {collider.name} for {damage} HP!");
            }
        }
    }

    public override void Launch(Vector3 direction, float projectileSpeed, float projectileDamage)
    {
        damage = projectileDamage;
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        if (explosionVFX != null)
        {
            GameObject vfxInstance = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(explosionDelay);
            Destroy(vfxInstance);
        }

        // ApplyDamage(null);
        // Destroy(gameObject);
    }
}