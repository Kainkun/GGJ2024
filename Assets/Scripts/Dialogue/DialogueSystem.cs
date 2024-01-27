using System;
using System.Collections;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField] 
    private Dialogue[] registeredDialogue;

    [SerializeField] 
    private SubtitleRunner subtitleRunner;
    
    public event Action<DialogueEventData> OnRunDialogue;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public Coroutine RunDialogue(string id)
    {
        Dialogue dialogue = GetDialogue(id);

        ActiveDialogue activeDialogue = new ActiveDialogue {
            Coroutine = StartCoroutine(RunDialogueCoroutine(dialogue)),
            Dialogue = dialogue,
        };
        
        return activeDialogue.Coroutine;
    }

    private IEnumerator RunDialogueCoroutine(Dialogue dialogue)
    {
        print($"[DIALOGUE SYSTEM] Playing dialogue with id \"{dialogue.id}\".");

        foreach (Dialogue.Line dialogueLine in dialogue.lines)
        {
            if (!dialogueLine.isPause)
            {
                if (!dialogueLine.speaker.TryFindCharacter(out GameObject speaker))
                    throw new Exception($"[DIALOGUE SYSTEM] Tried to run dialogue for {dialogue.id}, but was missing speaker!");
            
                dialogueLine.audioEvent.Post(speaker);
                subtitleRunner.DisplaySubtitle(dialogueLine.subtitle, dialogueLine.duration);
            
                DialogueEventData eventData = new DialogueEventData {
                    Duration = dialogueLine.duration,
                    Speaker = speaker,
                };
            
                OnRunDialogue?.Invoke(eventData);
            }
            yield return new WaitForSeconds(dialogueLine.duration);
        }
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
