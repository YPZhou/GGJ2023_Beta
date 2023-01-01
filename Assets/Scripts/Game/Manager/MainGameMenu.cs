using UnityEngine;
using UnityEngine.UI;

namespace GGJ2023.Beta
{
	public class MainGameMenu : MonoBehaviour
	{
		void Start()
		{
			GameStatus.IsGameStarted = false;
			GameStatus.IsGameRunning = false;

			currentGameMenu = 1;
		}

		void Update()
		{
			if (GameStatus.IsGameRunning)
			{
				mainGameMenu.gameObject.SetActive(false);

				if (Input.GetKeyUp(KeyCode.Escape))
				{
					GameStatus.PauseGame(Time.realtimeSinceStartup);
					currentGameMenu = 0;
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
						if (i == 0)
						{
							if (GameStatus.IsGameStarted)
							{
								gameMenuText.color = Color.white;
							}
							else
							{
								gameMenuText.color = Color.gray;
							}
						}
						else
						{
							gameMenuText.color = Color.white;
						}
					}
				}

				if (Input.GetKeyUp(KeyCode.UpArrow))
				{
					currentGameMenu -= 1;
					if (GameStatus.IsGameStarted)
					{
						if (currentGameMenu < 0)
						{
							currentGameMenu = 0;
						}
					}
					else
					{
						if (currentGameMenu < 1)
						{
							currentGameMenu = 1;
						}
					}
					SoundManager.PlayAudio("menu_change");
				}

				if (Input.GetKeyUp(KeyCode.DownArrow))
				{
					currentGameMenu += 1;
					if (currentGameMenu > 2)
					{
						currentGameMenu = 2;
					}
					SoundManager.PlayAudio("menu_change");
				}

				if (Input.GetKeyUp(KeyCode.Return))
				{
					switch (currentGameMenu)
					{
						case 0: // resume game
							GameStatus.ResumeGame();
							break;
						case 1: // start new game
							GameStatus.StartNewGame(Time.realtimeSinceStartup);
							break;
						case 2: // quit game
#if UNITY_EDITOR
							UnityEditor.EditorApplication.isPlaying = false;
#endif
							Application.Quit();
							break;
					}
					SoundManager.PlayAudio("menu_click");
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
