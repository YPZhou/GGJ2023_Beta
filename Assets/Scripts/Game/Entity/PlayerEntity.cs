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

        private void Awake()
        {
            Instance = this;
            Collider2D = GetComponent<CircleCollider2D>();
        }
        

        [Tooltip("横轴移动速度")]
        [SerializeField]
        private float moveSpeed = 5f;


        private void Update()
        {
            var axis = Input.GetAxisRaw("Horizontal");
            var translation = new Vector2(axis * moveSpeed * Time.deltaTime, 0);
            transform.Translate(translation);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Prop"))
            {
                
            }
        }


        void OnCollisionProp()
        {
            
        }

        void OnCollisionTrap()
        {
            
        }
    }
}