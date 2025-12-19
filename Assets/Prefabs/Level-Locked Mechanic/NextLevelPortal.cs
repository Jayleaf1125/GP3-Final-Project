using UnityEngine;

public class NextLevelPortal : MonoBehaviour
{
    [SerializeField] GameObject[] _levelLockedBlocks = new GameObject[4];
    SpriteRenderer _sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePortalOn();
    }

    void HandlePortalOn()
    {
        foreach (var block in _levelLockedBlocks)
        {
            if (!block.GetComponent<LevelLockedBlock>().GetBlockIsActive()) return;
        }

        _sr.color = Color.cyan;

    }
}
