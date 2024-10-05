using Fireflies.Data;
using Zenject;

namespace Fireflies
{
	public class HealthLightChecker : ITickable
	{
		public bool StayInLight { get; private set; }

		private readonly FirefliesHealth _health;
		private readonly FirefliesConfig _config;
		private readonly FirefliesContainer _firefliesContainer;

		[Inject]
		public HealthLightChecker(FirefliesHealth firefliesHealth, FirefliesConfig config,
			FirefliesContainer firefliesContainer)
		{
			_config = config;
			_health = firefliesHealth;
			_firefliesContainer = firefliesContainer;
		}

		public void StayInLightActivate() => StayInLight = true;

		public void StayInLightDeactivate() => StayInLight = false;

		public void Tick()
		{
			if (_firefliesContainer.InvisibleModule.IsInvisible)
				return;

			if (!StayInLight)
				_health.DecreaseHealth(_config.MinusHealthPerTick);
			else
				_health.IncreaseHealth(_config.PlusHealthPerTick);
		}
	}
}