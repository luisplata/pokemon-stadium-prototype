using UnityEngine;

public class Character3DAnimation : MonoBehaviour, ICharacter3DAnimation
{
    [SerializeField] private Animator animator;

    private static readonly int Shoot = Animator.StringToHash("shoot");
    private static readonly int IsCasting = Animator.StringToHash("is_casting");

    public void PlayCastAnimation()
    {
        animator?.SetBool(IsCasting, true);
    }

    public void PlayShootAnimation()
    {
        animator?.SetTrigger(Shoot);
    }

    public void PlayInterruptAnimation()
    {
        animator?.SetBool(IsCasting, false);
    }
}


public interface ICharacter3DAnimation
{
    void PlayCastAnimation();
    void PlayShootAnimation();
    void PlayInterruptAnimation();
}