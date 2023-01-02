using UnityEngine;
using UnityEngine.UI;

namespace GGJ2023.Beta
{
	public class ScoreManager : MonoBehaviour
	{
		void Update()
		{
			if (GameStatus.IsGameRunning)
			{
				var gameRunningTime = GameStatus.GetGameRunningTime(Time.realtimeSinceStartup);
				if (gameRunningTime - GameStatus.ScoreUpdateTime > GameStatus.SCORE_GAIN_INTERVAL)
				{
					GameStatus.Score += Mathf.FloorToInt(GameStatus.BASE_SCORE_GAIN * GameStatus.ScoreFactor);
					GameStatus.ScoreFactor += GameStatus.BASE_SCORE_FACTOR_GAIN;

					GameStatus.ScoreUpdateTime = gameRunningTime;
				}

				scoreText.text = GameStatus.Score.ToString();
				scoreFactorText.text = GameStatus.ScoreFactor.ToString("f1");
			}
		}

		[SerializeField]
		TextMesh scoreText;

		[SerializeField]
		TextMesh scoreFactorText;
	}
}
