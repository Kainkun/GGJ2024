using UnityEngine;

namespace ScriptedEvents
{
    public class CashierEncounterEvent : MonoBehaviour
    {
        public PlayerCashierEncounterState cashierEncounterState;
        
        public void StartEvent()
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            cashierEncounterState.Initialize(player);
            player.StateMachine.TransitionTo(cashierEncounterState);
        }

        public void PauseTimeline()
        {
            cashierEncounterState.PauseTimeline();
        }
    }
}
