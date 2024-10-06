using Core.Data;
using Core.Game;
using Fireflies;
using Player.Data;
using Zenject;

namespace Player
{
	public class HealthLightChecker : ITickable
	{
		public bool StayInLight { get; private set; }

		private readonly PlayerHealth _health;
		private readonly PlayerConfig _config;
		private readonly FirefliesContainer _firefliesContainer;
		private readonly GameStates _gameStates;

		[Inject]
		public HealthLightChecker(PlayerHealth playerHealth, PlayerConfig config,
			FirefliesContainer firefliesContainer, GameStates gameStates)
		{
			_config = config;
			_health = playerHealth;
			_firefliesContainer = firefliesContainer;
			_gameStates = gameStates;
		}

		public void StayInLightActivate() => StayInLight = true;

		public void StayInLightDeactivate() => StayInLight = false;

		public void Tick()
		{
			if (_gameStates.PlayerType == PlayerType.FIREFLIES && _firefliesContainer.InvisibleModule.IsInvisible)
				return;

			if (!StayInLight)
				_health.DecreaseHealth(_config.MinusHealthPerTick);
			else
				_health.IncreaseHealth(_config.PlusHealthPerTick);
		}
	}
}