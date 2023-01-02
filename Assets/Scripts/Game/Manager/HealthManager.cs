using UnityEngine;
using UnityEngine.UI;

namespace GGJ2023.Beta
{
	public class HealthManager : MonoBehaviour
	{
		
		void Update()
		{
			healthCountText.text = GameStatus.Health.ToString();
			for (var i = 0; i < GameStatus.MAX_HEALTH; i++)
			{
				var healthImage = healthImageList[i];
				if (GameStatus.Health > i)
				{
					healthImage.sprite = healthBarFull;
				}
				else
				{
					healthImage.sprite = healthBarEmpty;
				}
			}
		}

		[SerializeField]
		SpriteRenderer[] healthImageList;

		public TextMesh healthCountText;

		public Sprite healthBarFull;
		public Sprite healthBarEmpty;
	}
}
