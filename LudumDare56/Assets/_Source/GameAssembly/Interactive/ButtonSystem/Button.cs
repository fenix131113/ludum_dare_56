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
		}

		private void Deactivate()
		{
			buttonRenderer.sprite = inactivateButtonSprite;
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
				return;

			if (_currentObject)
				return;
			
			Activate();
			_currentObject = other.gameObject;
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer))
				return;

			if (other.gameObject != _currentObject)
				return;
			
			Deactivate();
			_currentObject = null;
		}
	}
}