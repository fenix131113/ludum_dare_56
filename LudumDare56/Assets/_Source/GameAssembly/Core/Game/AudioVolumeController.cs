using UnityEngine;

namespace Core.Game
{
	public class AudioVolumeController : MonoBehaviour
	{
		private float _savedVolume = 1;
		
		private void Awake()
		{
			AudioListener.volume = _savedVolume;
		}

		public void SetVolume(float volume)
		{
			_savedVolume = volume;
			AudioListener.volume = volume;
		}
	}
}