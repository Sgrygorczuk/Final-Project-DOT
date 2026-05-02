using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFixedRotation : MonoBehaviour
{
    public Transform NPC;
    Quaternion FixedRotation;
    // Start is called before the first frame update
    void Start()
    {
        FixedRotation = NPC.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        NPC.rotation = FixedRotation;
    }
}
