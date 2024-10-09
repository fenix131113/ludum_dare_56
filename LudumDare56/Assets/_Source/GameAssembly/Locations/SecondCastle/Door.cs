using Interactive;
using UnityEngine;

namespace Locations.SecondCastle
{
	public class Door : MonoBehaviour
	{
		[SerializeField] private Sprite closedSprite;
		[SerializeField] private Sprite openedSprite;
		[SerializeField] private Collider2D doorCollider;
		[SerializeField] private SpriteRenderer doorRenderer;
		[SerializeField] private GameObject interactableObject;
		[SerializeField] private GameObject interactableSecondObject;

		private IInteractableProgress _interactable;
		private IInteractableProgress _secondInteractable;

		private void Awake()
		{
			if (interactableObject.TryGetComponent(out IInteractableProgress interactable) &&
			    interactableSecondObject.TryGetComponent(out IInteractableProgress secondInteractable))
			{
				_interactable = interactable;
				_secondInteractable = secondInteractable;
				Bind();
			}
			else
				Debug.LogWarning(
					$"Can't find interactable on {interactableObject.name} or {interactableSecondObject.name}");
		}

		private void CheckProgress()
		{
			if (Mathf.Approximately(_interactable.Progress, 1) && Mathf.Approximately(_secondInteractable.Progress, 1))
				OpenDoor();
			else
				CloseDoor();
		}

		private void Bind()
		{
			_interactable.OnProgressChanged += CheckProgress;
			_secondInteractable.OnProgressChanged += CheckProgress;
		}

		private void Expose()
		{
			_interactable.OnProgressChanged -= CheckProgress;
			_secondInteractable.OnProgressChanged -= CheckProgress;
		}

		private void OpenDoor()
		{
			doorRenderer.sprite = openedSprite;
			doorCollider.enabled = false;
		}

		private void CloseDoor()
		{
			doorRenderer.sprite = closedSprite;
			doorCollider.enabled = true;
		}

		private void OnApplicationQuit() => Expose();

		private void OnDestroy() => Expose();
	}
}