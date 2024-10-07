using System.Collections.Generic;
using UnityEngine;

namespace Fireflies
{
	public class FirefliesInvisible
	{
		public bool IsInvisible { get; private set; }

		public void Hide(List<Firefly> fireflies)
		{
			foreach (var firefly in fireflies)
				firefly.MoveOutLocal(firefly.transform.localPosition + new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0));

			IsInvisible = true;
		}

		public void Show(List<Firefly> fireflies)
		{
			foreach (var firefly in fireflies)
				firefly.MoveIn();

			IsInvisible = false;
		}
	}
}