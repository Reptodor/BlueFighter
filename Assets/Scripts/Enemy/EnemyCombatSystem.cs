using UnityEngine;

public class EnemyCombatSystem
{
    private Animator _animator;

    public EnemyCombatSystem(Animator animator)
    {
        _animator = animator;
    }

    public void Attack(Fist[] fists)
    {
        _animator.SetBool("Attacking", true);
        foreach (var fist in fists)
        {
            fist.gameObject.SetActive(true);
        }
    }
}
