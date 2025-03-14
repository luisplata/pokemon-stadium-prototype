using UnityEngine;

namespace V2
{
    public interface ICharacter3D
    {
        bool IsMoving();
        ITarget GetTarget();
        GameObject GetGameObject();
        bool IsInFieldOfView(Transform transform);
        bool IsAlly(ITarget target);
        bool IsGrounded();
        float CurrentMana();
        void GoTo(Vector2 direction2D);
        void IsControlActivate(bool isEnable);
    }
}