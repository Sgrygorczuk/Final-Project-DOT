using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrashLogic : MonoBehaviour
{
    [Header("Detection Settings")]
    public float sweepRange = 3.5f;

    private Transform _playerTransform;
    private bool _isSwept = false;

    void Start()
    {
        // Find the player automatically
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (_playerTransform == null || _isSwept) return;

        // 1. Check distance to player
        float distance = Vector2.Distance(transform.position, _playerTransform.position);

        // 2. Detect Sweep (Left Click)
        if (distance <= sweepRange && Input.GetMouseButtonDown(0))
        {
            _isSwept = true; // Prevents double-collection
            SweepAndCollect();
        }
    }

    void SweepAndCollect()
    {
        // 3. Find the Sweep script on the player to add ammo
        Player_Sweep_Attack playerScript = _playerTransform.GetComponent<Player_Sweep_Attack>();

        if (playerScript != null)
        {
            playerScript.AddAmmo(1); // Calls the function we added to your player script
        }

        // 4. Visual cleanup
        // Disable collider so player doesn't bump into "ghost" trash
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Destroy the GroundTrash object after a tiny delay
        Destroy(gameObject, 0.1f);
    }
}