using DG.Tweening;
using UnityEngine;

namespace Fireflies.Data
{
	[CreateAssetMenu(fileName = "New FirefliesConfig", menuName = "ScriptableObjects/New FirefliesConfig")]
	public class FirefliesConfig : ScriptableObject
	{
		[field: SerializeField] public Ease EaseType { get; private set; }
		[field: SerializeField] public float MinMoveTime { get; private set; }
		[field: SerializeField] public float MaxMoveTime { get; private set; }
		[field: SerializeField] public float MinChangeDirectionTime { get; private set; }
		[field: SerializeField] public float MaxChangeDirectionTime { get; private set; }
	}
}