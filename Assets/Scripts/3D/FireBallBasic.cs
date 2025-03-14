using UnityEngine;

public class FireBallBasic : Ability3D
{
    [SerializeField] private FireBallProjectile fireballPrefab;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float speed = 12f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireballSpeed = 10f;

    protected override void ConfigureSelf()
    {
        if (fireballPrefab == null)
        {
            Debug.LogError("FireBallBasic: Fireball prefab is missing!");
        }

        spawnPoint = Character.GetSpellCharacter().GetPointToSpawn();
    }

    public override void Ability()
    {
        var target = Character.GetTarget();
        if (target == null)
        {
            Debug.Log("Fireball failed: No valid target.");
            return;
        }

        FireBallProjectile fireball =
            Instantiate(fireballPrefab, Character.GetTransform().position, Quaternion.identity);
        Vector3 direction = (target.transform.position - Character.GetTransform().position).normalized;
        fireball.Launch(direction, speed, damage);

        Debug.Log($"🔥 {NameOfSpell} launched at {target.name}");
    }

    protected override bool RequiresTarget()
    {
        return true;
    }
}