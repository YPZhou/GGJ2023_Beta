using UnityEngine;

namespace GGJ2023.Beta
{
    public class BuffBase : MonoBehaviour
    {
        /// <summary>
        /// 是否生效中。
        /// </summary>
        public bool IsEffective { get; private set; }

        /// <summary>
        /// 剩余时间。
        /// </summary>
        public float EffectiveTime { get; private set; }

        /// <summary>
        /// 生效时的回调函数。
        /// </summary>
        protected virtual void OnEffective()
        {
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnEffectBegin()
        {
            
        }

        
        protected virtual void OnEffectOver()
        {
            
        }

        /// <summary>
        /// 使buff生效。
        /// </summary>
        public void TakeEffect(float duration)
        {
            EffectiveTime = duration;
            IsEffective = true;
            OnEffectBegin();
        }

        /// <summary>
        /// 延长buff时间。
        /// </summary>
        /// <param name="duration"></param>
        public void Delay(float duration)
        {
            EffectiveTime += duration;
        }

        private void Update()
        {
            
            if (IsEffective)
            {
                if (EffectiveTime > 0)
                {
                    OnEffective();
                    EffectiveTime -= Time.deltaTime;
                }
                else
                {
                    EffectiveTime = 0;
                    IsEffective = false;
                    // 移除组件。
                    Destroy(this);
                    OnEffectOver();
                }
                
            }
            
            
        }
    }
}