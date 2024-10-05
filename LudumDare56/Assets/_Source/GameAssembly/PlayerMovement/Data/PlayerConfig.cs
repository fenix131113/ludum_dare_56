using UnityEngine;

namespace PlayerMovement.Data
{
	[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "ScriptableObject/New PlayerConfig")]
	public class PlayerConfig : ScriptableObject
	{
		[field: SerializeField] public float FirefliesSpeed { get; private set; }
	}
}