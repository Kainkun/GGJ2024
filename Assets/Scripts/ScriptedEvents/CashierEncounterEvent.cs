using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

namespace ScriptedEvents
{
    public class CashierEncounterEvent : MonoBehaviour
    {
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        
        [Header("Settings")]
        [SerializeField] private int clicksToPutDownMilk = 3;
        [SerializeField] private float maxMilkShakeIntensity = 2;
    
        [Header("References")]
        [SerializeField] private ShakeApplier milkShake;
        [SerializeField] private CinemachineVirtualCamera encounterCamera;
        [SerializeField] private PlayableDirector timelineAnimation;
        [SerializeField] private Animator epilogueAnimator;

        public void PauseTimeline()
        {
            timelineAnimation.Pause();
        }
        
        public void StartEvent()
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            player.StateMachine.TransitionTo(null);
            StartCoroutine(EncounterCoroutine());
        }
        
        private IEnumerator EncounterCoroutine()
        {
            // move camera to initial position
            encounterCamera.Priority = 5;
        
            // [dialogue] play cashier initial dialogue
            yield return DialogueSystem.Instance.RunDialogue("dread_where_are_you_going");
        
            // [timeline] slow, shaky turn to counter, mood change
            yield return StartCoroutine(timelineAnimation.PlayCoroutine());
        
            // [dialogue] ask to put down on counter
            yield return DialogueSystem.Instance.RunDialogue("dread_put_down_on_counter");
        
            // [timeline] you go to put the milk down, but pause
            yield return StartCoroutine(timelineAnimation.PlayCoroutine());
        
            // [interaction] waits for you to put click x times to put onto counter, occasional comments if u take long
            yield return StartCoroutine(PutDownMilkInteraction());
        
            // [timeline] clerk checks out the milk, making small talk
            yield return StartCoroutine(timelineAnimation.PlayCoroutine());
        
            // [dialogue] asks for payment
            yield return DialogueSystem.Instance.RunDialogue("dread_you_need_to_pay");
        
            // [interaction] needs to hold up coupon, occasional comments if take long
            while (!Input.anyKeyDown)
                yield return null;
        
            // [timeline] places coupon on counter, clerk inspects it and comments on it, you are finally good to go.
            yield return StartCoroutine(timelineAnimation.PlayCoroutine());
        
            epilogueAnimator.SetTrigger(FadeOut);
        }

        private IEnumerator PutDownMilkInteraction()
        {
            int remainingAttempts = clicksToPutDownMilk;
        
            while (remainingAttempts > 0)
            {
                if (Input.anyKeyDown)
                {
                    remainingAttempts--;
                    // todo: this shake really isnt that well done
                    milkShake.Strength = (clicksToPutDownMilk + 1.0f - remainingAttempts) / clicksToPutDownMilk * maxMilkShakeIntensity;
                }
                yield return null;
            }
        }
    }
}
