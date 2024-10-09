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
				case 7:
				case 8:
					if (playSource.clip == musics[0])
						break;
					
					playSource.volume = 1f;
					playSource.clip = musics[0];
					playSource.Play();
					break;
				case 2:
					playSource.clip = musics[2];
					playSource.volume = 1f;
					playSource.Play();
					break;
				case 3:
				case 4:
				case 6:
					if (playSource.clip == musics[1])
						break;
					
					playSource.volume = 0.05f;
					playSource.clip = musics[1];
					playSource.Play();
					break;
				case 5:
					playSource.Stop();
					break;
			}
		}
	}
}