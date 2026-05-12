using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Your existing movement logic
        if (_rigidbody2D.velocity.x != 0 || _rigidbody2D.velocity.y != 0)
        {
            _animator.SetBool(Structs.AnimationParameters.isWalking, true);
        }
        else
        {
            _animator.SetBool(Structs.AnimationParameters.isWalking, false);
        }
    }

    /// <summary>
    /// Call this when depositing trash to avoid the MissingComponentException
    /// </summary>
    public void PlayDepositAnimation(GameObject bin)
    {
        if (bin != null)
        {
            // Check the bin for an animator
            Animator binAnim = bin.GetComponent<Animator>();

            // ONLY try to set the bool if the animator actually exists
            if (binAnim != null)
            {
                // If your bin has a parameter name, put it in quotes here
                // binAnim.SetBool("IsOpen", true); 
            }
        }

        // Trigger the player's throw animation if they have one
        if (_animator != null)
        {
            // _animator.SetTrigger("Throw"); 
        }
    }
}
