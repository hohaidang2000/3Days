using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health :MonoBehaviour
{

    #region Variables

    [SerializeField] private float _health;

    [SerializeField] private UnityEvent _onDie;

    [SerializeField] private UnityEvent _onTakeDamage;

    #endregion

    #region Methods

    public void TakeDamage(float damage)
    {
        _health -= damage;

        _onTakeDamage.Invoke();

        if (_health <= 0f)
            Die();
        
    }

    private void Die()
    {
        _onDie.Invoke();
    }

    #endregion

}
