using UnityEngine;

namespace Player
{
    public class PaperRandomizer : MonoBehaviour
    {
        [SerializeField] 
        private Sprite[] paperMessages;

        [SerializeField] 
        private SpriteRenderer spriteRenderer;

        [SerializeField] 
        private Sprite defaultSprite;

        [SerializeField] 
        private float randomChance = 0.5f;

        [SerializeField] 
        private int initialDelay = 3;
        
        public void Randomize()
        {
            if (initialDelay > 0)
            {
                initialDelay--;
                return;
            }

            if (Random.value < randomChance)
            {
                int randomIndex = Random.Range(0, paperMessages.Length);
                spriteRenderer.sprite = paperMessages[randomIndex];
            }
            else
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }
    }
}
