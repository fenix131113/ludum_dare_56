using UnityEngine.SceneManagement;

namespace Utils
{
	public static class GameRestart
	{
		public static void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}