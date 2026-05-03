using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    // Updated: Now using the custom DialogueData class instead of a simple string
    public List<DialogueData> dialogue = new List<DialogueData>();
    private bool canSpeak = false;
    private bool isSpeaking = false;
    private GameObject _talkPanel;
    private TextMeshProUGUI _talkText;
    private int _talkIndex = 0;

    // New: Component to handle playing the audio
    private AudioSource _audioSource;

    private void Start()
    {
        _talkText = GameObject.Find(Structs.GameObjects.talkText).GetComponent<TextMeshProUGUI>();

        _talkPanel = GameObject.Find(Structs.GameObjects.talkPanel);
        _talkPanel.SetActive(false);

        // New: Get or Add an AudioSource component
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isSpeaking && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogue.Count - 1 == _talkIndex)
            {
                isSpeaking = false;
                _talkPanel.SetActive(false);
            }
            else
            {
                _talkIndex++;
                // Updated: Access .text and play the associated sound
                _talkText.text = dialogue[_talkIndex].text;
                PlayCurrentAudio();
            }
        }
        else if (canSpeak && Input.GetKeyDown(KeyCode.E))
        {
            isSpeaking = true;
            _talkPanel.SetActive(true);
            _talkIndex = 0;
            // Updated: Access .text and play the associated sound
            _talkText.text = dialogue[_talkIndex].text;
            PlayCurrentAudio();
        }
    }

    // New: Helper method to play the audio clip assigned to the current dialogue index
    private void PlayCurrentAudio()
    {
        if (dialogue[_talkIndex].soundEffect != null)
        {
            _audioSource.PlayOneShot(dialogue[_talkIndex].soundEffect);
        }
    }

    public void SetCanSpeak(bool newCanSpeak)
    {
        canSpeak = newCanSpeak;
    }

    public bool IsSpeaking()
    {
        return isSpeaking;
    }

    // Updated: Changed parameter type to List<DialogueData>
    public void CopyDialogue(List<DialogueData> newDialogue)
    {
        dialogue.Clear();
        dialogue.AddRange(newDialogue);
    }
}
