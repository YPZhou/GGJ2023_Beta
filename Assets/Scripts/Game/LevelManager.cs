using UnityEngine;

namespace GGJ2023.Beta
{
	public class LevelManager : MonoBehaviour
	{
		void Start()
		{
		
		}

		void Update()
		{
			if (GameStatus.IsGameRunning)
			{
				var gameRunningTime = GameStatus.GetGameRunningTime(Time.realtimeSinceStartup);
			}
		}
	}
}
