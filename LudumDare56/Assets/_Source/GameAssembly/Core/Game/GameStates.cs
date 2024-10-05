using Core.Data;

namespace Core.Game
{
	public class GameStates
	{
		public PlayerType PlayerType { get; private set; } = PlayerType.FIREFLIES;
		
		public void ChangePlayerType(PlayerType playerType) => PlayerType = playerType;
	}
}