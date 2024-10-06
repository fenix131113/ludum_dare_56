using Player.Data;
using UnityEngine;
using Zenject;

namespace Player
{
	public class PlayerHealth
	{
		public float Health { get; private set; }
		
		private readonly PlayerConfig _config;

		[Inject]
		public PlayerHealth(PlayerConfig playerConfig)
		{
			_config = playerConfig;

			Health = _config.MaxHealth;
		}
		
		public void DecreaseHealth(float value) => Health = Mathf.Clamp(Health - value, 0, _config.MaxHealth);
		public void IncreaseHealth(float value) => Health = Mathf.Clamp(Health + value, 0, _config.MaxHealth);
	}
}