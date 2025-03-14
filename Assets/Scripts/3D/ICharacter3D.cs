using System;
using UnityEngine;

public interface ICharacter3D
{
    GameObject GetTarget();
    event Action OnMovement;
    ICharacter3DAnimation Animation { get; }
    Transform GetTransform();
    ISpellCharacter GetSpellCharacter();
    void TakeDamage(float damage);
}