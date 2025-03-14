using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace V2
{
    public class ChargeAbility : Ability
    {
        [SerializeField] private float chargeDistance = 5f;
        [SerializeField] private float chargeSpeed = 10f;
        [SerializeField] private float damage = 20f;
        private Vector3 targetPosition;

        protected override async UniTask CastAbility(CancellationToken ctsToken)
        {
            await UniTask.WaitForSeconds(castTime, cancellationToken: cts.Token);
        }

        public override void InterruptAbility()
        {
            base.InterruptAbility();
            skillManager.GetCharacter().IsControlActivate(true);
        }

        protected override async UniTask PerformAction(CancellationToken ctsToken)
        {
            targetPosition = skillManager.GetCharacter().GetGameObject().transform.position +
                             skillManager.GetCharacter().GetGameObject().transform.forward * chargeDistance;
            skillManager.GetCharacter().IsControlActivate(false);

            float elapsedTime = 0f; // Variable para contar el tiempo

            while (
                Vector3.Distance(skillManager.GetCharacter().GetGameObject().transform.position, targetPosition) >
                0.1f &&
                elapsedTime < actionTime
            ) // Condición extra para salir
            {
                Vector3 direction3D = (targetPosition - skillManager.GetCharacter().GetGameObject().transform.position)
                    .normalized;
                Vector2 direction2D = new Vector2(direction3D.x, direction3D.z);
                //Vector3 direction2D = Vector2.up;
                skillManager.GetCharacter().GoTo(direction2D * chargeSpeed);


                // Verificar colisiones
                var colliders =
                    Physics.OverlapSphere(skillManager.GetCharacter().GetGameObject().transform.position, 1.5f);
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out IDamageable target))
                    {
                        target.TakeDamage(damage);
                    }
                }

                // Incrementar el tiempo transcurrido
                elapsedTime += Time.deltaTime;

                var progress = elapsedTime / actionTime;
                OnProgressActionAction(progress);

                await UniTask.Yield(cancellationToken: cts.Token);
            }

            skillManager.GetCharacter().IsControlActivate(true);
        }
    }
}