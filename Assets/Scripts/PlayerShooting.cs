using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Position Parameters")]
    [SerializeField] GameObject _leftShootBaseObj;
    [SerializeField] GameObject _rightShootBaseObj;

    [Header("Shooting Parameters")]
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] float _shootingDist;
    [SerializeField] float _damage;

    InputManager _inputManager;

    private void Awake()
    {
        _inputManager = InputManager.instance;
    }

    private void OnEnable()
    {
        _inputManager.inputActions.Player.Enable();
        _inputManager.Shoot += HandleShooting;
    }

    private void OnDisable()
    {
        _inputManager.inputActions.Player.Disable();
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
            Debug.DrawRay(_leftShootBaseObj.transform.position, Vector2.left * _shootingDist, Color.red);

            if (hit.collider == null) return;

            GameObject hitObj = hit.collider.gameObject;
            hitObj.GetComponent<HealthSystem>().DamageHealth(_damage);
            return;
        }

        if (_rightShootBaseObj.activeSelf)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rightShootBaseObj.transform.position, Vector2.right, _shootingDist, _enemyLayer);
            Debug.DrawRay(_rightShootBaseObj.transform.position, Vector2.right * _shootingDist, Color.red);

            if (hit.collider == null) return;

            GameObject hitObj = hit.collider.gameObject;
            hitObj.GetComponent<HealthSystem>().DamageHealth(_damage);
            return;
        }
    }


}
