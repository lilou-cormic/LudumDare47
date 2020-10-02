using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class ToggleSprite : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer SpriteRenderer = null;

        [SerializeField]
        private Sprite[] Sprites = null;

        [SerializeField]
        private int CurrentSpriteIndex = 0;

        private void OnValidate()
        {
            SetCurrentSprite();
        }

        public void Toggle()
        {
            CurrentSpriteIndex = (CurrentSpriteIndex + 1) % Sprites.Length;
        }

        private void SetCurrentSprite()
        {
            if (SpriteRenderer != null)
                SpriteRenderer.sprite = Sprites.ElementAtOrDefault(CurrentSpriteIndex);
        }
    }
}