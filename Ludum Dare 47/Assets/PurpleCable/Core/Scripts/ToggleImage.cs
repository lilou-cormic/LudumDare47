using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PurpleCable
{
    public class ToggleImage : MonoBehaviour
    {
        [SerializeField]
        private Image Image = null;

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

            SetCurrentSprite();
        }

        private void SetCurrentSprite()
        {
            if (Image != null)
                Image.sprite = Sprites.ElementAtOrDefault(CurrentSpriteIndex);
        }
    }
}