using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    public string dialogueIdToTest;

    private void Start()
    {
        DialogueSystem.Instance.RunDialogue(dialogueIdToTest);
    }
}
