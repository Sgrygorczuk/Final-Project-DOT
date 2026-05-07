using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Sweep_Attack : MonoBehaviour
{
    [Header("Visual Switchbox")]
    public GameObject idleVisuals;
    public GameObject sweepVisuals;

    [Header("Trashbag Perk")]
    public GameObject trashbagPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public int trashAmmo = 0;

    [Header("Timing")]
    public float sweepDuration = 0.4f;

    private bool _isAttacking = false;

    void Start()
    {
        // Safety: If these aren't dragged in, the script warns you immediately
        if (idleVisuals == null || sweepVisuals == null)
        {
            Debug.LogError("<color=red>Missing Visuals!</color> Drag your child objects into the slots on " + gameObject.name);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            // Only start if we have the objects assigned
            if (idleVisuals != null && sweepVisuals != null)
                StartCoroutine(PerformHardSwapSweep());
        }

        if (Input.GetKeyDown(KeyCode.Space) && trashAmmo > 0 && !_isAttacking)
        {
            ShootTrash();
        }
    }

    IEnumerator PerformHardSwapSweep()
    {
        _isAttacking = true;

        // SWAP ON
        idleVisuals.SetActive(false);
        sweepVisuals.SetActive(true);

        yield return new WaitForSeconds(sweepDuration);

        // SWAP OFF
        sweepVisuals.SetActive(false);
        idleVisuals.SetActive(true);

        _isAttacking = false;
    }

    public void AddAmmo(int amount) => trashAmmo += amount;

    void ShootTrash()
    {
        if (trashbagPrefab == null || firePoint == null) return;

        trashAmmo--;
        GameObject bullet = Instantiate(trashbagPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = new Vector2(direction * bulletSpeed, 0);
        }
    }
}