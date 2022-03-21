using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosives : MonoBehaviour
{

    #region Variables

    [SerializeField] private GameObject _barrel;
    [SerializeField] private GameObject _gasTank;
    [SerializeField] private LayerMask _otherMask;
    private int maxSpawn = 100;
    private int spawnCount = 0;
    public MenuScript gameManager;
    #endregion


    #region MonoBehavior

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {

        if (spawnCount < maxSpawn)
        {
            RandomSpawn();
        }

    }
    #endregion


    #region Methods

    private void RandomSpawn()
    {

        float x = Random.Range(-40, 40);
        float z = Random.Range(-40, 40);

        Vector3 rp = new Vector3(x, 10f, z);

        float r = Random.Range(-1, 1);

        if (r >= 0)
        {
            _barrel.GetComponent<Health>().gameManager = gameManager;
            Instantiate(_barrel, rp, Quaternion.identity);
        }
        else if (r < 0)
        {
            _gasTank.GetComponent<Health>().gameManager = gameManager;
            Instantiate(_gasTank, rp, Quaternion.identity);
        }

        spawnCount++;

    }

    #endregion

}
