using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelfDestruction : MonoBehaviour
{

    #region Variables

    [SerializeField] private float _waitTime = 5f;
    [SerializeField] private UnityEvent _onSelfDestruction;

    #endregion


    #region MonoBehaviour

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    #endregion


    #region Methods

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(_waitTime);
        _onSelfDestruction.Invoke();
        Destroy(gameObject);
    }

    #endregion

}
