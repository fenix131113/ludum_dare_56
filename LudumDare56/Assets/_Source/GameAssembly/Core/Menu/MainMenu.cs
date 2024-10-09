using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.Menu
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private Button startGameButton;
		[SerializeField] private Button exitButton;

		private void Start()
		{
			Bind();
		}

		private void Bind()
		{
			startGameButton.onClick.AddListener(() => SceneManager.LoadScene(1));
			exitButton.onClick.AddListener(Application.Quit);
		}

		private void Expose()
		{
			startGameButton.onClick.RemoveAllListeners();
			exitButton.onClick.RemoveAllListeners();
		}

		private void OnApplicationQuit() => Expose();
	}
}
