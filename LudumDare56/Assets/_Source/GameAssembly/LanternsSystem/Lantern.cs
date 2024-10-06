using Fireflies;
using Player;
using UnityEngine;
using Utils;
using Zenject;

namespace LanternsSystem
{
	public class Lantern : MonoBehaviour
	{
		[SerializeField] private LayerMask playerMask;

		private bool _isLanternActive;
		private PlayerInputListener _inputListener;
		private HealthLightChecker _healthChecker;

		[Inject]
		private void Construct(PlayerInputListener inputListener, HealthLightChecker healthLightChecker)
		{
			_inputListener = inputListener;
			_healthChecker = healthLightChecker;
		}

		public void TurnLanternOn()
		{
			if (_isLanternActive)
				return;

			_isLanternActive = true;
			_healthChecker.StayInLightActivate();
		}

		private void Bind()
		{
			_inputListener.OnInteractiveKeyDown += TurnLanternOn;
		}

		private void Expose()
		{
			_inputListener.OnInteractiveKeyDown -= TurnLanternOn;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, playerMask))
				return;

			Bind();
			
			if (_isLanternActive)
				_healthChecker.StayInLightActivate();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, playerMask))
				return;

			Expose();
			
			if (_isLanternActive)
				_healthChecker.StayInLightDeactivate();
		}
	}
}