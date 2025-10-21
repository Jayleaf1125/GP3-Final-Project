using System.Collections.Generic;
using UnityEngine;

public class DeathPosition : MonoBehaviour
{
    Vector3 PosSave;
    public GameObject SaveLocationPrefab;

    [SerializeField] Transform _lastRespawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            SavePos();
        }
        
    }

    void SavePos()
    {
        PosSave = new Vector2(transform.position.x, transform.position.y - 1);
        Instantiate(SaveLocationPrefab, PosSave, Quaternion.identity);

        this.transform.position = _lastRespawnPoint.position;
    }

    public void SetLastRespawnPoint(Transform newRespawnPoint) => _lastRespawnPoint = newRespawnPoint;
    public void ResetPlayerPos() => this.transform.position = _lastRespawnPoint.position;

}
