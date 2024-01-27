using System;
using System.Collections;
using UnityEngine;

public struct DialogueEventData
{
    public GameObject Speaker;
    public float Duration;
}

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public string id;
    public Line[] lines;

    [Serializable]
    public struct Line
    {
        public string speakerName;
        public float duration;
        public AK.Wwise.Event audioEvent;
    }
}

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField] 
    private Dialogue[] registeredDialogue;

    public event Action<DialogueEventData> OnRunDialogue;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RunDialogue(string id)
    {
        StartCoroutine(RunDialogueCoroutine(id));
    }

    public IEnumerator RunDialogueCoroutine(string id)
    {
        print($"[DIALOGUE SYSTEM] Playing dialogue with id \"{id}\".");
        Dialogue dialogue = GetDialogue(id);

        foreach (Dialogue.Line dialogueLine in dialogue.lines)
        {
            GameObject targetObject = GameObject.Find(dialogueLine.speakerName);
            dialogueLine.audioEvent.Post(targetObject);
            
            DialogueEventData eventData = new DialogueEventData {
                Duration = dialogueLine.duration,
                Speaker = targetObject,
            };
            
            OnRunDialogue?.Invoke(eventData);
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
}
