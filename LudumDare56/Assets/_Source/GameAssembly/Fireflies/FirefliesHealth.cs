using Fireflies.Data;
using UnityEngine;
using Zenject;

namespace Fireflies
{
	public class FirefliesHealth
	{
		public float Health { get; private set; }
		
		private readonly FirefliesConfig _config;

		[Inject]
		public FirefliesHealth(FirefliesConfig firefliesConfig)
		{
			_config = firefliesConfig;

			Health = _config.MaxHealth;
		}
		
		public void DecreaseHealth(float value) => Health = Mathf.Clamp(Health - value, 0, _config.MaxHealth);
		public void IncreaseHealth(float value) => Health = Mathf.Clamp(Health + value, 0, _config.MaxHealth);
	}
}