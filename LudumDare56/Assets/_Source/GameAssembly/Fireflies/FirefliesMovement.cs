using Player.Data;
using UnityEngine;
using Zenject;

namespace Fireflies
{
	public class FirefliesMovement : MonoBehaviour
	{
		[SerializeField] private float floorDistance;
		[SerializeField] private float gravity;
		[SerializeField] private float groundSafeDistance;
		[SerializeField] private LayerMask floorLayers;
		[SerializeField] private CircleCollider2D playerColldier;

		private Rigidbody2D _rb;
		private PlayerConfig _config;
		private FirefliesContainer _firefliesContainer;

		[Inject]
		private void Construct(PlayerConfig config, FirefliesContainer firefliesContainer)
		{
			_config = config;
			_firefliesContainer = firefliesContainer;
		}

		private void OnDrawGizmosSelected()
		{
			if (!playerColldier)
				return;

			Gizmos.color = Color.yellow;
			var ray = new Ray2D(transform.position, Vector2.down);
			Gizmos.DrawRay(ray.origin, ray.direction * floorDistance);
		}

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			var ray = new Ray2D(transform.position, Vector2.down * floorDistance);

			var hit = Physics2D.Raycast(ray.origin, ray.direction, floorDistance, floorLayers);
			if (!hit)
				_rb.velocity += Vector2.down * (gravity * Time.fixedDeltaTime);
			else if(Vector2.Distance(transform.position, hit.point) < floorDistance - groundSafeDistance)
				_rb.velocity += Vector2.up * (gravity * Time.fixedDeltaTime);
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