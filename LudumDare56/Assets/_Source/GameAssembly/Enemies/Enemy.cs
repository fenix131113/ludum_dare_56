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

		private Rigidbody2D _rb;
		private bool _rightMove = true;
		private bool _lookAtPlayer;
		private bool _isRunning;
		private GameObject _player;
		private float _lookTimer;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();

			Bind();
		}

		private void Update()
		{
			if (_lookAtPlayer)
				LookAtPlayer();
			else if(!_isRunning)
				visionLight.color = spottedGradient.Evaluate(0);
		}

		private void FixedUpdate()
		{
			if (_lookAtPlayer)
				return;

			var xMove = 0f;

			xMove = _isRunning ?
				  _rightMove ? config.runSpeed * Time.deltaTime : -config.runSpeed * Time.deltaTime
				: _rightMove ? config.speed * Time.deltaTime : -config.speed * Time.deltaTime;

			_rb.velocity = new Vector2(xMove * 500, 0);
		}

		public void SetMoveRotate(bool isRight)
		{
			SetRotation(isRight);

			if (!vision.IsPlayerSpotted)
				_isRunning = false;

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