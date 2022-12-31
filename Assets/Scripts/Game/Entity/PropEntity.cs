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
		}

		[SerializeField]
		SpriteRenderer sprite;

		[SerializeField]
		public ColorType colorType;
	}
}