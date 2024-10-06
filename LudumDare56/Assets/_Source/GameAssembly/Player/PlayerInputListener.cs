using Core.Data;
using Core.Game;
using Fireflies;
using Knight;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;
using Zenject;

namespace Player
{
	public class PlayerInputListener : ITickable
	{
		private readonly FirefliesMovement _firefliesMovement;
		private readonly FirefliesContainer _firefliesContainer;
		private readonly GameStates _gameStates;
		private readonly KnightMovement _knightMovement;

		public event Action OnInteractiveKeyPressed;
		public event Action OnInteractiveKeyUnPressed;

		[Inject]
		private PlayerInputListener(FirefliesMovement firefliesMovement, FirefliesContainer firefliesContainer,
			GameStates gameStates, KnightMovement knightMovement)
		{
			_firefliesMovement = firefliesMovement;
			_firefliesContainer = firefliesContainer;
			_gameStates = gameStates;
			_knightMovement = knightMovement;
		}

		public void Tick()
		{
			ReadInteractiveKey();

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

		private void ReadInteractiveKey()
		{
			if (Input.GetKeyDown(KeyCode.E))
				OnInteractiveKeyPressed?.Invoke();
			if(Input.GetKeyUp(KeyCode.E))
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