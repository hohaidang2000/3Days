using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
#region Variables

    private AudioSource _audioSource;

    [Header("Player Audio")]

    [SerializeField] private AudioClip _takeDameAudio;

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

    public void TakeDameAudio()
    {
        _audioSource.PlayOneShot(_takeDameAudio);
    }

    #endregion


}
