using UnityEngine;

namespace Enemies.Data
{
	[CreateAssetMenu(fileName = "New EnemyConfig", menuName = "ScriptableObjects/New EnemyConfig")]
	public class EnemyConfig : ScriptableObject
	{
		[field: SerializeField] public float speed;
	}
}