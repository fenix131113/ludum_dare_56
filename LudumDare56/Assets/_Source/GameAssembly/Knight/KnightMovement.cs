using Player.Data;
using UnityEngine;
using Zenject;

namespace Knight
{
	public class KnightMovement : MonoBehaviour
	{
		private PlayerConfig _config;
		private Rigidbody2D _rb;
		private Vector2 _currentVelocity;

		[Inject]
		private void Construct(PlayerConfig config)
		{
			_config = config;
		}
		
		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			_rb.velocity = new Vector2(_currentVelocity.x, _currentVelocity.y) * _config.KnightSpeed;
		}
		
		public void Move(Vector2 moveVector)
		{
			_currentVelocity = moveVector * _config.KnightSpeed;
		}
	}
}