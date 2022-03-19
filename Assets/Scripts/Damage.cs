using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField] private float _damage;

    public void InflictDamage(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }

}
