using System.Collections;
using UnityEngine;
using DG.Tweening;
using Fireflies.Data;

namespace Fireflies
{
	public class Firefly : MonoBehaviour
	{
		[SerializeField] private FirefliesConfig config;
		[SerializeField] private bool withPlayer;

		private Vector3 _currentDirection;
		private CircleCollider2D _firefliesGroupCollider;

		public void Init(CircleCollider2D firefliesGroupCollider)
		{
			if (_firefliesGroupCollider)
				return;

			_firefliesGroupCollider = firefliesGroupCollider;

			if (!withPlayer)
				MoveToPlayer();
			else
			{
				GenerateMovement();
				StartCoroutine(ChangeDirectionTimer());
			}
		}

		private void MoveToPlayer()
		{
			transform.DOLocalMove(Vector3.zero, config.MinMoveTime).SetEase(config.EaseType)
				.onComplete += () =>
			{
				GenerateMovement();
				StartCoroutine(ChangeDirectionTimer());
			};
		}

		private void GenerateMovement()
		{
			var point = Random.insideUnitCircle * _firefliesGroupCollider.radius;
			transform.DOLocalMove(new Vector3(point.x, point.y, 0),
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