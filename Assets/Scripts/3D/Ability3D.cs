using System;
using System.Collections;
using UnityEngine;

public abstract class Ability3D : MonoBehaviour
{
    [Header("Ability Info")] [SerializeField]
    private string nameOfSpell;

    [SerializeField] private float timeToCast = 1f;
    [SerializeField] private float timeToCooldown = 1f;
    [SerializeField] private bool needsTarget;

    protected ICharacter3D Character;
    protected float _cooldownTimer;
    protected bool _isReadyToCast = true;
    protected Coroutine _castRoutine;

    public event Action<float> OnProgress;

    public string NameOfSpell => nameOfSpell;
    public float TimeToCast => timeToCast;
    public float TimeToCooldown => timeToCooldown;
    public bool IsReadyToCast => _isReadyToCast;

    public void Configure(ICharacter3D character)
    {
        Character = character;
        ConfigureSelf();
        Debug.Log($"Configure {gameObject.name}");
        Character.OnMovement += InterruptAbility;
    }

    protected virtual void OnDestroy()
    {
        if (Character != null)
        {
            Character.OnMovement -= InterruptAbility;
        }
    }

    private void Awake()
    {
        needsTarget = RequiresTarget();
    }

    public virtual void StartAbility()
    {
        if (!_isReadyToCast)
        {
            Debug.Log($"Cooldown active. Ready in {timeToCooldown - _cooldownTimer:F2} seconds.");
            return;
        }

        if (RequiresTarget() && (Character.GetTarget() == null))
        {
            Debug.Log("No valid target found!");
            return;
        }

        BeginCasting();
    }

    protected virtual void BeginCasting()
    {
        _isReadyToCast = false;
        _cooldownTimer = 0f;

        if (_castRoutine != null)
            StopCoroutine(_castRoutine);

        _castRoutine = StartCoroutine(CastAbility());
    }

    protected IEnumerator CastAbility()
    {
        float elapsedTime = 0f;
        Character.Animation.PlayCastAnimation();

        while (elapsedTime < timeToCast)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / timeToCast;

            Debug.Log($"progress: {progress}");
            OnProgress?.Invoke(progress);

            yield return null;
        }

        Ability();
        Character.Animation.PlayShootAnimation();
    }

    public virtual void InterruptAbility()
    {
        if (_castRoutine == null) return;

        StopCoroutine(_castRoutine);
        _castRoutine = null;
        Debug.Log("Ability interrupted!");

        OnProgress?.Invoke(0f);

        _isReadyToCast = false;
        _cooldownTimer = 0f;
        Character.Animation.PlayInterruptAnimation();
    }

    protected virtual void ConfigureSelf()
    {
    }

    public abstract void Ability();
    protected abstract bool RequiresTarget(); 

    protected void Update()
    {
        if (!_isReadyToCast)
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= timeToCooldown)
            {
                _isReadyToCast = true;
            }
        }
    }
}
