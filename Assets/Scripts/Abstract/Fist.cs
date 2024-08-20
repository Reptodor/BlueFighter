using UnityEngine;

public abstract class Fist : MonoBehaviour
{
    protected int Damage;

    public virtual void Initialize(int damage)
    {
        Damage = damage;
    }

    public abstract void OnTriggerEnter(Collider other);
}
