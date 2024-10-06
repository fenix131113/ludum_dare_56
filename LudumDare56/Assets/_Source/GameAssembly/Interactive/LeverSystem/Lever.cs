using System;
using Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Interactive.LeverSystem
{
	public class Lever : MonoBehaviour, IInteractableProgress
	{
		[SerializeField] private LayerMask interactLayer;
		[SerializeField] private bool canSwitchOff;

		public float Progress => _isOn ? 1f : 0f;
		public event Action OnProgressChanged;

		private PlayerInputListener _inputListener;
		private bool _isOn;

		[Inject]
		private void Construct(PlayerInputListener inputListener) => _inputListener = inputListener;

		private void TurnOn()
		{
			_isOn = true;

			OnProgressChanged?.Invoke();
		}

		private void TurnOff()
		{
			_isOn = false;

			OnProgressChanged?.Invoke();
		}

		private void Switch()
		{
			switch (_isOn)
			{
				case true when canSwitchOff:
					TurnOff();
					break;
				case false:
					TurnOn();
					break;
			}
		}

		private void Bind()
		{
			_inputListener.OnInteractiveKeyDown += Switch;
		}

		private void Expose()
		{
			_inputListener.OnInteractiveKeyDown -= Switch;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
				return;

			Bind();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
				return;

			Expose();
		}
		
		private void OnApplicationQuit() => Expose();

		private void OnDestroy() => Expose();
	}
}