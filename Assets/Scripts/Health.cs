using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health :MonoBehaviour
{

    #region Variables

    [SerializeField] private float health;

    [SerializeField] private UnityEvent onDie;

    #endregion

    #region Methods

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0f)
            Die();
        
    }

    private void Die()
    {
        onDie.Invoke();
    }

    #endregion
}
