using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimClip
{
    public AnimationClip clip;
    public string name;
}
public class AnimationManager : MonoBehaviour
{
    [SerializeField] private List<AnimClip> animationClips;
    [SerializeField] Animator animator;

    public void PlayAnimation(string name)
    {
        AnimationClip clip = GetAnimationClip(name);
        if (!clip)
        {
            Debug.Log(name + "animation can't be found for " + this.gameObject.name);
            return;
        }

        animator.Play(clip.name);
    }

    AnimationClip GetAnimationClip(string name)
    {
        foreach (AnimClip animClip in animationClips)
        {
            if (animClip.name == name)
            {
                return animClip.clip;
            }
        }

        return null;
    }
}
