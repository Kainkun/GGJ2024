using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{ 
    public class PaperRandomizer : MonoBehaviour
    {
        [SerializeField] 
        private MessageGroup[] paperMessages;

        [SerializeField] 
        private SpriteRenderer spriteRenderer;

        [SerializeField] 
        private float randomChance = 0.5f;

        [SerializeField] 
        private int initialDelay = 3;

        public State CurrentState { get; set; } = State.Explore;

        [Serializable]
        public class MessageGroup
        {
            public Sprite[] messageOptions;
            public Sprite defaultSprite;
            public State state;
            
            [NonSerialized]
            public int CurrentIndex;
        }

        public enum State
        {
            Explore, 
            Chaos,
            Maze,
            Success,
        }
        
        public void Randomize()
        {
            if (initialDelay > 0)
            {
                initialDelay--;
                return;
            }

            foreach (MessageGroup messageGroup in paperMessages)
            {
                if (messageGroup.state == CurrentState)
                {
                    if (Random.value < randomChance)
                    {
                        spriteRenderer.sprite = messageGroup.messageOptions[messageGroup.CurrentIndex];
                        messageGroup.CurrentIndex = (messageGroup.CurrentIndex + 1) % messageGroup.messageOptions.Length;
                    }
                    else
                    {
                        spriteRenderer.sprite = messageGroup.defaultSprite;
                    }
                    return;
                }
            }

        }
    }
}
