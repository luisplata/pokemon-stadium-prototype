using Multiplayer.Camera;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class FollowCameraController : BaseCameraController
{
    private CinemachineCamera cinemachineCamera;
    private CinemachineComponentBase followComponent;

    public override void Initialize()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();

        if (target != null)
        {
            cinemachineCamera.Follow = target;
            cinemachineCamera.LookAt = target;
        }
    }

    public override void SetTarget(Transform target)
    {
        base.SetTarget(target);
        cinemachineCamera.Follow = target;
        cinemachineCamera.LookAt = target;
    }

    public override void UpdateCamera()
    {
        // Puedes agregar lógica adicional aquí si quieres manipular la cámara en cada frame
    }
}