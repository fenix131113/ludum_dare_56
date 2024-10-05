using PlayerMovement.Data;
using UnityEngine;
using Zenject;

namespace PlayerMovement
{
	public class FirefliesMovement : MonoBehaviour
	{
		private Rigidbody2D _rb;
		private PlayerConfig _config;

		[Inject]
		private void Construct(PlayerConfig config)
		{
			_config = config;
		}

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		public void Move(Vector2 moveVector)
		{
			//if (moveVector.magnitude == 0)
				//_rb.velocity = Vector2.zero;
			//else
				_rb.AddForce(new Vector2(moveVector.x, moveVector.y) * _config.FirefliesSpeed, ForceMode2D.Force);
		}
	}
}