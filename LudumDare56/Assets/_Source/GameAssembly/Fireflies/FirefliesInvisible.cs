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
				firefly.MoveOut(firefly.transform.position + new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0));

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