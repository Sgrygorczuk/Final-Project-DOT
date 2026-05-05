using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Sweep_Attack : MonoBehaviour
{
    [Header("Target Renderer")]
    public GameObject characterVisualObject;

    [Header("Prefab Configuration")]
    public GameObject sweepAttackPrefab;
    [Tooltip("Adjust this to align the sweep with the player's body")]
    public Vector3 spawnOffset = Vector3.zero;

    [Header("Timing")]
    public float attackVisualDuration = 0.3f;
    public float attackCooldown = 0.5f;
    private float _nextAttackTime = 0f;

    private SpriteRenderer _foundRenderer;
    private bool _isAttacking = false;

    private void Start()
    {
        UpdateRendererReference();
    }

    private void UpdateRendererReference()
    {
        if (characterVisualObject != null)
        {
            _foundRenderer = characterVisualObject.GetComponent<SpriteRenderer>();
            if (_foundRenderer == null)
                _foundRenderer = characterVisualObject.GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Update()
    {
        bool cooldownOver = Time.time >= _nextAttackTime;
        PlayerDialogue dialogueSystem = GetComponent<PlayerDialogue>();
        bool isTalking = dialogueSystem != null && dialogueSystem.IsSpeaking();

        if (Input.GetMouseButtonDown(0) && cooldownOver && !isTalking && !_isAttacking)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        _nextAttackTime = Time.time + attackCooldown;
        StartCoroutine(PerformSweepSequence());
    }

    private IEnumerator PerformSweepSequence()
    {
        _isAttacking = true;
        if (_foundRenderer == null) UpdateRendererReference();

        // 1. VANISH
        if (_foundRenderer != null) _foundRenderer.enabled = false;

        // 2. SPAWN
        GameObject spawnedAttack = Instantiate(sweepAttackPrefab, transform.position, transform.rotation);

        // 3. ALIGNMENT FIX
        spawnedAttack.transform.SetParent(this.transform);

        // Use the Offset variable to move the prefab into place
        spawnedAttack.transform.localPosition = spawnOffset;

        // Keep scale consistent
        spawnedAttack.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(attackVisualDuration);

        // 4. REAPPEAR
        if (spawnedAttack != null) Destroy(spawnedAttack);
        if (_foundRenderer != null) _foundRenderer.enabled = true;

        _isAttacking = false;
    }
}