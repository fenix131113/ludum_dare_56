using Core.Data;
using Core.Game;
using Fireflies;
using UnityEngine;
using Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace Player
{
	public class DamageZone : MonoBehaviour
	{
		[SerializeField] private LayerMask attackLayer;
		[SerializeField] private string[] damageReasons;

		private bool _damaged;
		private PlayerHealth _health;
		private GameStates _gameStates;
		private FirefliesContainer _firefliesContainer;
		
		[Inject]
		private void Construct(PlayerHealth health, GameStates gameStates, FirefliesContainer firefliesContainer)
		{
			_health = health;
			_gameStates = gameStates;
			_firefliesContainer = firefliesContainer;
		}

		private void OnEnable()
		{
			_damaged = false;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!LayerService.CheckLayersEquality(collision.gameObject.layer, attackLayer) || _damaged)
				return;

			
			_damaged = true;
			_health.DecreaseHealth(1f, damageReasons[Random.Range(0, damageReasons.Length)]);
			
			if(_gameStates.PlayerType == PlayerType.FIREFLIES)
				_firefliesContainer.MakeInvisibleWithoutCooldown();
		}
	}
}