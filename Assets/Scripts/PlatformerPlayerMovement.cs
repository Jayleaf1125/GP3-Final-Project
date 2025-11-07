using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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

    InputManager _inputManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _inputManager = InputManager.instance;
    }

    private void OnEnable()
    {
        _inputManager.inputActions.Player.Enable();
        _inputManager.Jump += Jumping;
    }

    private void OnDisable()
    {
        _inputManager.inputActions.Player.Disable();
        _inputManager.Jump -= Jumping;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerIsGrounded();
    }

    void CheckIfPlayerIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _rayDistance, Color.red);

        _isGrounded = hit.collider != null;
    }

    private void FixedUpdate()
    {
        Movement(); 
    }

    void Movement()
    {
        _vm = _inputManager.MoveDirection;
        _vm.y = 0f;
        _rb.linearVelocityX = _vm.x * speed;    
    }

    void Jumping()
    {
        if (_isGrounded)
        {
            _rb.linearVelocity += (Vector2.up * jumpForce);
            _isGrounded = false;
            Debug.Log("Jumping Work");
        }
    }
}
