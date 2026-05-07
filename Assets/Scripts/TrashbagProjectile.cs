using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashbagProjectile : MonoBehaviour
{
    public float lifeTime = 3f;
    public GameObject groundTrashPrefab;
    private bool _hasHitTarget = false;

    private void Start()
    {
        Invoke("ConvertToTrash", lifeTime);
    }

    // 1. THIS MUST BE PUBLIC SO THE BIN CAN SEE IT
    public void SetHitTarget(bool state)
    {
        _hasHitTarget = state;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            ConvertToTrash();
        }
    }

    void ConvertToTrash()
    {
        if (!_hasHitTarget)
        {
            if (groundTrashPrefab != null)
            {
                Instantiate(groundTrashPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}