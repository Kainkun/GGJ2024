using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    [SerializeField] 
    private string id;
    
    public void StartDialogue()
    {
        DialogueSystem.Instance.RunDialogue(id);
    }
    
    public void StartDialogueRepeating()
    {
        DialogueSystem.Instance.RunDialogueRepeating(id);
    }

    public void StopDialogue()
    {
        DialogueSystem.Instance.StopDialogue(id);
    }
}
