using Enemies.Data;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Enemies
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private Gradient spottedGradient = new();
		[SerializeField] private Light2D visionLight;
		[SerializeField] private EnemyConfig config;
		[SerializeField] private EnemyVision vision;
		[SerializeField] private Transform rotatePivot;
		[SerializeField] private float lookTimeBeforeAttack;
		[SerializeField] private GameObject damageZone;
		[SerializeField] private Transform rightPatrolPoint;
		[SerializeField] private Transform leftPatrolPoint;
		[SerializeField] private float floorDistance;
		[SerializeField] private float gravity;
		[SerializeField] private float groundSafeDistance;
		[SerializeField] private LayerMask floorLayers;

		private Rigidbody2D _rb;
		private bool _rightMove = true;
		private bool _lookAtPlayer;
		private bool _isRunning;
		private GameObject _player;
		private float _lookTimer;

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			var ray = new Ray2D(transform.position, Vector2.down);
			Gizmos.DrawRay(ray.origin, ray.direction * floorDistance);
		}

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();

			Bind();
		}

		private void Update()
		{
			if (_lookAtPlayer)
				LookAtPlayer();
			else if (!_isRunning)
				visionLight.color = spottedGradient.Evaluate(0);

			if (rightPatrolPoint && leftPatrolPoint && !_isRunning)
				CheckPointsLimit();
		}

		private void CheckPointsLimit()
		{
			if (transform.position.x >= rightPatrolPoint.position.x)
				SetMoveRotate(false);
			else if (transform.position.x <= leftPatrolPoint.position.x)
				SetMoveRotate(true);
		}

		public void StartRunManually()
		{
			RunToPlayer();
			visionLight.color = spottedGradient.Evaluate(1);
		}

		private void FixedUpdate()
		{
			if (_lookAtPlayer)
				return;

			var xMove = 0f;

			xMove = _isRunning
				? _rightMove 
					? config.runSpeed * Time.fixedDeltaTime
					: -config.runSpeed * Time.fixedDeltaTime
				: _rightMove
					? config.speed * Time.fixedDeltaTime
					: -config.speed * Time.fixedDeltaTime;

			_rb.velocity = new Vector2(xMove * 500, _rb.velocity.y);

			var ray = new Ray2D(transform.position, Vector2.down * floorDistance);

			var hit = Physics2D.Raycast(ray.origin, ray.direction, floorDistance, floorLayers);
			if (!hit)
				_rb.velocity += Vector2.down * (gravity * Time.fixedDeltaTime);
			else if (Vector2.Distance(transform.position, hit.point) < floorDistance - groundSafeDistance)
				_rb.velocity += Vector2.up * (gravity * Time.fixedDeltaTime);
		}

		public void SetMoveRotate(bool isRight)
		{
			SetRotation(isRight);

			if (!vision.IsPlayerSpotted)
			{
				_isRunning = false;
				damageZone.SetActive(false);
			}

			_rightMove = isRight;
		}

		public void SetRotation(bool isRight)
		{
			var multiplier = isRight != _rightMove ? -1 : 1;
 
			rotatePivot.localScale = new Vector3(rotatePivot.localScale.x * multiplier, transform.localScale.y,
				transform.localScale.z);
		}

		private void ActivatePlayerLook(GameObject player)
		{
			if (_isRunning)
				return;

			_player = player;
			_lookAtPlayer = true;
			_lookTimer = Time.time;
		}

		private void DeactivatePlayerLook()
		{
			if (_isRunning)
				return;

			_lookAtPlayer = false;
		}

		private void LookAtPlayer()
		{
			SetRotation(_player.transform.position.x > transform.position.x);

			visionLight.color = spottedGradient.Evaluate((Time.time - _lookTimer) / lookTimeBeforeAttack);

			if (Time.time - _lookTimer >= lookTimeBeforeAttack && _lookAtPlayer && !_isRunning)
				RunToPlayer();
		}

		private void RunToPlayer()
		{
			DeactivatePlayerLook();
			damageZone.SetActive(true);
			_isRunning = true;
		}

		private void Bind()
		{
			vision.OnPlayerDetected += ActivatePlayerLook;
			vision.OnPlayerGone += DeactivatePlayerLook;
		}

		private void Expose()
		{
			vision.OnPlayerDetected -= ActivatePlayerLook;
			vision.OnPlayerGone -= DeactivatePlayerLook;
		}

		private void OnApplicationQuit() => Expose();
	}
}