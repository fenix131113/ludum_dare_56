using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Utils
{
	public class CameraShaker : MonoBehaviour
	{
		private CinemachineBasicMultiChannelPerlin _vCam;
		private readonly List<GameObject> _invokers = new();

		private void Awake()
		{
			_vCam = GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
		}

		private void Start() => _vCam.enabled = false;

		public void StartShake(GameObject invoker)
		{
			if (_invokers.Contains(invoker))
				return;
			
			_invokers.Add(invoker);
			_vCam.enabled = true;
		}

		public void StopShake(GameObject invoker)
		{
			if (!_invokers.Contains(invoker))
				return;

			_invokers.Remove(invoker);

			if (_invokers.Count == 0)
				_vCam.enabled = false;
		}
	}
}