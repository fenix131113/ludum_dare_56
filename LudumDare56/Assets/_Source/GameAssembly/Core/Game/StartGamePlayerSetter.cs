using Core.Data;
using UnityEngine;
using Zenject;

namespace Core.Game
{
	public class StartGamePlayerSetter : MonoBehaviour
	{
		[SerializeField] private PlayerType startPlayerType;

		private GameStates _gameStates;
		
		[Inject]
		private void Construct(GameStates gameStates) => _gameStates = gameStates;

		private void Awake() => _gameStates.ChangePlayerType(startPlayerType);
	}
}