using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField] 
    private Dialogue[] registeredDialogue;

    public event Action<DialogueEventData> OnRunDialogue;
    private readonly List<ActiveDialogue> _activeDialogue = new List<ActiveDialogue>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StopDialogue(string id)
    {
        foreach (ActiveDialogue activeDialogue in _activeDialogue)
        {
            if (activeDialogue.Dialogue.id == id)
            {
                StopCoroutine(activeDialogue.Coroutine);
                _activeDialogue.Remove(activeDialogue);
                return;
            }
        }
    }

    public void RunDialogueRepeating(string id)
    {
        Dialogue dialogue = GetDialogue(id);

        ActiveDialogue activeDialogue = new ActiveDialogue {
            Coroutine = StartCoroutine(RunDialogueCoroutine(dialogue, true)),
            Dialogue = dialogue,
        };
        
        _activeDialogue.Add(activeDialogue);
    }
    
    public Coroutine RunDialogue(string id)
    {
        Dialogue dialogue = GetDialogue(id);

        ActiveDialogue activeDialogue = new ActiveDialogue {
            Coroutine = StartCoroutine(RunDialogueCoroutine(dialogue, false)),
            Dialogue = dialogue,
        };
        
        _activeDialogue.Add(activeDialogue);
        return activeDialogue.Coroutine;
    }

    private IEnumerator RunDialogueCoroutine(Dialogue dialogue, bool repeating)
    {
        print($"[DIALOGUE SYSTEM] Playing dialogue with id \"{dialogue.id}\".");

        do
        {
            foreach (Dialogue.Line dialogueLine in dialogue.lines)
            {
                if (!dialogueLine.isPause)
                {
                    if (!dialogueLine.speaker.TryFindCharacter(out GameObject speaker))
                        throw new Exception($"[DIALOGUE SYSTEM] Tried to run dialogue for {dialogue.id}, but was missing speaker!");

                    dialogueLine.audioEvent.Post(speaker);

                    DialogueEventData eventData = new DialogueEventData {
                        Duration = dialogueLine.duration,
                        Speaker = speaker,
                    };

                    OnRunDialogue?.Invoke(eventData);
                }
                yield return new WaitForSeconds(dialogueLine.duration);
            }
        } while (repeating);
    }

    private Dialogue GetDialogue(string id)
    {
        foreach (Dialogue dialogue in registeredDialogue)
        {
            if (dialogue.id == id)
                return dialogue;
        }

        throw new Exception($"[DIALOGUE SYSTEM] No dialogue with id \"{id}\" was found!");
    }

    private struct ActiveDialogue
    {
        public Coroutine Coroutine;
        public Dialogue Dialogue;
    }
}
