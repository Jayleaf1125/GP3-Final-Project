 using UnityEngine;

public class DeathLayerLaser : MonoBehaviour
{
    [SerializeField] float _rayDistance;
    [SerializeField] LayerMask _playerLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerPassedFallBoundary();
    }

    void CheckIfPlayerPassedFallBoundary()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _rayDistance, _playerLayer);
        Debug.DrawRay(transform.position, Vector2.right * _rayDistance, Color.blue);

        Collider2D player = hit.collider;

        if (player != null)
        {
            SoundManager.instance.PlayPlayerDeathSound();
            player.GetComponent<DeathPosition>().ResetPlayerPos();
        }
    }
}
