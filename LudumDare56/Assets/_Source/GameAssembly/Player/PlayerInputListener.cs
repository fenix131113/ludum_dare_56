using Core.Data;
using Core.Game;
using Fireflies;
using Knight;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;
using Utils;
using Zenject;

namespace Player
{
	public class PlayerInputListener : IFixedTickable, ITickable
	{
		private readonly FirefliesMovement _firefliesMovement;
		private readonly FirefliesContainer _firefliesContainer;
		private readonly GameStates _gameStates;
		private readonly KnightMovement _knightMovement;
		private readonly PlayerHealth _health;

		public event Action OnInteractiveKeyDown;
		public event Action OnInteractiveKeyUnPressed;

		[Inject]
		private PlayerInputListener(FirefliesMovement firefliesMovement, FirefliesContainer firefliesContainer,
			GameStates gameStates, KnightMovement knightMovement, PlayerHealth health)
		{
			_firefliesMovement = firefliesMovement;
			_firefliesContainer = firefliesContainer;
			_gameStates = gameStates;
			_knightMovement = knightMovement;
			_health = health;
		}

		public void Tick()
		{
			if(_health.IsDead)
			{
				ReadGameRestartInput();
				return;
			}

			ReadInteractiveKey();
			
			if (_gameStates.PlayerType == PlayerType.FIREFLIES)
				ReadFirefliesInvisibleAbilityInput();
		}

		public void FixedTick()
		{
			if(_health.IsDead)
				return;

			if (_gameStates.PlayerType == PlayerType.FIREFLIES)
			{
				ReadFirefliesMovementInput();
				ReadFirefliesInvisibleAbilityInput();
			}
			else
			{
				ReadKnightMovementInput();
			}
		}

		private static void ReadGameRestartInput()
		{
			if(Input.anyKeyDown)
				GameRestart.RestartGame();
		}
		
		private void ReadInteractiveKey()
		{
			if (Input.GetKeyDown(KeyCode.E))
				OnInteractiveKeyDown?.Invoke();
			if (Input.GetKeyUp(KeyCode.E))
				OnInteractiveKeyUnPressed?.Invoke();
		}

		private void ReadKnightMovementInput()
		{
			var moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

			_knightMovement.Move(moveVector);
		}


		private void ReadFirefliesInvisibleAbilityInput()
		{
			if (!Input.GetKeyDown(KeyCode.Space)) return;

			if (_firefliesContainer.InvisibleModule.IsInvisible)
				_firefliesContainer.MakeVisible();
			else
				_firefliesContainer.MakeInvisible();
		}

		private void ReadFirefliesMovementInput()
		{
			var inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

			_firefliesMovement.Move(inputVector.normalized);
		}
	}
}