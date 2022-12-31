using UnityEngine;
using UnityEngine.UI;

namespace GGJ2023.Beta
{
	public class LevelManager : MonoBehaviour
	{

		void Update()
		{
			for (var i = 0; i < GameStatus.MAX_HEALTH; i++)
			{
				var healthImage = healthImageList[i];
				if (GameStatus.Health > i)
				{
					healthImage.color = Color.red;
				}
				else
				{
					healthImage.color = Color.gray;
				}
			}
		}

		[SerializeField]
		Image[] healthImageList;
	}
}
