using System;
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

        public SpriteRenderer SpriteRenderer;
        
        private void Awake()
        {
            Instance = this;
            Collider2D = GetComponent<CircleCollider2D>();
            ChangeColor(colorType);
        }
        

        [Tooltip("横轴移动速度")]
        [SerializeField]
        public float moveSpeed = 5f;
        
        [SerializeField]
        private ColorType colorType;

        
        private Vector2 translation;
        
        private void Update()
        {
            if (!GameStatus.IsGameRunning)
            {
                return;
            }
            
            var axis = Input.GetAxisRaw("Horizontal");
            translation = new Vector2(axis * moveSpeed * Time.deltaTime, 0);
            var contactFilter2D = new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = LayerMask.GetMask("Border")
            };
            var result = new RaycastHit2D[1];
            var origin = Collider2D.bounds.center;
            var distance = translation.magnitude + Collider2D.bounds.extents.x;
            Physics2D.Raycast(origin, translation.normalized, contactFilter2D, result, distance);
            if (result[0].collider != null)
            {
                translation = Vector2.zero;
            }
            
            transform.Translate(translation);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsHuge || IsSmall)
                {
                    return;
                }

				SoundManager.PlayAudio("switch_color");
				ChangeColor(colorType.Reverse());

            }
        }

        
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PropEntity>(out var propEntity))
            {
                OnTriggerProp(propEntity);
            }
            
            if (other.gameObject.TryGetComponent<ObstacleEntity>(out var obstacleEntity))
            {
                OnTriggerObstacle(obstacleEntity);
            }
        }

        
        void OnTriggerProp(PropEntity propEntity)
        {
            SoundManager.PlayAudio("pick_prop");
            SoundManager.PlayAudio("switch_color");
            // 拾取到异色道具时变为对应的颜色
            ChangeColor(propEntity.colorType);
            
            if (TryGetComponent<HugeBuff>(out var hugeBuff))
            {
                hugeBuff.Delay(GameStatus.BASE_BUFF_DELAY);
            }
            else if (TryGetComponent<SmallBuff>(out var smallBuff))
            {
                smallBuff.Delay(GameStatus.BASE_BUFF_DELAY);
            }
            else
            {
                if (colorType == propEntity.colorType)
                {
                    AddBuff<HugeBuff>();
                }
                else
                {
                    AddBuff<SmallBuff>();
                }
            }

            propEntity.Death();

        }

        void OnTriggerObstacle(ObstacleEntity obstacleEntity)
        {
            switch (obstacleEntity.obstacleType)
            {
                // 激光。
                case ObstacleType.Laser:
                {
                    if (IsSmall)
                    {
                        return;
                    }

                    if (IsHuge)
                    {
                        obstacleEntity.Death();
                        return;
                    }

                    if (colorType != obstacleEntity.colorType)
                    {
                        Hurt();
                        obstacleEntity.Death();
                    }

                    break;
                }
                // 障碍。
                case ObstacleType.Block:
                {
                    obstacleEntity.Death();
                    if (IsHuge)
                    {
                        return;
                    }
                    
                    Hurt();
                    break;
                }
                
                // 陷阱。
                case ObstacleType.Trap:
                {
                    if (IsSmall)
                    {
                        return;
                    }
                    
                    Hurt();
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        void ChangeColor(ColorType color)
        {
            if (colorType != color)
            {
                colorType = color;
                switch (color)
                {
                    case ColorType.Red:
                        SpriteRenderer.color = ArtStatus.ColorA;
                        break;
                    case ColorType.Blue:
                        SpriteRenderer.color = ArtStatus.ColorB;
                        break;
                    default:
                        return;
                }
            }
        }

        void AddBuff<T>() where T : BuffBase
        {
            var buff = gameObject.GetComponent<T>();
            if (buff == null)
            {
                buff = gameObject.AddComponent<T>();
                buff.TakeEffect(GameStatus.BASE_BUFF_DURATION);
            }
        }


        void Hurt()
        {
            SoundManager.PlayAudio("lose_health");
            Debug.Log("Hurt");
            // 颜色不同时扣血，重置得分倍率。
            GameStatus.Health -= GameStatus.HURT_HEALTH;
            GameStatus.ScoreFactor = GameStatus.BASE_SCORE_FACTOR;
            
            // 血量归零游戏结束。
            if (GameStatus.Health <= 0)
            {
                GameStatus.GameOver();
            }
        }

        public bool IsHuge => TryGetComponent<HugeBuff>(out _);
        
        public bool IsSmall => TryGetComponent<SmallBuff>(out _);


        public float BuffDuration 
        {
            get
            {
                if (TryGetComponent<HugeBuff>(out var hugeBuff))
                {
                    return hugeBuff.EffectiveTime;
                }
                else if (TryGetComponent<SmallBuff>(out var smallBuff))
                {
                    return smallBuff.EffectiveTime;
                }

                return 0f;
            }
        }
        
    }
}