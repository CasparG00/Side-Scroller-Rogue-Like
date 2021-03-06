using UnityEngine;

public class HelmetAnimation : MonoBehaviour
{
    Animator animator;
    Animator baseAnimator;
    SpriteRenderer spriteRenderer;
    SpriteRenderer baseSpriteRenderer;

    void Start() 
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        baseAnimator = transform.parent.GetComponent<Animator>();
        baseSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    void Update() 
    {
        var speed = baseAnimator.GetFloat("Speed");
        animator.SetFloat("Speed", speed);

        spriteRenderer.flipX = baseSpriteRenderer.flipX;

        var isJumping = baseAnimator.GetBool("IsJumping");
        animator.SetBool("IsJumping", isJumping);

        var verticalSpeed = baseAnimator.GetFloat("VerticalSpeed");
        animator.SetFloat("VerticalSpeed", verticalSpeed);
    }
}
