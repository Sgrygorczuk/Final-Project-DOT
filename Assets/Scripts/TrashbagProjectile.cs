using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrashbagProjectile : MonoBehaviour

{

    [Header("Projectile Settings")]

    public GameObject trashPrefab;

    public float shootForce = 15f;

    public float spawnOffset = 0.7f;



    [Header("References")]

    public Camera mainCamera;

    public Transform bulletTrash;



    void Update()

    {

        if (Input.GetMouseButtonDown(1))

        {

            Shoot();

        }

    }



    void Shoot()

    {

        // 1. Safety Checks

        if (trashPrefab == null)

        {

            Debug.LogError("Assign the Trash Prefab in the Inspector!");

            return;

        }

        if (mainCamera == null)

        {

            mainCamera = Camera.main; // Auto-assign if forgotten

        }



        // 2. Calculate Direction

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0f;

        Vector2 dir = ((Vector2)mousePos - (Vector2)transform.position).normalized;



        // 3. Set Spawn Position

        Vector3 spawnPos = transform.position + (Vector3)dir * spawnOffset;



        // 4. CREATE THE CLONE

        // We store the clone in a variable called 'newBag'

        GameObject newBag = Instantiate(trashPrefab, spawnPos, Quaternion.identity);





        // 6. APPLY PHYSICS TO THE CLONE

        Rigidbody2D rb = newBag.GetComponent<Rigidbody2D>();

        if (rb != null)

        {

            rb.velocity = Vector2.zero;

            rb.AddForce(dir * shootForce, ForceMode2D.Impulse);

        }

    }



    public void SetHitTarget(Transform target)

    {

        // Keeps TrashBin script from breaking

        Debug.Log("Trashbag hit target: " + target.name);

    }

}