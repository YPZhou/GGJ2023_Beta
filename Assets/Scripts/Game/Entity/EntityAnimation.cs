using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Beta
{
    public class EntityAnimation : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        [SerializeField]
        Sprite[] AnimationFrames;

        [Range(1, 15)]
        public int TickPerFrame = 10;

        private int tick = 0;
        private int frameIndex = 0;

        private void FixedUpdate()
        {
            if (tick < TickPerFrame)
            {
                tick++;
            }
            else
            {
                tick = 0;
                frameIndex++;
                if (frameIndex >= AnimationFrames.Length)
                {
                    frameIndex = 0;
                }

                spriteRenderer.sprite = AnimationFrames[frameIndex];
            }
        }
    }
}
