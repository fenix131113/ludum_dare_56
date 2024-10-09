using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Locations.Sewage
{
	public class DeathLightController : MonoBehaviour
	{
		[SerializeField] private List<DeathLight> deathLights;

		[SerializeField] private float animTimePerUnit;

		private void Start()
		{
			foreach (var unit in deathLights)
				unit.Init(animTimePerUnit);

			StartCoroutine(StartAnimation());
		}

		// ReSharper disable once FunctionRecursiveOnAllPaths
		private IEnumerator StartAnimation()
		{
			foreach (var unit in deathLights)
			{
				unit.ActivateAnimation();
				yield return new WaitForSeconds(animTimePerUnit);
			}
			
			StartCoroutine(StartAnimation());
		}
	}
}