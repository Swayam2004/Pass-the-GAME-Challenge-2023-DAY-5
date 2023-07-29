using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float _maxMusicVolume;

    private AudioSource _musicAudioSource;

    private void Awake()
    {
        _musicAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_musicAudioSource.volume < _maxMusicVolume) 
        {
            _musicAudioSource.volume += Time.deltaTime;
        }
        else
        {
            _musicAudioSource.volume = _maxMusicVolume;
        }
    }
}
