using Core.Data;
using Core.Game;
using Fireflies;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Player
{
	public class HealthIndicator : ITickable, IInitializable
	{
		private readonly VolumeProfile _volumeProfile;
		private readonly PlayerHealth _health;
		private readonly GameStates _gameStates;
		private readonly FirefliesContainer _firefliesContainer;

		[Inject]
		public HealthIndicator(VolumeProfile volumeProfile, PlayerHealth health, GameStates gameStates, FirefliesContainer firefliesContainer)
		{
			_volumeProfile = volumeProfile;
			_health = health;
			_gameStates = gameStates;
			_firefliesContainer = firefliesContainer;
		}

		public void Tick()
		{
			if(_gameStates.PlayerType == PlayerType.FIREFLIES && _firefliesContainer.InvisibleModule.IsInvisible)
				return;
			
			const float divider = 3f;
			
			SetVignette(1f / divider - _health.Health / divider);
		}

		public void Initialize()
		{
			SetVignette(0f);
		}

		private void SetVignette(float value)
		{
			Debug.Log(value);
			var vignette = (Vignette)_volumeProfile.components[0];
			var parameter = vignette.intensity;
			
			parameter.value = value;
		}
	}
}