using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Rendering;

enum EnemyState
{
    Patrol,
    Attack
};

public class EnemySystem : MonoBehaviour
{
    SpriteRenderer _sr;
    Rigidbody2D _rb;
    Vector2 _vm = Vector2.right;
    public LayerMask groundLayer;

    // Ground Checks
    bool _isOnLeft = false;
    bool _isOnRight = false;
    public float HIT_OFFSET;
    public float Speed;

    EnemyState _currState = EnemyState.Patrol;
    Color _originalColor;

    bool _isPlayerInChaseRange = false;
    //bool _isPlayerInAttackRange = false;
    public LayerMask playerLayer;

    [Header("State Settings")]
    public float chaseRangeDist;
    //public float attackRangeDist;

    public ParticleSystem ShootParticle;
    public float ChaseSpeedMultipler;
    bool _alreadyBoosted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _originalColor = _sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyGrounded();
        CheckIfPlayerInChaseRange();
        SetDirection();
        SetState();
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

    void CheckIfPlayerInChaseRange() => _isPlayerInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRangeDist, playerLayer);
    //void CheckIfPlayerInAttackRange() => _isPlayerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRangeDist, playerLayer);


    void SetState()
    {
        if (_isPlayerInChaseRange)
        {
            _sr.color = Color.yellow;
            _alreadyBoosted = false;
            _currState = EnemyState.Attack;
            } else
        {
            _sr.color = _originalColor;
            _alreadyBoosted = false;
            _currState = EnemyState.Patrol;
        }
    }

    void Attacking()
    {
        StartCoroutine(SpawnLaserVFX(transform.transform));
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
        Gizmos.DrawWireSphere(transform.position, chaseRangeDist);
    }
}
