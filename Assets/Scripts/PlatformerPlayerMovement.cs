using UnityEngine;

public class PlatformerPlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Vector2 _vm;

    public bool isVerticalMovementOn = false;
    [SerializeField] bool _isGrounded = false;

    [Header("Raycasting Ground")]
    public LayerMask groundLayer;
    [SerializeField] float _rayDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _vm.x = Input.GetAxisRaw("Horizontal");

        if (isVerticalMovementOn) _vm.y = Input.GetAxisRaw("Vertical");

        CheckIfPlayerIsGrounded();
        Jumping();
    }

    void CheckIfPlayerIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _rayDistance, Color.red);

        _isGrounded = hit.collider != null;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_vm * speed * Time.fixedDeltaTime);
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        { 
            _rb.AddForce(new Vector2(0, jumpForce));
            _isGrounded = false;
        }
    }
}
