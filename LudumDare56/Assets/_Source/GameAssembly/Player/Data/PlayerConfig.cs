using UnityEngine;

namespace Player.Data
{
	[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "ScriptableObjects/New PlayerConfig")]
	public class PlayerConfig : ScriptableObject
	{
		[field: SerializeField] public float FirefliesSpeed { get; private set; }
		[field: SerializeField] public float KnightSpeed { get; private set; }
	}
}