using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sweep_Attack : MonoBehaviour
{
    [Header("Sweep Settings")]
    public GameObject sweepPrefab;
    public float sweepDuration = 0.4f;

    [Header("Trashbag Perk")]
    public GameObject trashbagPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public int trashAmmo = 0; // This is what the TrashLogic script looks for

    private SpriteRenderer _sr;
    private Animator _anim;
    private bool _isAttacking = false;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    // --- THIS FIXES THE TRASHLOGIC ERROR ---
    public void AddAmmo(int amount)
    {
        trashAmmo += amount;
        Debug.Log("Picked up trash! Current Ammo: " + trashAmmo);
    }

    void Update()
    {
        // Left Click to Sweep
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            if (sweepPrefab != null)
                StartCoroutine(PerformForcedSweep());
        }

        // Spacebar to Shoot (Only if you have ammo)
        if (Input.GetKeyDown(KeyCode.Space) && trashAmmo > 0 && !_isAttacking)
        {
            ShootTrash();
        }
    }

    IEnumerator PerformForcedSweep()
    {
        _isAttacking = true;

        // 1. HIDE THE PLAYER
        // We disable the Animator so it stops forcing the sprite to stay visible
        if (_anim != null) _anim.enabled = false;
        if (_sr != null) _sr.enabled = false;

        // 2. SPAWN THE ATTACK
        GameObject sweep = Instantiate(sweepPrefab, transform.position, transform.rotation);
        sweep.transform.SetParent(this.transform);

        yield return new WaitForSeconds(sweepDuration);

        // 3. CLEANUP
        if (sweep != null) Destroy(sweep);

        // 4. SHOW THE PLAYER AGAIN
        if (_sr != null) _sr.enabled = true;
        if (_anim != null) _anim.enabled = true;

        _isAttacking = false;
    }

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
