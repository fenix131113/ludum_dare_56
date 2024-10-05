using Core.Data;
using Core.Game;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Fireflies
{
	public class FirefliesHealthIndicator : ITickable, IInitializable
	{
		private readonly VolumeProfile _volumeProfile;
		private readonly FirefliesHealth _health;
		private readonly GameStates _gameStates;

		[Inject]
		public FirefliesHealthIndicator(VolumeProfile volumeProfile, FirefliesHealth health, GameStates gameStates)
		{
			_volumeProfile = volumeProfile;
			_health = health;
			_gameStates = gameStates;
		}

		public void Tick()
		{
			if(_gameStates.PlayerType != PlayerType.FIREFLIES)
				return;
			
			const float divider = 6f;
			
			SetVignette(1f / divider - _health.Health / divider);
		}

		public void Initialize()
		{
			SetVignette(0f);
		}

		private void SetVignette(float value)
		{
			var vignette = (Vignette)_volumeProfile.components[0];
			var parameter = vignette.intensity;
			
			parameter.value = value;
		}
	}
}