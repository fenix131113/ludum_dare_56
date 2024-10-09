using Core.Game;
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
		[SerializeField] private Slider volumeSlider;
		
		public bool IsPaused { get; private set; }

		private AudioVolumeController _audioVolumeController;

		private void Start()
		{
			_audioVolumeController = FindObjectOfType<AudioVolumeController>();
			volumeSlider.value = AudioListener.volume;
		}

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

		private static void LoadMenu() => SceneManager.LoadScene(0);

		private void OnVolumeChanged(float value)
		{
			_audioVolumeController.SetVolume(value);
		}
		
		private void Bind()
		{
			continueButton.onClick.AddListener(UnpauseGame);
			exitButton.onClick.AddListener(LoadMenu);

			volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
		}

		private void Expose()
		{
			exitButton.onClick.RemoveAllListeners();
			continueButton.onClick.RemoveAllListeners();
		}

		private void OnApplicationQuit() => Expose();
	}
}