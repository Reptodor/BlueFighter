using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement
{
    private NavMeshAgent _enemy;
    private Animator _animator;
    private Transform _enemyTarget;

    public EnemyMovement(NavMeshAgent enemy, Animator animator, Transform enemyTarget)
    {
        _enemy = enemy;
        _animator = animator;
        _enemyTarget = enemyTarget;
    }

    public void ChasePlayer(Fist[] fists)
    {
        _animator.SetBool("Attacking", false);
        foreach (var fist in fists)
        {
            fist.gameObject.SetActive(false);
        }
        _animator.SetBool("IsMoving", true);
        _enemy.SetDestination(_enemyTarget.transform.position);

    }
}
