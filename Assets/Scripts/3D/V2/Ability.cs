using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace V2
{
    public abstract class Ability : MonoBehaviour, IAbility
    {
        [SerializeField] private string nameId;
        [SerializeField] private float cooldown;
        [SerializeField] protected float castTime;
        [SerializeField] protected float actionTime;
        [SerializeField] private float manaCost;
        [SerializeField] private bool isHarmful;
        [SerializeField] private bool isBeneficial;
        [SerializeField] private float maxRange;
        [SerializeField] private bool isTargetRequired;
        public event Action OnStartAbility;
        public event Action OnConditionBefore;
        public event Action<float> OnProgressCast;
        public event Action<float> OnProgressAction;
        public event Action OnConditionAfter;
        public event Action OnCasting;
        public event Action OnCooldown;
        public event Action OnReadyToUse;
        public event Action OnInterrupt;

        public string Id => nameId;
        public float MaxRange => maxRange;

        public bool IsRequiresTarget()
        {
            return isTargetRequired;
        }

        bool IAbility.IsBeneficial()
        {
            return true;
        }

        public bool IsHarmful()
        {
            return true;
        }

        public float ManaCost()
        {
            return manaCost;
        }

        protected ISkillManager skillManager;
        private List<IAbilityCondition> conditions;
        protected CancellationTokenSource cts = new();

        public void SetOwner(ISkillManager manager)
        {
            skillManager = manager;
            conditions = new List<IAbilityCondition>
            {
                new IsNotMovingCondition(),
                new HasValidTargetCondition(),
                new IsWithinRangeCondition(),
                new IsInFieldOfViewCondition(),
                new CannotHarmAlliesCondition(),
                new CannotBuffEnemiesCondition(),
                new HasEnoughResourceCondition()
            };
        }

        public async UniTask<bool> StartAbility()
        {
            OnStartAbility?.Invoke();
            OnConditionBefore?.Invoke();
            foreach (var condition in conditions)
            {
                if (!condition.Validate(this, out string errorMessage))
                {
                    Debug.LogError(errorMessage);
                    return false;
                }
            }

            await CastAbility(cts.Token);
            foreach (var condition in conditions)
            {
                if (!condition.Validate(this, out string errorMessage))
                {
                    Debug.LogError(errorMessage);
                    return false;
                }
            }

            OnConditionAfter?.Invoke();
            OnCasting?.Invoke();
            await PerformAction(cts.Token);

            StartCooldown().Forget();
            return true;
        }

        private async UniTask StartCooldown()
        {
            OnCooldown?.Invoke();
            await UniTask.WaitForSeconds(cooldown);
            ReadyToUseAgain().Forget();
        }

        private async UniTask ReadyToUseAgain()
        {
            await UniTask.Yield();
            OnReadyToUse?.Invoke();
            cts = new CancellationTokenSource();
        }

        public virtual void InterruptAbility()
        {
            cts.Cancel();
            OnInterrupt?.Invoke();
            ReadyToUseAgain().Forget();
        }

        public ICharacter3D Owner => skillManager.GetCharacter();

        protected abstract UniTask CastAbility(CancellationToken ctsToken);
        protected abstract UniTask PerformAction(CancellationToken ctsToken);

        protected void OnProgressCastAction(float progressPercent)
        {
            OnProgressCast?.Invoke(progressPercent);
        }

        protected void OnProgressActionAction(float progressPercent)
        {
            OnProgressAction?.Invoke(progressPercent);
        }
    }
}