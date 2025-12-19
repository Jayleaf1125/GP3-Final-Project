using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerPlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float sprintForce;

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
        //_inputManager.inputActions.Player.Enable();
        //SoundManager.instance.PlayBackgroundMusicSound();
        _inputManager.Jump += Jumping;
        _inputManager.Dash += Dashing;
    }

    private void OnDisable()
    {
        //_inputManager.inputActions.Player.Disable();
        _inputManager.Jump -= Jumping;
        _inputManager.Dash -= Dashing;  
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerIsGrounded();
    }

    void CheckIfPlayerIsGrounded()
    {
        const float HIT_OFFSET = 0.5f;

        RaycastHit2D midHit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, groundLayer);
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - HIT_OFFSET, transform.position.y), Vector2.down, _rayDistance, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + HIT_OFFSET, transform.position.y), Vector2.down, _rayDistance, groundLayer);

        Debug.DrawRay(transform.position, Vector2.down * _rayDistance, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - HIT_OFFSET, transform.position.y), Vector2.down * _rayDistance, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + HIT_OFFSET, transform.position.y), Vector2.down * _rayDistance, Color.red);

        _isGrounded = (leftHit.collider != null) || (midHit.collider != null) || (rightHit.collider != null);
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
            SoundManager.instance.PlayPlayerJumpSound();
            _rb.linearVelocity += (Vector2.up * jumpForce);
            _isGrounded = false;
            Debug.Log("Jumping Work");
        }
    }

    // Fix this shit, it does not work 
    void Dashing()
    {
        Debug.Log("Dashing Works");
        _rb.AddForce(Vector2.right * sprintForce, ForceMode2D.Impulse);
    }
}
