using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashBin : MonoBehaviour
{
    [Header("Detection Settings")]
    public string trashTag = "Trash"; // Optional: Use tags for extra safety

    [Header("Effects")]
    public GameObject depositParticle; // Assign a particle prefab for a 'pop' effect
    public AudioSource depositSound;   // Assign an AudioSource for a 'ding' sound

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Try to find the TrashbagProjectile script on the object that entered
        TrashbagProjectile bag = other.GetComponent<TrashbagProjectile>();

        if (bag != null)
        {
            // 2. Trigger the "Success" logic in the bag
            // This sends the bin's position to the bag so it can zip to the center
            bag.SetHitTarget(this.transform);

            // 3. Play visual and audio feedback
            PlayEffects();

            Debug.Log("Trash successfully deposited!");
        }
    }

    private void PlayEffects()
    {
        if (depositSound != null) depositSound.Play();

        if (depositParticle != null)
        {
            Instantiate(depositParticle, transform.position, Quaternion.identity);
        }
    }
}