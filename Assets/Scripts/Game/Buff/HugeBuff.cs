using UnityEngine;

namespace GGJ2023.Beta
{
    public class HugeBuff : BuffBase
    {
        protected override void OnEffectBegin()
        {
            transform.localScale = new Vector3(2, 2, 1);
        }

        protected override void OnEffectOver()
        {
            transform.localScale = Vector3.one;
        }
    }
}