using Interactive;
using UnityEngine;

namespace Locations.Forest
{
	public class Door : MonoBehaviour
	{
		[SerializeField] private GameObject valve;
		[SerializeField] private float upY;

		private IInteractableProgress _interactable;
		private float _startY;

		private void Awake()
		{
			if (valve.TryGetComponent(out IInteractableProgress interactable))
			{
				_interactable = interactable;
				_startY = transform.position.y;
				Bind();
			}
			else
				Debug.LogWarning($"There is no IInteractableProgress component attached to {valve.name}");
		}

		private void CheckProgress()
		{
			transform.position = new Vector3(transform.position.x, _startY + (_startY + upY - _startY) * _interactable.Progress,
				transform.position.z);
		}

		private void Bind()
		{
			_interactable.OnProgressChanged += CheckProgress;
		}

		private void Expose()
		{
			_interactable.OnProgressChanged -= CheckProgress;
		}

		private void OnApplicationQuit() => Expose();
	}
}