using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnId;

    void Start()
    {
        if (SpawnManager.Instance == null) return;

        if (SpawnManager.Instance.nextSpawnId == spawnId)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.transform.position = transform.position;
            }
        }
    }
}
