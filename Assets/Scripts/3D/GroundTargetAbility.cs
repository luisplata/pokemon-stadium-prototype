using System;
using UnityEngine;

public abstract class GroundTargetAbility : Ability3D
{
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float maxRange = 10f;

    public event Action<Vector3> OnSelectingPointToCast;
    public event Action<Vector3> OnPointToCast;
    public event Action<Vector3> OnAbilityActivated;

    protected Vector3 _targetPosition;
    private bool _isSelectingTarget;

    public override void StartAbility()
    {
        _isSelectingTarget = true;
    }

    public override void Ability()
    {
        OnAbilityActivated?.Invoke(_targetPosition);
    }

    private void Update()
    {
        if (_isSelectingTarget)
        {
            if (TryGetGroundPoint(out _targetPosition))
            {
                _targetPosition = ClampToMaxRange(_targetPosition);
                OnSelectingPointToCast?.Invoke(_targetPosition);
            }

            if (Input.GetMouseButtonDown(0) && TryGetGroundPoint(out _targetPosition))
            {
                _isSelectingTarget = false;
                _targetPosition = ClampToMaxRange(_targetPosition);
                OnPointToCast?.Invoke(_targetPosition);
                ConfirmTargetAndCast();
            }

            if (Input.GetMouseButtonDown(1)) InterruptAbility();
        }
    }

    private void ConfirmTargetAndCast()
    {
        BeginCasting();
        OnAbilityActivated?.Invoke(_targetPosition);
    }

    private Vector3 ClampToMaxRange(Vector3 targetPoint)
    {
        Vector3 origin = transform.position;
        Vector3 direction = targetPoint - origin;

        return direction.magnitude > maxRange ? origin + direction.normalized * maxRange : targetPoint;
    }

    private bool TryGetGroundPoint(out Vector3 groundPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayers))
        {
            groundPoint = hit.point;
            return true;
        }

        groundPoint = Vector3.zero;
        return false;
    }
}