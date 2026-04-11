using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _musicPlayer;
    [SerializeField] AudioSource _audioPlayer;

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    void Awake ()
    {
        if(_instance != null) Destroy(gameObject);
        _instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        _audioPlayer.PlayOneShot(clip);
    }

    public void PlayClipAtLocation(AudioClip clip, Vector3 location)
    {
        AudioSource.PlayClipAtPoint(clip, location);
    }

    public void PlayMusicLoop(AudioClip clip)
    {
        _musicPlayer.clip = clip;
        _musicPlayer.loop = true;
        _musicPlayer.Play();
    }
}