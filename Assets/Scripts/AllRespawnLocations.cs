using System.Collections.Generic;
using UnityEngine;

public class AllRespawnLocations : MonoBehaviour
{
    [SerializeField] List<Transform> _respawnList = new();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (Transform respawn in _respawnList)
        {
            Gizmos.DrawWireSphere(respawn.position, 0.5f);
        }
    }
}
