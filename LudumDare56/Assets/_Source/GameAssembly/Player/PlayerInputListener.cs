using System;
using Core.Data;
using Core.Game;
using Core.Menu;
using Fireflies;
using Knight;
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
		private readonly GamePauseMenu _gamePauseMenu;

		public event Action OnInteractiveKeyDown;
		public event Action OnInteractiveKeyUnPressed;

		[Inject]
		private PlayerInputListener(FirefliesMovement firefliesMovement, FirefliesContainer firefliesContainer,
			GameStates gameStates, KnightMovement knightMovement, PlayerHealth health, GamePauseMenu gamePauseMenu)
		{
			_firefliesMovement = firefliesMovement;
			_firefliesContainer = firefliesContainer;
			_gameStates = gameStates;
			_knightMovement = knightMovement;
			_health = health;
			_gamePauseMenu = gamePauseMenu;
		}

		public void Tick()
		{
			if (_health.IsDead)
			{
				ReadGameRestartInput();
				return;
			}

			if(!_gameStates.CanControlPlayer)
				return;
			
			ReadMenuInput();
			ReadInteractiveKey();

			if (_gameStates.PlayerType == PlayerType.FIREFLIES)
				ReadFirefliesInvisibleAbilityInput();
		}

		private void ReadMenuInput()
		{
			if(!Input.GetKeyDown(KeyCode.Escape))
				return;
			
			if (_gamePauseMenu.IsPaused)
				_gamePauseMenu.UnpauseGame();
			else
				_gamePauseMenu.PauseGame();
		}

		public void FixedTick()
		{
			if (_health.IsDead || _gamePauseMenu.IsPaused || !_gameStates.CanControlPlayer)
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
			if (Input.anyKeyDown)
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