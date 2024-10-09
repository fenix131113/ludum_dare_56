using Player.Data;
using UnityEngine;
using Zenject;

namespace Knight
{
	public class KnightMovement : MonoBehaviour
	{
		private static readonly int IsRunningKey = Animator.StringToHash("IsRunning");
		private static readonly int JumpKey = Animator.StringToHash("Jump");
		private static readonly int IsOnGroundKey = Animator.StringToHash("IsOnGround");

		[SerializeField] private Animator animator;
		[SerializeField] private SpriteRenderer knightRenderer;
		[SerializeField] private float floorDistanceCheck;
		[SerializeField] private LayerMask groundLayer;

		private PlayerConfig _config;
		private Rigidbody2D _rb;
		private Vector2 _currentVelocity;
		private bool _isOnGround;

		[Inject]
		private void Construct(PlayerConfig config) => _config = config;

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;

			Gizmos.DrawLine(transform.position, transform.position + Vector3.down * floorDistanceCheck);
		}

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			CheckGround();
		}

		private void CheckGround()
		{
			_isOnGround = Physics2D.Raycast(transform.position, Vector2.down, floorDistanceCheck, groundLayer);
			animator.SetBool(IsOnGroundKey, _isOnGround);
		}

		private void FixedUpdate()
		{
			_rb.velocity = new Vector2(_currentVelocity.x * _config.KnightSpeed, _rb.velocity.y);
		}

		public void Move(Vector2 moveVector)
		{
			_currentVelocity = moveVector * _config.KnightSpeed;

			if (moveVector.magnitude != 0)
				knightRenderer.flipX = moveVector.x < 0;

			animator.SetBool(IsRunningKey, moveVector.magnitude != 0);
		}

		public void Jump()
		{
			if (!_isOnGround) return;

			_rb.AddForce(Vector2.up * _config.KnightJumpForce, ForceMode2D.Impulse);
			animator.SetTrigger(JumpKey);
		}
	}
}