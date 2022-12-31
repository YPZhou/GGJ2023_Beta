using UnityEngine;

namespace GGJ2023.Beta
{
	public class LevelManager : MonoBehaviour
	{
		void Start()
		{
			propGenerationInterval = Random.Range(minPropGenerationInterval, maxPropGenerationInterval);
		}

		void Update()
		{
			if (GameStatus.IsGameRunning)
			{
				var gameRunningTime = GameStatus.GetGameRunningTime(Time.realtimeSinceStartup);
				if (gameRunningTime - GameStatus.PropUpdateTime > propGenerationInterval)
				{
					GenerateRandomProp();

					GameStatus.PropUpdateTime = gameRunningTime;
				}

				if (gameRunningTime - GameStatus.ObstacleUpdateTime > 1f)
				{
					GenerateRandomObstacle();

					GameStatus.ObstacleUpdateTime = gameRunningTime;
				}
			}
		}

		void GenerateRandomProp()
		{
			var laneRandomPick = Random.Range(0, 3);
			var propPosition = new Vector3((laneRandomPick - 1) * 2f, GameStatus.OBJECT_INITIAL_VERTICAL_POSITION, 0f);

			var propRandomPick = Random.Range(0, 2);
			Instantiate(propPrefabList[propRandomPick], propPosition, Quaternion.identity);

			propGenerationInterval = Random.Range(minPropGenerationInterval, maxPropGenerationInterval);
		}

		float propGenerationInterval;

		void GenerateRandomObstacle()
		{
			var laserObstacleRandomPick = Random.Range(0, 6);
			switch (laserObstacleRandomPick)
			{
				case 0:		// 3路激光
					break;
				case 1:		// 左右2路激光
					break;
				case 2:		// 左中2路激光
					break;
				case 3:		// 右中2路激光
					break;
				case 4:		// 左路激光
					break;
				case 5:		// 右路激光
					break;
			}
		}

		[SerializeField]
		float minPropGenerationInterval = 3f;

		[SerializeField]
		float maxPropGenerationInterval = 7f;

		[SerializeField]
		GameObject[] propPrefabList;
	}
}
