using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PurpleCable
{
    public class ColorAnimation : SimpleAnimation
    {
        private SpriteRenderer SpriteRenderer = null;

        [SerializeField] Color EndColor = Color.white;

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();

            if (SpriteRenderer == null)
                Destroy(gameObject);
        }

        protected override void SetEndValue()
        {            
            SpriteRenderer.color = EndColor;
            Debug.Break();
        }

        protected override bool MustUpdate()
        {
            return SpriteRenderer.color.Equals(EndColor);
        }

        protected override void UpdateValue(float t)
        {
            SpriteRenderer.color = Color.Lerp(SpriteRenderer.color, EndColor, t);
        }
    }
}
