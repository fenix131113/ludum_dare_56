using Player.Data;
using UnityEngine;
using Zenject;

namespace Fireflies
{
	public class FirefliesMovement : MonoBehaviour
	{
		private Rigidbody2D _rb;
		private PlayerConfig _config;
		private FirefliesContainer _firefliesContainer;

		[Inject]
		private void Construct(PlayerConfig config, FirefliesContainer firefliesContainer)
		{
			_config = config;
			_firefliesContainer = firefliesContainer;
		}

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		public void Move(Vector2 moveVector)
		{
			if (!_firefliesContainer.InvisibleModule.IsInvisible)
				_rb.AddForce(new Vector2(moveVector.x, moveVector.y) * (_config.FirefliesSpeed),
					ForceMode2D.Force);
			else
				_rb.velocity = Vector2.zero;
		}
	}
}