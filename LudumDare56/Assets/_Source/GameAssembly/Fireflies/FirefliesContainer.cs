using System.Collections;
using System.Collections.Generic;
using Fireflies.Data;
using UnityEngine;
using Zenject;

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
		private FirefliesConfig _config;
		private bool _canUseInvisibleModule = true;

		[Inject]
		private void Construct(FirefliesConfig config) => _config = config;
		
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
			if(!_canUseInvisibleModule)
				return;
			
			InvisibleModule.Hide(fireflies);
			playerCollider.enabled = false;
		}

		public void MakeVisible()
		{
			InvisibleModule.Show(fireflies);
			playerCollider.enabled = true;
			StartCoroutine(InvisibleCooldownCoroutine());
		}

		private IEnumerator InvisibleCooldownCoroutine()
		{
			_canUseInvisibleModule = false;
			
			yield return new WaitForSeconds(_config.InvisibleCooldown);
			
			_canUseInvisibleModule = true;
		}
	}
}