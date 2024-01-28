using System.Collections;
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
    public Color failColor;

    private readonly List<int> _currentCode = new List<int>();
    private bool _isSolved;
    private bool _isLocked;

    private void Awake()
    {
        currentInput.text = string.Empty;
        
        for (int i = 0; i < buttons.Length; ++i)
        {
            int i1 = i;
            buttons[i].onInteract.AddListener(data => HandleButtonPressed(i1 + 1, data));
        }
    }

    private IEnumerator FailCoroutine()
    {
        _isLocked = true;
        Color normalColor = currentInput.color;
        currentInput.color = failColor;
        yield return new WaitForSeconds(1);
        currentInput.color = normalColor;
        _currentCode.Clear();
        currentInput.text = string.Empty;
        _isLocked = false;
    }

    private void HandleButtonPressed(int key, InteractEventData data)
    {
        if (_isSolved || _isLocked)
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
                onFailure.Invoke();
                StartCoroutine(FailCoroutine());
            }
        }
    }
}
