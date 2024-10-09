using System;
using Player.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Player
{
	public class PlayerHealth
	{
		public float Health { get; private set; }
		public bool IsDead { get; private set; }

		private readonly PlayerConfig _config;
		private readonly AudioSource _oneShotSource;

		public event Action<string> OnDeath;

		[Inject]
		public PlayerHealth(PlayerConfig playerConfig, AudioSource oneShotAudioSource)
		{
			_config = playerConfig;
			_oneShotSource = oneShotAudioSource;

			Health = _config.MaxHealth;
		}

		public void DecreaseHealth(float value, string damageReason)
		{
			if (IsDead)
				return;

			Health = Mathf.Clamp(Health - value, 0, _config.MaxHealth);

			if (Health != 0) return;

			_oneShotSource.PlayOneShot(_config.DeathSounds[Random.Range(0, _config.DeathSounds.Length)], 0.4f);
			OnDeath?.Invoke(damageReason);
			IsDead = true;
		}

		public void IncreaseHealth(float value) => Health = Mathf.Clamp(Health + value, 0, _config.MaxHealth);
	}
}