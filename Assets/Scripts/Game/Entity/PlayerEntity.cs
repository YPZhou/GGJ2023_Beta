using UnityEngine;

namespace GGJ2023.Beta
{
    /// <summary>
    /// 玩家控制器。
    /// </summary>
    public class PlayerEntity : MonoBehaviour
    {
        public static PlayerEntity Instance { get; private set; }

        public CircleCollider2D Collider2D { get; private set; }
        
        
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            Collider2D = GetComponent<CircleCollider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            
            SpriteRenderer.color = colorType switch
            {
                ColorType.Red => Color.red,
                ColorType.Blue => Color.blue,
                _ => SpriteRenderer.color
            };
        }
        

        [Tooltip("横轴移动速度")]
        [SerializeField]
        private float moveSpeed = 5f;
        
        [SerializeField]
        private ColorType colorType;
        
        
            
        private void Update()
        {
            var axis = Input.GetAxisRaw("Horizontal");
            var translation = new Vector2(axis * moveSpeed * Time.deltaTime, 0);
            transform.Translate(translation);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<PropEntity>(out var propEntity))
            {
                OnCollisionProp(propEntity);
            }

            if (other.gameObject.TryGetComponent<ObstacleEntity>(out var obstacleEntity))
            {
                OnCollisionObstacle(obstacleEntity);
            }
        }

        void OnCollisionProp(PropEntity propEntity)
        {

            if (TryGetComponent<HugeBuff>(out var hugeBuff))
            {
                // 接触到同色道具时球体会变大，但速度会变慢
                if (colorType == propEntity.colorType)
                {
                    hugeBuff.Delay(5f);
                }
                else
                {
                    // 拾取到异色道具时变为对应的颜色
                    colorType = propEntity.colorType;
                }

            }
            else if (TryGetComponent<SmallBuff>(out var smallBuff))
            {
                if (colorType == propEntity.colorType)
                {
                    smallBuff.Delay(5f);
                }
                else
                {
                    // 拾取到异色道具时变为对应的颜色
                    colorType = propEntity.colorType;
                }
            }
            else
            {
                if (colorType == propEntity.colorType)
                {
                    AddBuff<HugeBuff>(5f);
                }
                else
                {
                    AddBuff<SmallBuff>(5f);
                }
            }

            Destroy(propEntity.gameObject);
        }

        void OnCollisionObstacle(ObstacleEntity obstacleEntity)
        {
            
        }

        void AddBuff<T>(float duration) where T : BuffBase
        {
            var buff = gameObject.GetComponent<T>();
            if (buff == null)
            {
                buff = gameObject.AddComponent<T>();
                buff.TakeEffect(duration);
            }
        }
        

    }
}