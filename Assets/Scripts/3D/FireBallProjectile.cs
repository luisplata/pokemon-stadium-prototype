using UnityEngine;

public class FireBallProjectile : Projectile3D
{
    protected override bool ShouldHit(Collider other)
    {
        return other.GetComponent<ICharacter3D>() != null;
    }

    protected override void ApplyDamage(Collider target)
    {
        if (target != null)
        {
            ICharacter3D enemy = target.GetComponent<ICharacter3D>();
            enemy?.TakeDamage(damage);
            Debug.Log($"🔥 Fireball hit {target.name}, dealing {damage} damage!");
        }
    }
}