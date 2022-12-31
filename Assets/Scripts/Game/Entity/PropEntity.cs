using UnityEngine;

namespace GGJ2023.Beta
{
	/// <summary>
	/// 道具实体基类。
	/// </summary>
	public class PropEntity : MonoBehaviour
	{
		void Start()
		{
			if (colorType == ColorType.Red)
			{
				sprite.color = Color.red;
			}
			else if (colorType == ColorType.Blue)
			{
				sprite.color = Color.blue;
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
		SpriteRenderer sprite;

		[SerializeField]
		public ColorType colorType;
	}
}