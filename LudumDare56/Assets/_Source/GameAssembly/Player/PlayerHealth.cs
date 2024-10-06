using System;
using Player.Data;
using UnityEngine;
using Zenject;

namespace Player
{
	public class PlayerHealth
	{
		public float Health { get; private set; }
		public bool IsDead { get; private set; }
		
		private readonly PlayerConfig _config;
		
		public event Action<string> OnDeath;

		[Inject]
		public PlayerHealth(PlayerConfig playerConfig)
		{
			_config = playerConfig;

			Health = _config.MaxHealth;
		}

		public void DecreaseHealth(float value, string damageReason)
		{
			if(IsDead)
				return;
			
			Health = Mathf.Clamp(Health - value, 0, _config.MaxHealth);

			if (Health != 0) return;
			
			OnDeath?.Invoke(damageReason);
			IsDead = true;
		}

		public void IncreaseHealth(float value) => Health = Mathf.Clamp(Health + value, 0, _config.MaxHealth);
	}
}