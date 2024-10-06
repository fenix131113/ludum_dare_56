using Player;
using UnityEngine;
using Utils;
using Zenject;

namespace Interactive.ValveSystem
{
	public class Valve : MonoBehaviour
	{
		[SerializeField] private LayerMask interactiveLayer;
		[SerializeField] private float progressSpeed;
		[SerializeField] private float rotateSpeed;

		public float Progress { get; private set; }

		private bool _isRotating;
		private PlayerInputListener _inputListener;

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
			_inputListener.OnInteractiveKeyPressed += ActivateRotation;
			_inputListener.OnInteractiveKeyUnPressed += DeactivateRotation;
		}

		private void Expose()
		{
			_inputListener.OnInteractiveKeyPressed -= ActivateRotation;
			_inputListener.OnInteractiveKeyUnPressed -= DeactivateRotation;
		}

		private void Rotate()
		{
			switch (_isRotating)
			{
				case true when Progress < 1:
					transform.eulerAngles -= Vector3.forward * (rotateSpeed * Time.deltaTime);
					Progress = Mathf.Clamp(Progress + Time.deltaTime * progressSpeed, 0, 1);
					break;
				case false when Progress > 0:
					transform.eulerAngles += Vector3.forward * (rotateSpeed * Time.deltaTime);
					Progress = Mathf.Clamp(Progress - Time.deltaTime * progressSpeed, 0, 1);
					break;
			}
		}
	}
}