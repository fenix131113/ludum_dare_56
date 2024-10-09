using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Utils
{
	public class CameraShaker : MonoBehaviour
	{
		[SerializeField] private float shakeAmplitude = 1.2f;
		[SerializeField] private float shakeFrequency = 2.0f;
		[SerializeField] private CinemachineVirtualCamera virtualCamera;

		private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
		private readonly List<GameObject> _invokers = new();
		private bool _isShaking;
		private float _selectedAmplitude;
		private float _selectedFrequency;

		private void Start()
		{
			if (virtualCamera != null)
				_virtualCameraNoise =
					virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

			_selectedAmplitude = shakeAmplitude;
			_selectedFrequency = shakeFrequency;
		}

		private void Update()
		{
			if (_isShaking)
			{
				_virtualCameraNoise.m_AmplitudeGain = _selectedAmplitude;
				_virtualCameraNoise.m_FrequencyGain = _selectedFrequency;
			}
			else
			{
				_virtualCameraNoise.m_AmplitudeGain = 0f;
				_virtualCameraNoise.m_FrequencyGain = 0f;
			}
		}

		public void StartShake(GameObject invoker, float shakeAmpl = 0, float shakeFreq = 0)
		{
			_selectedAmplitude = shakeAmpl == 0 ? shakeAmplitude : shakeAmpl;
			_selectedFrequency = shakeFreq == 0 ? shakeFrequency : shakeFreq;

			if (virtualCamera == null || _virtualCameraNoise == null)
				return;

			if (_invokers.Contains(invoker))
				return;

			_invokers.Add(invoker);
			_isShaking = true;
		}

		public void StopShake(GameObject invoker)
		{
			if (virtualCamera == null || _virtualCameraNoise == null)
				return;

			if (!_invokers.Contains(invoker))
				return;

			_invokers.Remove(invoker);

			if (_invokers.Count == 0)
				_isShaking = false;
		}
	}
}