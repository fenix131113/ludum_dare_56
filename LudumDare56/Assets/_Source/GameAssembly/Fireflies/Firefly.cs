using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Fireflies
{
	public class Firefly : MonoBehaviour
	{
		[SerializeField] private Ease ease;
		[SerializeField] private float minTime;
		[SerializeField] private float maxTime;
		[SerializeField] private float speed;
		[SerializeField] private float minChangeDirectionTime;
		[SerializeField] private float maxChangeDirectionTime;

		private Vector3 _currentDirection;

		private void Awake()
		{
			GenerateDirection();
			StartCoroutine(ChangeDirectionTimer());
		}

		private void GenerateDirection()
		{
			var point = Random.insideUnitCircle / 2;
			transform.DOLocalMove(new Vector3(point.x, point.y, 0), Random.Range(minTime, maxTime)).SetEase(ease);
		}

		// ReSharper disable once FunctionRecursiveOnAllPaths
		private IEnumerator ChangeDirectionTimer()
		{
			yield return new WaitForSeconds(Random.Range(minChangeDirectionTime, maxChangeDirectionTime));
			GenerateDirection();
			StartCoroutine(ChangeDirectionTimer());
		}
	}
}