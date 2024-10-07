using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.Menu
{
	public class GamePauseMenu : MonoBehaviour
	{
		[SerializeField] private GameObject menuBlocker;
		[SerializeField] private Button continueButton;
		[SerializeField] private Button exitButton;
		
		public bool IsPaused { get; private set; }

		public void PauseGame()
		{
			Bind();
			IsPaused = true;
			menuBlocker.SetActive(true);
		}

		public void UnpauseGame()
		{
			Expose();
			IsPaused = false;
			menuBlocker.SetActive(false);
		}

		private void LoadMenu() => SceneManager.LoadScene(0);

		private void Bind()
		{
			continueButton.onClick.AddListener(UnpauseGame);
			exitButton.onClick.AddListener(LoadMenu);
		}

		private void Expose()
		{
			exitButton.onClick.RemoveAllListeners();
			continueButton.onClick.RemoveAllListeners();
		}

		private void OnApplicationQuit() => Expose();
	}
}