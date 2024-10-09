using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable FunctionRecursiveOnAllPaths

namespace Core.Game
{
	public class MusicManager : MonoBehaviour // Not enough time to do normally :(
	{
		[SerializeField] private AudioSource playSource;
		[SerializeField] private AudioClip[] musics;

		private void CheckScene(Scene scene, LoadSceneMode mode) => SelectMusic(scene);

		private void Start()
		{
			DontDestroyOnLoad(this);
			SceneManager.sceneLoaded += CheckScene;
		}

		private void SelectMusic(Scene scene)
		{
			switch (scene.buildIndex)
			{
				case 0:
				case 1:
				case 8:
					if (playSource.clip == musics[0])
						break;
					playSource.clip = musics[0];
					playSource.Play();
					break;
				case 2:
					playSource.clip = musics[2];
					playSource.Play();
					break;
				case 5:
				case 6:
				case 7:
					if (playSource.clip == musics[0])
						break;
					playSource.clip = musics[1];
					playSource.Play();
					break;
				case 3:
					playSource.Stop();
					break;
				case 4:
					playSource.Stop();
					break;
			}
		}
	}
}