using UnityEngine;

namespace GGJ2023.Beta
{
	public class ScoreManager : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{
		
		}

		// Update is called once per frame
		void Update()
		{
			if (GameStatus.IsGameRunning)
			{
				var gameRunningTime = GameStatus.GetGameRunningTime(Time.realtimeSinceStartup);
			}
		}
	}
}
