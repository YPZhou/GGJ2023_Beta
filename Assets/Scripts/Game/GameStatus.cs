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

		public static int Score { get; set; }

		public static float ScoreFactor { get; set; }

		public static int Health { get; set; }


		public const int BASE_SCORE_GAIN = 100;

		public const float BASE_SCORE_FACTOR = 1f;

		public const float BASE_SCORE_FACTOR_GAIN = 0.1f;
	}
}
