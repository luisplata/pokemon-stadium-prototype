using System.Collections;
using UnityEngine;

public abstract class Projectile3D : MonoBehaviour
{
    protected float damage;
    protected float speed;
    protected bool hasHit = false;
    
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private GameObject impactVFX;
    [SerializeField] private float destroyDelay = 1.5f;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _renderer;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    public virtual void Launch(Vector3 direction, float projectileSpeed, float projectileDamage)
    {
        _direction = direction;
        speed = projectileSpeed;
        damage = projectileDamage;

        Destroy(gameObject, lifetime); // Destruir si no golpea nada
    }

    private void Update()
    {
        if (!hasHit)
        {
            transform.position += _direction * (speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        if (ShouldHit(other))
        {
            HandleImpact(other);
        }
    }

    protected abstract bool ShouldHit(Collider other);
    protected abstract void ApplyDamage(Collider target);

    private void HandleImpact(Collider other)
    {
        hasHit = true;
        speed = 0f;

        if (_collider) _collider.enabled = false;
        if (_renderer) _renderer.enabled = false;

        if (impactVFX != null)
        {
            GameObject vfxInstance = Instantiate(impactVFX, transform.position, Quaternion.identity);
            StartCoroutine(DelayedDamage(vfxInstance));
        }
        else
        {
            ApplyDamage(other);
            Destroy(gameObject, destroyDelay);
        }
    }

    private IEnumerator DelayedDamage(GameObject vfxInstance)
    {
        yield return new WaitForSeconds(destroyDelay);
        ApplyDamage(null);
        Destroy(vfxInstance);
        Destroy(gameObject);
    }
}