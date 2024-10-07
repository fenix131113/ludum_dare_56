using Core.Data;

namespace Core.Game
{
	public class GameStates
	{
		public PlayerType PlayerType { get; private set; } = PlayerType.FIREFLIES;
		public bool CanControlPlayer { get; private set; } = true;
		
		public void ChangePlayerType(PlayerType playerType) => PlayerType = playerType;
		public void SetControlState(bool state) => CanControlPlayer = state;
	}
}