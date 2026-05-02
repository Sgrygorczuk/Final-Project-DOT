using UnityEngine;

public class NPCConverter : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeToBecomeEnemy = 5.0f;
    public float timeToStayEnemy = 2.0f;


    [Header("Sprites")]
    public Sprite enemyLitterSprite;

    private Sprite originalNPCSprite;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isEnemy = false;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalNPCSprite = spriteRenderer.sprite;
        }

        timer = timeToBecomeEnemy;
    }

    void Update()
    {
        // Stop if we have reached the max number of loops

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!isEnemy)
            {
                TransformToEnemy();
            }
            else
            {
                TransformBackToNPC();
            }
        }
    }

    void TransformToEnemy()
    {
        spriteRenderer.sprite = enemyLitterSprite;
        isEnemy = true;
        timer = timeToStayEnemy;
        Debug.Log("Switched to Enemy_litter");
    }

    void TransformBackToNPC()
    {
        spriteRenderer.sprite = originalNPCSprite;
        isEnemy = false;
        timer = timeToBecomeEnemy; // Reset to the original wait time
    }
}