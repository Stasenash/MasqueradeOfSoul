using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnId;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SpawnPoint " + spawnId + 
          " waiting for " + SpawnManager.Instance.nextSpawnId);
        if (SpawnManager.Instance == null) return;
        if (SpawnManager.Instance.nextSpawnId != spawnId) return;

        Player player = FindObjectOfType<Player>();
        if (player == null) return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        // 1. Сбрасываем физику
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // 2. Меняем ТОЛЬКО X
        Vector3 pos = player.transform.position;
        pos.x = transform.position.x;
        player.transform.position = pos;
    }
}
