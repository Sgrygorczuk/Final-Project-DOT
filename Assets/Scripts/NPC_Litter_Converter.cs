using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPC_Litter_Converter : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeToBecomeLitterer = 5.0f; // Time spent in Idle
    public float timeInLitterPose = 2.0f;    // Time spent in Litter Sprite
    public float trashDropCooldown = 1.0f;   // Delay before the next drop loop starts

    [Header("Sprites")]
    public Sprite litterSprite;

    [Header("Trash Logic")]
    public GameObject groundTrashPrefab;
    public float dropRandomRange = 1.5f;

    private Sprite originalNPCSprite;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isLittering = false;

    void Start()
    {
        // Reach into the children to find the SpriteRenderer
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalNPCSprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogError("NPC_Litter_Converter: No SpriteRenderer found in children of " + gameObject.name);
        }

        timer = timeToBecomeLitterer;
    }

    void Update()
    {
        if (spriteRenderer == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!isLittering)
            {
                StartLittering();
            }
            else
            {
                StopLittering();
            }
        }
    }

    void StartLittering()
    {
        spriteRenderer.sprite = litterSprite;
        isLittering = true;
        timer = timeInLitterPose;

        DropTrashRandomly();
    }

    void StopLittering()
    {
        spriteRenderer.sprite = originalNPCSprite;
        isLittering = false;

        // The "Cooldown" is essentially how long they wait before becoming an enemy again
        timer = timeToBecomeLitterer + trashDropCooldown;
    }

    void DropTrashRandomly()
    {
        if (groundTrashPrefab != null)
        {
            float randomX = Random.Range(-dropRandomRange, dropRandomRange);
            float randomY = Random.Range(-dropRandomRange, dropRandomRange);
            Vector3 spawnPos = transform.position + new Vector3(randomX, randomY, 0);

            Instantiate(groundTrashPrefab, spawnPos, Quaternion.identity);
        }
    }
}