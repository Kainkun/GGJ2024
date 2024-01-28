using System.Collections;
using Cinemachine;
using UnityEngine;

namespace ScriptedEvents
{
    public class CashierHelpEvent : MonoBehaviour
    {
        [SerializeField] 
        private CinemachineVirtualCamera eventCamera;
        
        private bool _canProgress;

        public void HandlePlayerInteractWithCashier()
        {
            _canProgress = true;
        }
        
        public void StartEvent()
        {
            StartCoroutine(EventCoroutine());
        }

        private IEnumerator EventCoroutine()
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            Coroutine askIfYouNeedHelp = DialogueSystem.Instance.RunDialogue("explore_check_on_player");
            
            while (!_canProgress)
                yield return null;
            
            StopCoroutine(askIfYouNeedHelp);
            player.StateMachine.TransitionTo(null);
            eventCamera.Priority = 5;

            yield return DialogueSystem.Instance.RunDialogue("explore_direct_to_milk");
            FindAnyObjectByType<TeleportSystem>().Teleport();
        }
    }
}
