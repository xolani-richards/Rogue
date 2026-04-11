// using System.Collections;
// using System.Collections.Generic;
// using ONI.Events;
// using UnityEngine;

// public class PlayOnEvent: MonoBehaviour, IObserver
// {
//     [SerializeField] List<AudioClip> clips = new ();
//     [SerializeField] bool SingleShot;
//     [SerializeField] bool locationSpecific;
//     [SerializeField] EventTypes trigger;
//     bool _isPlaying;
//     float _duration;

//     void Start ()
//     {
//         EventBus.Instance.Register(trigger, this);
//     }

//     void OnDestroy ()
//     {
//         EventBus.Instance.Unregister(trigger, this);
//     }

//     void OnTrigger ()
//     {
//         if(_isPlaying) return;
//         _isPlaying = true;
//         AudioClip clip = clips[Random.Range(0, clips.Count)];
//         if(locationSpecific) AudioManager.Instance.PlayClipAtLocation (clip, transform.position);
//         else AudioManager.Instance.PlayClip (clip);
//         StartCoroutine(countdown());
//     }

//     IEnumerator countdown ()
//     {
//         yield return new WaitForSeconds (_duration);
//         if(!SingleShot) _isPlaying = false;
//     }

//     public void OnNotify(EventTypes eventType, object data)
//     {
//         if(eventType == trigger) OnTrigger();
//     }
// }