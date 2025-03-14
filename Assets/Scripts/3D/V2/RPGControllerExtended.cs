using JohnStairs.RCC.Character.Motor;
using UnityEngine;

namespace V2
{
    public class RPGControllerExtended : MonoBehaviour
    {
        [SerializeField] private RPGMotorMMO motor;
        [SerializeField] private RPGController controller;

        public void SetExternalMovement(Vector2 direction2D)
        {
            var dir = new Vector3(direction2D.x, 0, direction2D.y);
            motor.Move(dir * Time.deltaTime);
        }

        public void ActivateControls()
        {
            controller.ActivateControls();
        }

        public void DeactivateControls()
        {
            controller.DeactivateControls();
        }
    }
}