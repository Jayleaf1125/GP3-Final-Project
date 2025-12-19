using UnityEngine;
using System.Collections;

enum EnemyState
{
    Patrol,
    Attack
};

public enum StartDirection
{
    Left,
    Right
};

public class EnemySystem : MonoBehaviour
{
    SpriteRenderer _sr;
    Vector2 _vm = Vector2.right;
    public LayerMask groundLayer;

    // Ground Checks
    bool _isOnLeft = false;
    bool _isOnRight = false;
    public float HIT_OFFSET;
    public float Speed;

    EnemyState _currState = EnemyState.Patrol;
    public StartDirection _currDirection;
    Color _originalColor;

    bool _isPlayerInChaseRange = false;
    //bool _isPlayerInAttackRange = false;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;

    [Header("State Settings")]
    public float attackRangeDist;
    //public float attackRangeDist;

    public ParticleSystem ShootParticle;
    [SerializeField] float _enemyDamage;

    public GameObject playerObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _originalColor = _sr.color;

        _vm = _currDirection == StartDirection.Left ? Vector2.left : Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyGrounded();
        CheckIfPlayerInChaseRange();
        SetDirection();
        SetState();
        playerObj = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        switch(_currState)
        {
            case EnemyState.Patrol:
                Patrolling();
                break;
            case EnemyState.Attack:
                Attacking();
                break;
        }
    }

    void CheckIfEnemyGrounded()
    {
        //const float HIT_OFFSET = 1f;
        const float RAY_DIST = 1.5f;

        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x - HIT_OFFSET, transform.position.y), Vector2.down, RAY_DIST, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(transform.position.x + HIT_OFFSET, transform.position.y), Vector2.down, RAY_DIST, groundLayer);

        Debug.DrawRay(new Vector2(transform.position.x - HIT_OFFSET, transform.position.y), Vector2.down * RAY_DIST, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + HIT_OFFSET, transform.position.y), Vector2.down * RAY_DIST, Color.red);

        _isOnLeft = leftHit.collider != null;
        _isOnRight = rightHit.collider != null;
    }
    void SetDirection()
    {
        if (!_isOnLeft)
        {
            _vm = Vector2.right;
            return; 
        }

        if (!_isOnRight)
        {
            _vm = Vector2.left;
            return;
        }
    }

    void Patrolling()
    {
        //Speed /= ChaseSpeedMultipler;
        transform.Translate(_vm * Speed * Time.fixedDeltaTime);
    }

    void CheckIfPlayerInChaseRange() => _isPlayerInChaseRange = Physics2D.OverlapCircle(transform.position, attackRangeDist, playerLayer);
    //void CheckIfPlayerInAttackRange() => _isPlayerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRangeDist, playerLayer);

    IEnumerator DamageOvertime()
    {
        playerObj.GetComponent<HealthSystem>().DamageHealth(_enemyDamage);
        yield return new WaitForSeconds(1f);
    }


    void SetState()
    {
        if (_isPlayerInChaseRange)
        {
            _sr.color = Color.yellow;
            _currState = EnemyState.Attack;
        } else
        {
            _sr.color = _originalColor;
            _currState = EnemyState.Patrol;
        }
    }

    void Attacking()
    {
        StartCoroutine(SpawnLaserVFX(transform.transform));
        StartCoroutine(DamageOvertime());
    }

    IEnumerator SpawnLaserVFX(Transform pos)
    {
        ParticleSystem obj = Instantiate(ShootParticle, pos);
        yield return new WaitForSeconds(1.1f);
        Destroy(obj);
    }

    private void OnDrawGizmos()
    {
        // Attack Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRangeDist);
    }
}
