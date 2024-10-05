using UnityEngine;

namespace PlayerMovement.Data
{
	[CreateAssetMenu(fileName = "New PlayerConfig", menuName = "ScriptableObjects/New PlayerConfig")]
	public class PlayerConfig : ScriptableObject
	{
		[field: SerializeField] public float FirefliesSpeed { get; private set; }
	}
}