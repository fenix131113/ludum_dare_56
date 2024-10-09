using System;
using UnityEngine;
using Utils;

namespace Interactive.ButtonSystem
{
	public class Button : MonoBehaviour, IInteractableProgress
	{
		[SerializeField] private LayerMask interactLayer;
		[SerializeField] private SpriteRenderer buttonRenderer;
		[SerializeField] private Sprite activateButtonSprite;
		[SerializeField] private Sprite inactivateButtonSprite;
		
		public float Progress => _currentObject ? 1f : 0;
		
		public event Action OnProgressChanged;
		
		private GameObject _currentObject;

		private void Activate()
		{
			buttonRenderer.sprite = activateButtonSprite;
			OnProgressChanged?.Invoke();
		}

		private void Deactivate()
		{
			buttonRenderer.sprite = inactivateButtonSprite;
			OnProgressChanged?.Invoke();
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
				return;

			if (_currentObject)
				return;
			
			_currentObject = other.gameObject;
			Activate();
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
				return;

			if (other.gameObject != _currentObject)
				return;
			
			_currentObject = null;
			Deactivate();
		}
	}
}