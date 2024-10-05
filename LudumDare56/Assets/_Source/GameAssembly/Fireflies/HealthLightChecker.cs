using Fireflies.Data;
using Zenject;

namespace Fireflies
{
	public class HealthLightChecker : ITickable
	{
		public bool StayInLight { get; private set; }

		private readonly FirefliesHealth _health;
		private readonly FirefliesConfig _config;

		[Inject]
		public HealthLightChecker(FirefliesHealth firefliesHealth, FirefliesConfig config)
		{
			_config = config;
			_health = firefliesHealth;
		}

		public void StayInLightActivate() => StayInLight = true;

		public void StayInLightDeactivate() => StayInLight = false;

		public void Tick()
		{
			if (!StayInLight)
				_health.DecreaseHealth(_config.MinusHealthPerTick);
			else
				_health.IncreaseHealth(_config.PlusHealthPerTick);
		}
	}
}