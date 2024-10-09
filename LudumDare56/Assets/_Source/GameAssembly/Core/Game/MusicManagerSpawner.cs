using UnityEngine;

namespace Core.Game
{
	public class MusicManagerSpawner : MonoBehaviour  // Not enough time to do normally :(
	{
		[SerializeField] private GameObject musicManagerPrefab;

		private void Start()
		{
			if(!FindObjectOfType<MusicManager>())
				Instantiate(musicManagerPrefab);
		}
	}
}