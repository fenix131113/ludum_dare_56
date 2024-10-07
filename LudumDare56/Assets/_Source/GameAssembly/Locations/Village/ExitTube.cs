using Core.Game;
using Fireflies;
using UnityEngine;
using Utils;
using Zenject;

namespace Locations.Village
{
	public class ExitTube : MonoBehaviour
	{
		[SerializeField] private LayerMask interactLayer;
		[SerializeField] private Transform tubePoint;
		
		private FirefliesContainer _firefliesContainer;
		private GameStates _gameStates;
		private bool _triggered;
		
		[Inject]
		private void Construct(FirefliesContainer firefliesContainer, GameStates gameStates)
		{
			_firefliesContainer = firefliesContainer;
			_gameStates = gameStates;
		}

		private void ExitAnimation()
		{
			_gameStates.SetControlState(false);
			_firefliesContainer.MoveAllFirefliesToPositionGlobal(tubePoint.position, true).onComplete += ExitLogic;
		}

		private void ExitLogic()
		{
			// Load next level
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if(!LayerService.CheckLayersEquality(other.gameObject.layer, interactLayer) || _triggered)
				return;
			
			_triggered = true;
			ExitAnimation();
		}
	}
}
