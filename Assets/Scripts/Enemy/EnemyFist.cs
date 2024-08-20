using UnityEngine;

public class EnemyFist : Fist
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().RecieveDamage(Damage);
        }
    }
}
