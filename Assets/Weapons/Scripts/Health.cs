using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health :MonoBehaviour
{
    public MenuScript gameManager;
    #region Variables

    [SerializeField] public float _health;

    [SerializeField] private UnityEvent _onDie;

    [SerializeField] private UnityEvent _onTakeDamage;

    #endregion

    #region Methods
    private bool running = false;
    public void TakeDamage(float damage)
    {
        _health -= damage;

        _onTakeDamage.Invoke();

        if (_health <= 0f && !running)
        {
            running = true;
            Die();
        }
        
    }

    private void Die()
    {
        gameManager.Count();
        _onDie.Invoke();
    }

    #endregion

}
