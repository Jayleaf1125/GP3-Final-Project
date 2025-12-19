using System.Collections.Generic;
using UnityEngine;

public class DeathPosition : MonoBehaviour
{
    Vector3 PosSave;
    public GameObject SaveLocationPrefab;
    [SerializeField] Transform _lastRespawnPoint;
    InputManager _inputManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _inputManager = InputManager.instance;
    }

    private void OnEnable()
    {
        //_inputManager.inputActions.Player.Enable();
        _inputManager.Sacrfirce += SavePos;
    }

    private void OnDisable()
    {
        //_inputManager.inputActions.Player.Disable();
        _inputManager.Sacrfirce -= SavePos;
    }

    void SavePos()
    {
        SoundManager.instance.PlayPlayerSacfriceSound();
        PosSave = new Vector2(transform.position.x, transform.position.y - 1);
        Instantiate(SaveLocationPrefab, PosSave, Quaternion.identity);

        this.transform.position = _lastRespawnPoint.position;
    }

    public void SetLastRespawnPoint(Transform newRespawnPoint) => _lastRespawnPoint = newRespawnPoint;
    public void ResetPlayerPos() => this.transform.position = _lastRespawnPoint.position;

}
