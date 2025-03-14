using Cysharp.Threading.Tasks;

namespace V2
{
    public interface IAbility
    {
        void SetOwner(ISkillManager manager);
        UniTask<bool> StartAbility();
        void InterruptAbility();
        ICharacter3D Owner { get;}
        float MaxRange { get; }
        bool IsRequiresTarget();
        bool IsBeneficial();
        bool IsHarmful();
        float ManaCost();
    }
}