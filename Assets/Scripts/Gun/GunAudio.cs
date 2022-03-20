using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{

    #region Variables

    private AudioSource _audioSource;

    [Header("Firing Audio")]

    [SerializeField] private AudioClip _fireAudio;
    [SerializeField] private AudioClip _reloadAudio;

    #endregion


    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion


    #region Methods

    private void FireAudio()
    {
        _audioSource.PlayOneShot(_fireAudio);
    }

    #endregion

    private void ReloadAudio()
    {
        _audioSource.PlayOneShot(_reloadAudio);
    }

}
