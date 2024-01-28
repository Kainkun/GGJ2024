using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Padlock : MonoBehaviour
{
    public int[] passcode;
    public Interactable[] buttons;
    public TMP_Text currentInput;
    public UnityEvent onSuccess;
    public UnityEvent onFailure;
    public Color solvedColor;

    private readonly List<int> _currentCode = new List<int>();
    private bool _isSolved;

    private void Awake()
    {
        currentInput.text = string.Empty;
        
        for (int i = 0; i < buttons.Length; ++i)
        {
            int i1 = i;
            buttons[i].onInteract.AddListener(data => HandleButtonPressed(i1 + 1, data));
        }
    }

    private void HandleButtonPressed(int key, InteractEventData data)
    {
        if (_isSolved)
            return;
        
        _currentCode.Add(key);
        currentInput.text += key;
        
        if (_currentCode.Count >= passcode.Length)
        {
            bool solvedCode = true;
            
            for (int i = 0; i < _currentCode.Count; i++)
            {
                if (_currentCode[i] != passcode[i])
                {
                    solvedCode = false;
                    break;
                }
            }

            if (solvedCode)
            {
                onSuccess.Invoke();
                currentInput.color = solvedColor;
                _isSolved = true;
            }
            else
            {
                _currentCode.Clear();
                onFailure.Invoke();
                currentInput.text = string.Empty;
            }
        }
    }
}
