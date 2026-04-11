using System.Collections;
using UnityEngine;

public class PlayOnTrigger: MonoBehaviour 
{
    [SerializeField] AudioClip clip;
    [SerializeField] bool SingleShot;
    [SerializeField] bool locationSpecific;
    bool _isPlaying;
    float _duration;

    void Awake ()
    {
        _duration = clip.length;
    }

    void OnTriggerEnter (Collider collider)
    {
        if(_isPlaying) return;
        _isPlaying = true;
        AudioManager.Instance.PlayClipAtLocation (clip, transform.position);
        StartCoroutine(countdown());
    }

    IEnumerator countdown ()
    {
        yield return new WaitForSeconds (_duration);
        if(!SingleShot) _isPlaying = false;
    }
}