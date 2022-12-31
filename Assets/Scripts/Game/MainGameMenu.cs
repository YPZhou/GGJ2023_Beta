using UnityEngine;
using UnityEngine.UI;

namespace GGJ2023.Beta
{
	public class MainGameMenu : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{
			GameStatus.IsGameRunning = true;
		}

		// Update is called once per frame
		void Update()
		{
			if (GameStatus.IsGameRunning)
			{
				mainGameMenu.gameObject.SetActive(false);

				if (Input.GetKeyUp(KeyCode.Escape))
				{
					GameStatus.IsGameRunning = false;
				}
			}
			else
			{
				mainGameMenu.gameObject.SetActive(true);

				for (var i = 0; i < gameMenuTextList.Length; i++)
				{
					var gameMenuText = gameMenuTextList[i];
					if (currentGameMenu == i)
					{
						gameMenuText.color = Color.red;
					}
					else
					{
						gameMenuText.color = Color.white;
					}
				}

				if (Input.GetKeyUp(KeyCode.UpArrow))
				{
					currentGameMenu -= 1;
					if (currentGameMenu < 0)
					{
						currentGameMenu = 0;
					}
				}

				if (Input.GetKeyUp(KeyCode.DownArrow))
				{
					currentGameMenu += 1;
					if (currentGameMenu > 2)
					{
						currentGameMenu = 2;
					}
				}

				if (Input.GetKeyUp(KeyCode.Return))
				{
					switch (currentGameMenu)
					{
						case 0:	// resume game
							GameStatus.IsGameRunning = true;
							break;
						case 1: // start new game
							GameStatus.IsGameRunning = true;
							break;
						case 2: // quit game
#if UNITY_EDITOR
							UnityEditor.EditorApplication.isPlaying = false;
#endif
							Application.Quit();
							break;
					}
				}
			}
		}

		[SerializeField]
		Image mainGameMenu;

		[SerializeField]
		Text[] gameMenuTextList;

		int currentGameMenu;
	}
}
