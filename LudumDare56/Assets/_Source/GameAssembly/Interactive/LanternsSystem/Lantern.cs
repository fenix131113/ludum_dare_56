using System;
using Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Interactive.LanternsSystem
{
	public class Lantern : MonoBehaviour, IInteractableProgress
	{
		[SerializeField] private LayerMask playerMask;
		
		public float Progress => _isLanternActive ? 1f : 0f;
		public event Action OnProgressChanged;

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
			OnProgressChanged?.Invoke();
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