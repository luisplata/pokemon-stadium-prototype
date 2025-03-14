using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace V2
{
    public class HealAbility : Ability
    {
        [SerializeField] private float healAmount = 50f;
        private IHealable target;


        protected override async UniTask CastAbility(CancellationToken ctsToken)
        {
            float elapsedTime = 0f; // Variable para contar el tiempo

            while (elapsedTime < castTime)
            {
                elapsedTime += Time.deltaTime;

                await UniTask.Yield(cancellationToken: cts.Token);

                var progress = elapsedTime / castTime;
                OnProgressCastAction(progress);
            }
        }

        protected override async UniTask PerformAction(CancellationToken ctsToken)
        {
            await UniTask.Yield(cancellationToken: ctsToken);
            target.ReceiveHeal(healAmount);
        }
    }
}