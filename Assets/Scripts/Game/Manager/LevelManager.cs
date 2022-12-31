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
			var leftLanePosition = new Vector3(-2f, GameStatus.OBJECT_INITIAL_VERTICAL_POSITION, 0f);
			var midLanePosition = new Vector3(0f, GameStatus.OBJECT_INITIAL_VERTICAL_POSITION, 0f);
			var rightLanePosition = new Vector3(2f, GameStatus.OBJECT_INITIAL_VERTICAL_POSITION, 0f);

			switch (laserObstacleRandomPick)
			{
				case 0:     // 3路激光
					GenerateRandomLaser(leftLanePosition);
					GenerateRandomLaser(midLanePosition);
					GenerateRandomLaser(rightLanePosition);
					break;
				case 1:     // 左右2路激光
					GenerateRandomLaser(leftLanePosition);
					GenerateRandomLaser(rightLanePosition);
					break;
				case 2:     // 左中2路激光
					GenerateRandomLaser(leftLanePosition);
					GenerateRandomLaser(midLanePosition);
					break;
				case 3:     // 右中2路激光
					GenerateRandomLaser(midLanePosition);
					GenerateRandomLaser(rightLanePosition);
					break;
				case 4:     // 左路激光
					GenerateRandomLaser(leftLanePosition);
					break;
				case 5:     // 右路激光
					GenerateRandomLaser(rightLanePosition);
					break;
			}
		}

		void GenerateRandomLaser(Vector3 position)
		{
			var laserRandomPick = Random.Range(0, 2);
			Instantiate(laserPrefabList[laserRandomPick], position, Quaternion.identity);
		}

		[SerializeField]
		float minPropGenerationInterval = 3f;

		[SerializeField]
		float maxPropGenerationInterval = 7f;

		[SerializeField]
		GameObject[] propPrefabList;

		[SerializeField]
		GameObject[] laserPrefabList;

		[SerializeField]
		GameObject blockPrefab;

		[SerializeField]
		GameObject trapPrefab;
	}
}
