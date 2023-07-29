using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipsRefSO _audioClipsRefSO;

    private float _volume;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one SoundManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _volume = 0.5f;
    }


    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f, bool isGamePlayingRequired = false)
    {
        if (!isGamePlayingRequired)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, _volume * volumeMultiplier);
        }
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f, bool isGamePlayingRequired = false)
    {

        if (!isGamePlayingRequired)
        {
            PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier, isGamePlayingRequired);
        }
    }

    public void PlayPlopSound(float volume)
    {
        PlaySound(_audioClipsRefSO.PlopSound, Camera.main.transform.position, volume);
    }

    public float GetVolume()
    {
        return _volume;
    }

}
