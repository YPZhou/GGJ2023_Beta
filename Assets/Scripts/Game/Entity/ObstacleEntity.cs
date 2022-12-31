using UnityEngine;

namespace GGJ2023.Beta
{
    /// <summary>
    /// 障碍实体基类。
    /// </summary>
    public class ObstacleEntity : MonoBehaviour
    {
        [SerializeField]
        public ObstacleType obstacleType;
        
        [SerializeField]
        public ColorType colorType;

    }


    
}