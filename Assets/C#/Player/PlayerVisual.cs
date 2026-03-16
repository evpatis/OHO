using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    

    private void Awake()
    {
        if (playerRb == null)
            playerRb = GetComponentInParent<Rigidbody2D>();


        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (playerRb == null) return;

        Vector2 velocity = playerRb.linearVelocity;

        
        if (animator != null)
        {
            animator.SetBool("IsRunning", velocity.magnitude > 0.1f);
        }

        
        if (spriteRenderer != null)
        {
            if (velocity.x > 0.1f)
                spriteRenderer.flipX = false;
            else if (velocity.x < -0.1f)
                spriteRenderer.flipX = true;
        }
    }
}
