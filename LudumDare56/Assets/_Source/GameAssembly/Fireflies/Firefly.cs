using System.Collections;
using DG.Tweening;
using Fireflies.Data;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Fireflies
{
	public class Firefly : MonoBehaviour
	{
		[SerializeField] private FirefliesConfig config;
		[SerializeField] private Light2D fireflyLight;
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private bool withPlayer;

		private Vector3 _currentDirection;
		private CircleCollider2D _firefliesGroupCollider;
		private Tween _moveTween;
		private float _startIntensity;

		public void Init(CircleCollider2D firefliesGroupCollider)
		{
			if (_firefliesGroupCollider)
				return;

			_firefliesGroupCollider = firefliesGroupCollider;
			_startIntensity = fireflyLight.intensity;

			if (!withPlayer)
				MoveToPlayer();
			else
			{
				GenerateMovement();
				StartCoroutine(ChangeDirectionTimer());
			}
		}

		public void MoveToPlayer(float time = 0)
		{
			if (time <= 0)
				time = config.MinMoveTime;
			
			_moveTween = transform.DOLocalMove(Vector3.zero, time).SetEase(config.EaseType);
			_moveTween.onComplete += () =>
			{
				withPlayer = true;
				GenerateMovement();
				StartCoroutine(ChangeDirectionTimer());
			};
		}

		private void StopMoving()
		{
			_moveTween = null;
			withPlayer = false;
			StopAllCoroutines();
		}

		public Tween MoveOutLocal(Vector3 toPosition)
		{
			var animationTime = Random.Range(config.MinMoveTime, config.MaxMoveTime);
			
			StopMoving();
			spriteRenderer.DOFade(0f, animationTime);
			DOTween.To(() => fireflyLight.intensity, x => fireflyLight.intensity = x, 0f, animationTime);
			_moveTween = transform.DOLocalMove(toPosition, animationTime).SetEase(config.EaseType);

			return _moveTween;
		}
		
		public Tween MoveOutGlobal(Vector3 toPosition)
		{
			var animationTime = Random.Range(config.MinMoveTime, config.MaxMoveTime);
			
			StopMoving();
			spriteRenderer.DOFade(0f, animationTime);
			DOTween.To(() => fireflyLight.intensity, x => fireflyLight.intensity = x, 0f, animationTime);
			_moveTween = transform.DOMove(toPosition, animationTime).SetEase(config.EaseType);

			return _moveTween;
		}

		public void MoveIn()
		{
			var animationTime = Random.Range(config.MinMoveTime, config.MaxMoveTime);
			
			spriteRenderer.DOFade(1f, animationTime);
			DOTween.To(() => fireflyLight.intensity, x => fireflyLight.intensity = x, _startIntensity, animationTime);
			MoveToPlayer();
		}

		private void GenerateMovement()
		{
			var point = Random.insideUnitCircle * _firefliesGroupCollider.radius;
			_moveTween = transform.DOLocalMove(new Vector3(point.x, point.y, 0),
				Random.Range(config.MinMoveTime, config.MaxMoveTime)).SetEase(config.EaseType);
		}

		// ReSharper disable once FunctionRecursiveOnAllPaths
		private IEnumerator ChangeDirectionTimer()
		{
			yield return new WaitForSeconds(Random.Range(config.MinChangeDirectionTime, config.MaxChangeDirectionTime));
			GenerateMovement();
			StartCoroutine(ChangeDirectionTimer());
		}
	}
}