using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [Header("Visual Feedback")]
    public GameObject depositEffect; // Optional: Drag a "sparkle" or "poof" prefab here

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Try to get the projectile script from the object that hit the bin
        TrashbagProjectile bag = other.GetComponent<TrashbagProjectile>();

        // 2. If it IS a trashbag...
        if (bag != null)
        {
            // SUCCESS!
            Debug.Log("Bullseye! Trash in the bin.");

            // 3. Tell the bag to NOT spawn GroundTrash when it's destroyed
            bag.SetHitTarget(true);

            // 4. (Optional) Play a visual effect at the bin's position
            if (depositEffect != null)
            {
                Instantiate(depositEffect, transform.position, Quaternion.identity);
            }

            // 5. Remove the bag from the game
            Destroy(other.gameObject);

            // Note: You can add ScoreManager.instance.AddPoint() here later!
        }
    }
}