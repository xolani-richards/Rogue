using UnityEngine;

public class PlayOnAwake : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] bool atLocation;
    bool isPlaying = false;

    void Awake()
    {
        if (AudioManager.Instance == null) return;
        else Play();
    }

    void Start()
    {
        if (!isPlaying) Play();
    }

    void Play() {
        if (atLocation) AudioManager.Instance.PlayClipAtLocation(clip, transform.position);
        else AudioManager.Instance.PlayClip(clip);
        isPlaying = true;
    }
}