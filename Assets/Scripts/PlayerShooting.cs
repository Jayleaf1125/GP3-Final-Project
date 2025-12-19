using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Position Parameters")]
    [SerializeField] GameObject _leftShootBaseObj;
    [SerializeField] GameObject _rightShootBaseObj;

    [Header("Particles Parameters")]
    [SerializeField] Transform _rightParticleLoc;
    [SerializeField] Transform _leftParticleLoc;
    [SerializeField] ParticleSystem _shootParticle;

    [Header("Shooting Parameters")]
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] float _shootingDist;
    [SerializeField] float _damage;

    InputManager _inputManager;

    private void Awake()
    {
        //_inputManager.inputActions.Player.Enable();
        _inputManager = InputManager.instance;
        //_shootParticle.Play();
    }

    private void OnEnable()
    {
        //_inputManager.inputActions.Player.Enable();
        _inputManager.Shoot += HandleShooting;
    }

    private void OnDisable()
    {
        //_inputManager.inputActions.Player.Disable();
        _inputManager.Shoot -= HandleShooting;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _leftShootBaseObj.SetActive(false);
        _rightShootBaseObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveShootBase(_inputManager.AimDirection);
    }

    void SetActiveShootBase(Vector2 aimVec)
    {
        float aimVecXVal = aimVec.x;
        if (aimVecXVal < 0)
        {
            _leftShootBaseObj.SetActive(true);
            _rightShootBaseObj.SetActive(false);
        }
        else if (aimVecXVal > 0)
        {
            _leftShootBaseObj.SetActive(false);
            _rightShootBaseObj.SetActive(true);
        }
    }

    void HandleShooting()
    {
        if (_leftShootBaseObj.activeSelf)
        {
            RaycastHit2D hit = Physics2D.Raycast(_leftShootBaseObj.transform.position, Vector2.left, _shootingDist, _enemyLayer);
            SoundManager.instance.PlayPlayerAttackSound();
            StartCoroutine(SpawnLaserVFX(_leftParticleLoc, "left"));

            if (hit.collider == null) return;

            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.TryGetComponent<HealthSystem>(out HealthSystem hs))
            {
                hs.DamageHealth(_damage);
                return;
            }

            if (hitObj.TryGetComponent<LevelLockedBlock>(out LevelLockedBlock block))
            {
                block.SetBlockActive(true);
                return;
            }

            return;
        }

        if (_rightShootBaseObj.activeSelf)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rightShootBaseObj.transform.position, Vector2.right, _shootingDist, _enemyLayer);
            SoundManager.instance.PlayPlayerAttackSound();
            StartCoroutine(SpawnLaserVFX(_rightParticleLoc, "right"));

            if (hit.collider == null) return;

            GameObject hitObj = hit.collider.gameObject;

            if(hitObj.TryGetComponent<HealthSystem>(out HealthSystem hs))
            {
                hs.DamageHealth(_damage);
                return;
            }

            if (hitObj.TryGetComponent<LevelLockedBlock>(out LevelLockedBlock block))
            {
                block.SetBlockActive(true);
                return;
            }

            return;
        }
    }

    IEnumerator SpawnLaserVFX(Transform pos, string loc)
    {
        ParticleSystem obj; 

        if (loc == "left")
        {
            obj = Instantiate(_shootParticle, pos);
            obj.transform.Rotate(0, 180, 0);
        } else
        {
            obj = Instantiate(_shootParticle, pos);
        }

        yield return new WaitForSeconds(0.2f);
        Destroy(obj);
    }

    

    private void OnDrawGizmos()
    {
        const float SPHERE_RADIUS = 0.25f;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_rightParticleLoc.position, SPHERE_RADIUS);
        Gizmos.DrawWireSphere(_leftParticleLoc.position, SPHERE_RADIUS);
    }


}
