using UnityEngine;

namespace Multiplayer.Camera
{
    public abstract class BaseCameraController : MonoBehaviour, ICameraController
    {
        protected Transform target;

        public virtual void Initialize() { }
        public virtual void SetTarget(Transform target) => this.target = target;
        public abstract void UpdateCamera();
    }
}