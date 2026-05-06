using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrashSpawn : MonoBehaviour
{
    [Header("Trash Assets")]
    public GameObject[] trashPrefabs;
    public Transform spawnPoint;
    [Header("Timing")]
    [Tooltip("How often the NPC tries to throw (The Heartbeat)")]
    public float spawnInterval = 3f;

    [Tooltip("Wait time after a 'Burst' of throwing")]
    public float cooldownDuration = 5f;

    private float _nextCanSpawnTime = 0f;
    private bool _isOnCooldown = false;

    void Start()
    {
        // Repeating the attempt to spawn based on the interval
        InvokeRepeating("AttemptSpawn", 2f, spawnInterval);
    }

    void AttemptSpawn()
    {
        // 1. Check if we are currently in a cooldown period
        if (Time.time < _nextCanSpawnTime) return;

        if (trashPrefabs.Length == 0) return;

        // 2. Spawn the trash
        int index = Random.Range(0, trashPrefabs.Length);
        Instantiate(trashPrefabs[index], spawnPoint.position, Quaternion.identity);

        // 3. Optional: Set a cooldown after a throw
        // This makes the NPC wait 'cooldownDuration' before the NEXT interval works
        _nextCanSpawnTime = Time.time + cooldownDuration;

        Debug.Log("NPC Threw trash! Now resting...");
    }
}