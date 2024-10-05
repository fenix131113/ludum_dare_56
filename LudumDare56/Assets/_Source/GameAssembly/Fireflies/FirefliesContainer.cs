using System.Collections.Generic;
using UnityEngine;

namespace Fireflies
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class FirefliesContainer : MonoBehaviour
	{
		[SerializeField] private List<Firefly> fireflies = new();
		[SerializeField] private Firefly fireflyPrefab;
		
		private CircleCollider2D _circleCollider;

		private void Awake()
		{
			_circleCollider = GetComponent<CircleCollider2D>();

			foreach (var firefly in fireflies)
				firefly.Init(_circleCollider);
		}

		public void AddFirefly(Vector3 position)
		{
			var firefly = Instantiate(fireflyPrefab, position, Quaternion.identity, _circleCollider.transform);
			firefly.Init(_circleCollider);
			fireflies.Add(firefly);
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
				AddFirefly(new Vector3(_circleCollider.transform.position.x, _circleCollider.transform.position.y - 10, 0));
		}
	}
}