using UnityEngine;

public class PlayerCombatSystem
{
    private Animator _animator;
    private Fist[] _fists;

    public PlayerCombatSystem(Animator animator, Fist[] fists)
    {
        _animator = animator;
        _fists = fists;
    }

    public void Attack(bool isAttacking)
    {
        _animator.SetBool("Attacking", isAttacking);
        foreach (var fist in _fists)
        {
            fist.gameObject.SetActive(isAttacking);
        }
    }
}
