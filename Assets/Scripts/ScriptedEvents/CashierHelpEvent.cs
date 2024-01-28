using System.Collections;
using Cinemachine;
using UnityEngine;

namespace ScriptedEvents
{
    public class CashierHelpEvent : MonoBehaviour
    {
        [SerializeField] 
        private CinemachineVirtualCamera eventCamera;

        [SerializeField] 
        private TeleportNext teleportNext;
        
        private State _currentState = State.PreEvent;

        public void HandlePlayerInteractWithCashier()
        {
            switch (_currentState)
            {
                case State.PreEvent:
                    DialogueSystem.Instance.RunDialogue("clerk_1_pre_event");
                    break;
                case State.PostEvent:
                    DialogueSystem.Instance.RunDialogue("clerk_1_post_event");
                    break;
                case State.WaitingForPlayer:
                    _currentState = State.TalkingToPlayer;
                    break;
            }
        }
        
        public void StartEvent()
        {
            StartCoroutine(EventCoroutine());
        }

        private IEnumerator EventCoroutine()
        {
            _currentState = State.WaitingForPlayer;
            PlayerController player = FindAnyObjectByType<PlayerController>();
            DialogueSystem.Instance.RunDialogueRepeating("explore_check_on_player");
            
            while (_currentState != State.TalkingToPlayer)
                yield return null;
            
            DialogueSystem.Instance.StopDialogue("explore_check_on_player");
            player.StateMachine.TransitionTo(null);
            eventCamera.Priority = 5;
            yield return DialogueSystem.Instance.RunDialogue("explore_direct_to_milk");
            eventCamera.Priority = 0;
            yield return new WaitForSeconds(FindAnyObjectByType<CinemachineBrain>().m_DefaultBlend.BlendTime + 0.5f);
            teleportNext.Teleport();
            player.StateMachine.TransitionTo(player.freeMovementState);
            _currentState = State.PostEvent;
        }

        private enum State
        {
            PreEvent,
            WaitingForPlayer,
            TalkingToPlayer,
            PostEvent,
        }
    }
}
