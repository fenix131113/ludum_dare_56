using System.Collections.Generic;
using UnityEngine;

namespace Fireflies
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class FirefliesContainer : MonoBehaviour
	{
		[SerializeField] private List<Firefly> fireflies = new();
		[SerializeField] private Collider2D playerCollider;
		[SerializeField] private Firefly fireflyPrefab;

		public FirefliesInvisible InvisibleModule { get; private set; }

		private CircleCollider2D _circleCollider;

		private void Awake()
		{
			_circleCollider = GetComponent<CircleCollider2D>();
			InvisibleModule = new FirefliesInvisible();

			foreach (var firefly in fireflies)
				firefly.Init(_circleCollider);
		}

		public void AddFirefly(Vector3 fromPosition)
		{
			var firefly = Instantiate(fireflyPrefab, fromPosition, Quaternion.identity, _circleCollider.transform);
			firefly.Init(_circleCollider);
			fireflies.Add(firefly);
		}

		public void MakeInvisible()
		{
			InvisibleModule.Hide(fireflies);
			playerCollider.enabled = false;
		}

		public void MakeVisible()
		{
			InvisibleModule.Show(fireflies);
			playerCollider.enabled = true;
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.F))
				AddFirefly(new Vector3(_circleCollider.transform.position.x, _circleCollider.transform.position.y - 10, 0));
		}
	}
}