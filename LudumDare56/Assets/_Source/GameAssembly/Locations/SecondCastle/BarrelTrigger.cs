using UnityEngine;
using Utils;

namespace Locations.SecondCastle
{
	public class BarrelTrigger : MonoBehaviour
	{
		[SerializeField] private LayerMask playerLayer;
		[SerializeField] private Barrel barrel;

		private bool _entered;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(!LayerService.CheckLayersEquality(other.gameObject.layer, playerLayer) || _entered)
				return;

			_entered = true;
			barrel.ActivateBarrel();
		}
	}
}
