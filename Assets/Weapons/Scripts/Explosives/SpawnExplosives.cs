using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosives : MonoBehaviour
{

    #region Variables

    [SerializeField] private GameObject _explosives;
    [SerializeField] private LayerMask _otherMask;
    private bool _spawnable = true;
    #endregion


    #region MonoBehavior

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        if (_spawnable)
        {
            RandomSpawn();
        }

    }
    #endregion


    #region Methods

    private void RandomSpawn()
    {

        Vector3 rp = RandomPosition();
        if (rp != Vector3.zero)
        {
            Instantiate(_explosives, rp, Quaternion.identity);

        }
        else
        {
            _spawnable = false;
        }

    }

    private Vector3 RandomPosition(int time = 0)
    {
        if (time > 20)
            return Vector3.zero;

        float x = Random.Range(-50, 50);
        float z = Random.Range(-50, 50);

        Vector3 spherePos = new Vector3(x, 1f, z);
        if (Physics.CheckSphere(spherePos, 5f, _otherMask))
            return RandomPosition(++time);
        return spherePos;

    }

    #endregion

}
