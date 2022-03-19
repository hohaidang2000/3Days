using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{

    #region Variables

    private float _waitTime = 5f;

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
        Destroy(gameObject);
    }

    #endregion

}
