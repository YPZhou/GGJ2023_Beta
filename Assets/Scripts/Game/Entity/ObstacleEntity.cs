using UnityEngine;

namespace GGJ2023.Beta
{
	/// <summary>
	/// 障碍实体基类。
	/// </summary>
	public class ObstacleEntity : MonoBehaviour
	{
		void Start()
		{
			if (obstacleType == ObstacleType.Laser)
			{
				if (colorType == ColorType.Red)
				{
					sprite.color = Color.red;
				}
				else if (colorType == ColorType.Blue)
				{
					sprite.color = Color.blue;
				}
			}
			else if (obstacleType == ObstacleType.Block)
			{
				sprite.color = Color.yellow;
			}
			else if (obstacleType == ObstacleType.Trap)
			{
				sprite.color = Color.gray;
			}

			generationTime = GameStatus.GetGameRunningTime(Time.realtimeSinceStartup);
		}

		float generationTime;

		void Update()
		{
			if (GameStatus.IsGameRunning)
			{
				var gameRunningTime = GameStatus.GetGameRunningTime(Time.realtimeSinceStartup);
				var verticalTranslation = (gameRunningTime - generationTime) * GameStatus.VERTICAL_MOVE_SPEED;
				transform.position = new Vector3(transform.position.x, GameStatus.OBJECT_INITIAL_VERTICAL_POSITION - verticalTranslation);

				if (transform.position.y < -6f)
				{
					Destroy(gameObject);
				}
			}
		}

		[SerializeField]
		public ObstacleType obstacleType;
		
		[SerializeField]
		public ColorType colorType;

		[SerializeField]
		SpriteRenderer sprite;

		public void Death()
		{
			Destroy(gameObject);
		}
	}
}