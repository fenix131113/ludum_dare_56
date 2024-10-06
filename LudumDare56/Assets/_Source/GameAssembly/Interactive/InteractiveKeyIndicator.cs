using System;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Interactive
{
	public class InteractiveKeyIndicator : MonoBehaviour
	{
		[SerializeField] private bool useCollider;
		[SerializeField] private LayerMask interactLayer;
		[SerializeField] private float indicatorYOffset;
		[SerializeField] private float animationDuration;
		[SerializeField] private SpriteRenderer keyIndicatorRenderer;
		[SerializeField] private GameObject interactiveObject;

		private Sequence _indicatorSequence;
		private IInteractableProgress _interactable;
		private float _startIndicatorY;
		private bool _isActive;

		private void OnValidate()
		{
			if (!interactiveObject)
				return;

			if (interactiveObject.TryGetComponent(out IInteractableProgress interactable)) return;

			Debug.LogWarning("This interactive object must inherit from IInteractableProgress!");
			interactiveObject = null;
		}

		private void OnDrawGizmosSelected()
		{
			if (!keyIndicatorRenderer)
				return;

			Gizmos.color = Color.yellow;

			Gizmos.DrawLine(keyIndicatorRenderer.transform.position,
				keyIndicatorRenderer.transform.position + Vector3.up * indicatorYOffset);
		}

		private void Awake()
		{
			_startIndicatorY = keyIndicatorRenderer.transform.position.y;
			_interactable = interactiveObject.GetComponent<IInteractableProgress>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer) && !useCollider)
			{
				if (Mathf.Approximately(_interactable.Progress, 1)) return;
				
				ShowIndicator();
				Bind();
			}
			else if(LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
				ShowIndicator();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (useCollider && LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer)) return;
			
			Expose();
			
			if(_isActive)
				HideIndicator();
		}

		private void ShowIndicator()
		{
			_indicatorSequence = DOTween.Sequence();

			_indicatorSequence.Append(keyIndicatorRenderer.transform.DOMoveY(
				keyIndicatorRenderer.transform.position.y + indicatorYOffset, animationDuration));

			_indicatorSequence.Insert(0,
				DOTween.To(() => keyIndicatorRenderer.color, x => keyIndicatorRenderer.color = x,
					new Color(255, 255, 255, 1f), animationDuration));
			
			_isActive = true;
		}

		private void HideIndicator()
		{
			_indicatorSequence = DOTween.Sequence();

			_indicatorSequence.Append(keyIndicatorRenderer.transform.DOMoveY(
				_startIndicatorY, animationDuration));

			_indicatorSequence.Insert(0,
				DOTween.To(() => keyIndicatorRenderer.color, x => keyIndicatorRenderer.color = x,
					new Color(255, 255, 255, 0f), animationDuration));
			
			_isActive = false;
		}

		private void CheckProgress()
		{
			if(Mathf.Approximately(_interactable.Progress, 1) && _isActive)
			   HideIndicator();
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