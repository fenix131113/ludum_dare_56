using System;
using UnityEngine;
using Utils;

namespace Enemies
{
	public class EnemyVision : MonoBehaviour
	{
		[SerializeField] private LayerMask playerMask;
		
		public bool IsPlayerSpotted { get; private set; }
		public event Action<GameObject> OnPlayerDetected;
		public event Action OnPlayerGone;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if(!LayerService.CheckLayersEquality(other.gameObject.layer, playerMask)) return;
			
			IsPlayerSpotted = true;
			OnPlayerDetected?.Invoke(other.gameObject);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if(!LayerService.CheckLayersEquality(other.gameObject.layer, playerMask)) return;

			IsPlayerSpotted = false;
			OnPlayerGone?.Invoke();
		}
	}
}