using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    public string animation1 = "Animation1";
    public string animation2 = "Animation2";
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PlayAnimations());
    }
    IEnumerator PlayAnimations()
    {
        while (true)
        {
            // Choose a random animation with specified probabilities
            string animationName = Random.value < 0.1 ? animation1 : animation2;

            // Set a random speed for the animation
            animator.speed = Random.Range(0.2f, 0.8f);

            // Crossfade to the new animation over 0.5 seconds
            animator.CrossFadeInFixedTime(animationName, 0.5f);

            // Wait for the animation to finish before choosing the next one
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / animator.speed);
        }
    }
}