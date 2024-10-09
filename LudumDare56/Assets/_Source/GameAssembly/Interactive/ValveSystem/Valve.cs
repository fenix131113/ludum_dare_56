using System;
using Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Interactive.ValveSystem
{
	public class Valve : MonoBehaviour, IInteractableProgress
	{
		[SerializeField] private LayerMask interactiveLayer;
		[SerializeField] private float progressSpeedUp;
		[SerializeField] private float progressSpeedDown;
		[SerializeField] private float rotateOnSpeed;
		[SerializeField] private float rotateOffSpeed;

		public float Progress { get; private set; }

		private bool _isRotating;
		private PlayerInputListener _inputListener;
		
		public event Action OnProgressChanged;

		[Inject]
		private void Construct(PlayerInputListener inputListener)
		{
			_inputListener = inputListener;
		}

		private void Update()
		{
			Rotate();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactiveLayer))
				return;

			Bind();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactiveLayer))
				return;

			DeactivateRotation();
			Expose();
		}

		public void ActivateRotation()
		{
			_isRotating = true;
		}

		public void DeactivateRotation()
		{
			_isRotating = false;
		}

		private void Bind()
		{
			_inputListener.OnInteractiveKeyDown += ActivateRotation;
			_inputListener.OnInteractiveKeyUnPressed += DeactivateRotation;
		}

		private void Expose()
		{
			_inputListener.OnInteractiveKeyDown -= ActivateRotation;
			_inputListener.OnInteractiveKeyUnPressed -= DeactivateRotation;
		}

		private void Rotate()
		{
			switch (_isRotating)
			{
				case true when Progress < 1:
					transform.eulerAngles -= Vector3.forward * (rotateOnSpeed * Time.deltaTime);
					Progress = Mathf.Clamp(Progress + Time.deltaTime * progressSpeedUp, 0, 1);
					OnProgressChanged?.Invoke();
					break;
				case false when Progress > 0:
					transform.eulerAngles += Vector3.forward * (rotateOffSpeed * Time.deltaTime);
					Progress = Mathf.Clamp(Progress - Time.deltaTime * progressSpeedDown, 0, 1);
					OnProgressChanged?.Invoke();
					break;
			}
		}

		private void OnApplicationQuit() => Expose();
	}
}