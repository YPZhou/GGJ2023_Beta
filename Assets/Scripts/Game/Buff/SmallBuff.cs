using UnityEngine;

namespace GGJ2023.Beta
{
    public class SmallBuff : BuffBase
    {
        protected override void OnEffectBegin()
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        protected override void OnEffectOver()
        {
            transform.localScale = Vector3.one;
        }
    }
}