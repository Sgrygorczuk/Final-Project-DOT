using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading new scenes

public class PlayerDialogue : MonoBehaviour
{
    public List<DialogueData> dialogue = new List<DialogueData>();
    private bool canSpeak = false;
    private bool isSpeaking = false;
    private GameObject _talkPanel;
    private TextMeshProUGUI _talkText;
    private int _talkIndex = 0;

    [Header("Scene Settings")]
    public string nextSceneName; // Set this in the Unity Inspector

    private AudioSource _audioSource;

    private void Start()
    {
        _talkText = GameObject.Find(Structs.GameObjects.talkText).GetComponent<TextMeshProUGUI>();
        _talkPanel = GameObject.Find(Structs.GameObjects.talkPanel);
        _talkPanel.SetActive(false);

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

                // --- Scene Transition Logic ---
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            }
            else
            {
                _talkIndex++;
                _talkText.text = dialogue[_talkIndex].text;
                PlayCurrentAudio();
            }
        }
        else if (canSpeak && Input.GetKeyDown(KeyCode.E))
        {
            isSpeaking = true;
            _talkPanel.SetActive(true);
            _talkIndex = 0;
            _talkText.text = dialogue[_talkIndex].text;
            PlayCurrentAudio();
        }
    }

    private void PlayCurrentAudio()
    {
        if (dialogue[_talkIndex].soundEffect != null)
        {
            _audioSource.PlayOneShot(dialogue[_talkIndex].soundEffect);
        }
    }

    public void SetCanSpeak(bool newCanSpeak) { canSpeak = newCanSpeak; }
    public bool IsSpeaking() { return isSpeaking; }

    public void CopyDialogue(List<DialogueData> newDialogue)
    {
        dialogue.Clear();
        dialogue.AddRange(newDialogue);
    }
}