using UnityEngine;

public class PlayerFist : Fist
{
    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().RecieveDamage(Damage);
        }
    }
}
