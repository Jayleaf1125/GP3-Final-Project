using UnityEngine;

public class SwapRespawnPoint : MonoBehaviour
{
    [SerializeField] Transform _newRespawnPoint;
    [SerializeField] float _rayDistance;
    [SerializeField] LayerMask _playerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerPassedCheckPoint();
    }

    void CheckIfPlayerPassedCheckPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _playerLayer);
        Debug.DrawRay(transform.position, Vector2.down * _rayDistance, Color.red);

        Collider2D player = hit.collider;

        if (player != null) player.GetComponent<DeathPosition>().SetLastRespawnPoint(_newRespawnPoint);
    }
}
