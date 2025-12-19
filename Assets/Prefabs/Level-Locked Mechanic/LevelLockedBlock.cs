using Unity.VisualScripting;
using UnityEngine;

public class LevelLockedBlock : MonoBehaviour
{
    [SerializeField] Color _blockInactiveColor = Color.red;
    [SerializeField] Color _blockActiveColor = Color.red;
    bool _isBlockOn;
    SpriteRenderer _sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isBlockOn = false;
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = _isBlockOn ? _blockActiveColor : _blockInactiveColor;
    }

    public void SetBlockActive(bool val)
    {
        _isBlockOn = val;
        _sr.color = _isBlockOn ? _blockActiveColor : _blockInactiveColor;
    }

    public bool GetBlockIsActive() => _isBlockOn;
}
