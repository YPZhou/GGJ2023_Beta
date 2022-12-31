namespace GGJ2023.Beta
{
	public static class GameStatus
	{
		public static bool IsGameStarted { get; set; }
		public static bool IsGameRunning { get; set; }

		public static float GameStartTime { get; set; }
		public static float GamePauseTime { get; set; }

		public static float GetGameRunningTime(float gameTime)
		{
			if (GamePauseTime > -1)
			{
				GamePausedTime += gameTime - GamePauseTime;
				GamePauseTime = -1;
			}

			return gameTime - GameStartTime - GamePausedTime;
		}

		public static float GamePausedTime { get; set; }

		public static float ScoreUpdateTime { get; set; }

		public static float PropUpdateTime { get; set; }

		public static float ObstacleUpdateTime { get; set; }

		public static int Score { get; set; }

		public static float ScoreFactor { get; set; }

		public static int Health { get; set; }

		public static void StartNewGame(float gameStartTime)
		{
			IsGameStarted = true;
			IsGameRunning = true;

			GameStartTime = gameStartTime;
			GamePauseTime = -1f;
			GamePausedTime = 0f;

			ScoreUpdateTime = 0f;

			Score = 0;
			ScoreFactor = BASE_SCORE_FACTOR;

			Health = MAX_HEALTH;
		}

		public static void PauseGame(float gamePauseTime)
		{
			IsGameRunning = false;
			GamePauseTime = gamePauseTime;
			GamePausedTime = 0f;
		}

		public static void ResumeGame()
		{
			IsGameRunning = true;
		}

		public static void GameOver()
		{
			IsGameStarted = false;
			IsGameRunning = false;
		}

		public const int BASE_SCORE_GAIN = 100;

		public const float BASE_SCORE_FACTOR = 1f;

		public const float BASE_SCORE_FACTOR_GAIN = 0.1f;

		public const float SCORE_GAIN_INTERVAL = 1f;

		public const int MAX_HEALTH = 3;

		public const float VERTICAL_MOVE_SPEED = 1f;

		public const float OBJECT_INITIAL_VERTICAL_POSITION = 5f;
		
		public const int HURT_HEALTH = 1;

		public const float BASE_BUFF_DURATION = 5f;
		
		public const float BASE_BUFF_DELAY = 5f;
	}
}
