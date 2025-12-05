using UnityEngine;

public class FrogController : MonoBehaviour
{
    public Animator animator;
    public new Collider2D collider;
    public new Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Die() => animator.SetBool("Dying", true);

    public void Revive(Vector2 pos)
    {
        transform.position = pos;
        rb.linearVelocity = Vector2.zero;

        animator.SetBool("Dying", false);
        collider.enabled = true;
    }

    public void OnJump()
    {
        if (Week10GameManager.Instance.CanPlayerJump())
        {
            Week10GameManager.Instance.OnPlayerJumped(this);
            animator.SetTrigger("Jump");

            rb.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
        }
    }
}
