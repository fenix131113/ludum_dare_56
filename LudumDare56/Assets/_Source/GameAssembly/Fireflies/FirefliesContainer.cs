using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fireflies.Data;
using UnityEngine;
using Zenject;

namespace Fireflies
{
	[RequireComponent(typeof(CircleCollider2D))]
	public class FirefliesContainer : MonoBehaviour
	{
		[SerializeField] private List<Firefly> fireflies = new();
		[SerializeField] private CircleCollider2D playerCollider;
		[SerializeField] private Firefly fireflyPrefab;

		public FirefliesInvisible InvisibleModule { get; private set; }
		
		private FirefliesConfig _config;
		private bool _canUseInvisibleModule = true;

		[Inject]
		private void Construct(FirefliesConfig config) => _config = config;

		private void Awake()
		{
			InvisibleModule = new FirefliesInvisible();

			foreach (var firefly in fireflies)
				firefly.Init(playerCollider);
		}

		public void AddFirefly(Vector3 fromPosition)
		{
			var firefly = Instantiate(fireflyPrefab, fromPosition, Quaternion.identity, playerCollider.transform);
			firefly.Init(playerCollider);
			fireflies.Add(firefly);
		}

		public Tween MoveAllFirefliesToPositionGlobal(Vector3 toPosition, bool disableCollider = false)
		{
			Tween toReturn = null;
			for (var index = 0; index < fireflies.Count; index++)
			{
				var firefly = fireflies[index];
				if (index == 0)
					toReturn = firefly.MoveOutGlobal(toPosition);
				else
					firefly.MoveOutGlobal(toPosition);
			}
			
			if(disableCollider)
				playerCollider.enabled = false;

			return toReturn;
		}

		public void MakeInvisible()
		{
			if (!_canUseInvisibleModule)
				return;

			_canUseInvisibleModule = false;
			MakeInvisibleWithoutCooldown();
		}

		public void MakeInvisibleWithoutCooldown()
		{
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
			yield return new WaitForSeconds(_config.InvisibleCooldown);

			_canUseInvisibleModule = true;
		}
	}
}