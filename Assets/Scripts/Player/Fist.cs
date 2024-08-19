using UnityEngine;

public class Fist : MonoBehaviour
{
    private int _damage;

    public void Initialize(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            Debug.Log("Damaged");
            other.gameObject.GetComponent<Enemy>().RecieveDamage(_damage);
        }
    }
}
