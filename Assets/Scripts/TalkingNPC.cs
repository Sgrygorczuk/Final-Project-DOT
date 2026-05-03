using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : MonoBehaviour
{
    // Updated: Changed from <string> to <DialogueData>
    public List<DialogueData> dialogue = new List<DialogueData>();
    private GameObject _talkIcon;

    private void Start()
    {
        _talkIcon = transform.Find(Structs.GameObjects.talkIcon).gameObject;
        _talkIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Structs.Tags.playerTag)
        {
            _talkIcon.SetActive(true);

            // This now sends the "Pack" (Text + Sound) to the player
            collision.GetComponent<PlayerDialogue>().CopyDialogue(dialogue);
            collision.GetComponent<PlayerDialogue>().SetCanSpeak(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Structs.Tags.playerTag)
        {
            _talkIcon.SetActive(false);
            collision.GetComponent<PlayerDialogue>().SetCanSpeak(false);
        }
    }
}