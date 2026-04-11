using System;
using UnityEngine;
using ROGUE.Characters;

public class AnimationController: MonoBehaviour
{
    public AnimationSystem animationSystem;
    public Animator animator;
    Character character;
    public Action<string> onAnimEvent;
    void Awake()
    {
        animator = GetComponent<Animator>();
        character = GetComponentInParent<Character>();
    }

    public void AnimEvent(string eventName) => onAnimEvent?.Invoke(eventName);
    

    void OnAnimatorMove()
    {
        if(!character.useRootMotion) return;
        character.transform.position += animator.deltaPosition;
        character.transform.rotation *= animator.deltaRotation;
    }

    void FixedUpdate()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}