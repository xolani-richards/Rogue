using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using MEC;
using Random = UnityEngine.Random;
// using System.Dynamic; // Uses More Effective Coroutines from the Unity Asset Store


// USE THE PLAYABLES SYSTEM
public class AnimationSystem {
    PlayableGraph playableGraph;
    readonly AnimationMixerPlayable topLevelMixer;
    readonly AnimationMixerPlayable locomotionMixer;
    
    AnimationClipPlayable oneShotPlayable;
    
    CoroutineHandle blendInHandle;
    CoroutineHandle blendOutHandle;

    public AnimationSystem(Animator animator, AnimationClip idleClip, AnimationClip walkClip, AnimationClip runClip, bool randomiseSpeed = false) {
        playableGraph = PlayableGraph.Create("AnimationSystem");
        
        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);
        
        topLevelMixer = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(topLevelMixer);
        
        locomotionMixer = AnimationMixerPlayable.Create(playableGraph, 3);
        topLevelMixer.ConnectInput(0, locomotionMixer, 0);
        playableGraph.GetRootPlayable(0).SetInputWeight(0, 1f);
        
        AnimationClipPlayable idlePlayable = AnimationClipPlayable.Create(playableGraph, idleClip);
        AnimationClipPlayable walkPlayable = AnimationClipPlayable.Create(playableGraph, walkClip);
        AnimationClipPlayable runPlayable = AnimationClipPlayable.Create(playableGraph, runClip);
        
        idlePlayable.GetAnimationClip().wrapMode = WrapMode.Loop;
        walkPlayable.GetAnimationClip().wrapMode = WrapMode.Loop;
        runPlayable.GetAnimationClip().wrapMode = WrapMode.Loop;
        if(randomiseSpeed) runPlayable.SetSpeed(Random.Range(0.9f, 1.1f));
        
        locomotionMixer.ConnectInput(0, idlePlayable, 0);
        locomotionMixer.ConnectInput(1, walkPlayable, 0);
        locomotionMixer.ConnectInput(2, runPlayable, 0);
        
        playableGraph.Play();
    }

    public void UpdateLocomotion(Vector3 velocity, float maxSpeed) 
    {
        float weight = Mathf.InverseLerp(0f, maxSpeed, velocity.magnitude);
        locomotionMixer.SetInputWeight(0, 1f - weight);
        locomotionMixer.SetInputWeight(1, weight);
        locomotionMixer.SetInputWeight(2, 0f);
    }

    public void UpdateLocomotion(float speed)
    {
        // 0 = stop | 0.5f = walk | 1f = run
        speed = Mathf.Clamp01(speed);
        if(speed < 0.5f)
        {
            float weight = Mathf.InverseLerp(0f, 0.5f, speed);
            locomotionMixer.SetInputWeight(0, 1f - weight);
            locomotionMixer.SetInputWeight(1, weight);
            locomotionMixer.SetInputWeight(2, 0f);
        }
        else
        {
            float weight = Mathf.InverseLerp(0.5f, 1f, speed);
            locomotionMixer.SetInputWeight(0, 0f);
            locomotionMixer.SetInputWeight(1, 1f - weight);
            locomotionMixer.SetInputWeight(2, weight);
        }
    }

    public void PlayOneShotAndDestroy(AnimationClip oneShotClip, float speed = 1f)
    {
        if (!oneShotPlayable.IsValid()) return;
        InterruptOneShot();
        oneShotPlayable = AnimationClipPlayable.Create(playableGraph, oneShotClip);
        oneShotPlayable.SetSpeed(speed);
        topLevelMixer.ConnectInput(1, oneShotPlayable, 0);
        topLevelMixer.SetInputWeight(1, 1f);
        float adjustedClipLength = oneShotClip.length / speed;
        float blendDuration = Mathf.Clamp(adjustedClipLength * 0.1f, 0.1f, adjustedClipLength * 0.5f);
        
        BlendIn(blendDuration);
    }

    public void PlayOneShot(AnimationClip oneShotClip, float speed = 1f, float leadTime = 0) {
        if (oneShotPlayable.IsValid() && oneShotPlayable.GetAnimationClip() == oneShotClip) return;
        
        InterruptOneShot();
        oneShotPlayable = AnimationClipPlayable.Create(playableGraph, oneShotClip);
        oneShotPlayable.SetSpeed(speed);
        oneShotPlayable.SetLeadTime(leadTime);
        topLevelMixer.ConnectInput(1, oneShotPlayable, 0);
        topLevelMixer.SetInputWeight(1, 1f);
        
        // Calculate blendDuration as 10% of clip length,
        // but ensure that it's not less than 0.1f or more than half the clip length
        float adjustedClipLength = (oneShotClip.length - leadTime) / speed;
        float blendDuration = Mathf.Clamp(adjustedClipLength * 0.1f, 0.1f, adjustedClipLength * 0.5f);
        
        BlendIn(blendDuration);
        BlendOut(blendDuration, adjustedClipLength - blendDuration);
    }

    void BlendIn(float duration) {
        blendInHandle = Timing.RunCoroutine(Blend(duration, blendTime => {
            float weight = Mathf.Lerp(1f, 0f, blendTime);
            topLevelMixer.SetInputWeight(0, weight);
            topLevelMixer.SetInputWeight(1, 1f - weight);
        }));
    }
    
    void BlendOut(float duration, float delay) {
        blendOutHandle = Timing.RunCoroutine(Blend(duration, blendTime => {
            float weight = Mathf.Lerp(0f, 1f, blendTime);
            topLevelMixer.SetInputWeight(0, weight);
            topLevelMixer.SetInputWeight(1, 1f - weight);
        }, delay, DisconnectOneShot));
    }

    IEnumerator<float> Blend(float duration, Action<float> blendCallback, float delay = 0f, Action finishedCallback = null) {
        if (delay > 0f) {
            yield return Timing.WaitForSeconds(delay);
        }
        
        float blendTime = 0f;
        while (blendTime < 1f) {
            blendTime += Time.deltaTime / duration;
            blendCallback(blendTime);
            yield return blendTime;
        }
        
        blendCallback(1f);
        
        finishedCallback?.Invoke();
    }

    void InterruptOneShot() {
        Timing.KillCoroutines(blendInHandle);
        Timing.KillCoroutines(blendOutHandle);
        
        topLevelMixer.SetInputWeight(0, 1f);
        topLevelMixer.SetInputWeight(1, 0f);

        if (oneShotPlayable.IsValid()) {
            DisconnectOneShot();
        }
    }

    void DisconnectOneShot() {
        topLevelMixer.DisconnectInput(1);
        playableGraph.DestroyPlayable(oneShotPlayable);
    }

    public void Destroy() {
        if (playableGraph.IsValid()) {
            playableGraph.Destroy();
        }
    }
}
