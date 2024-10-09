using System.Collections;
using Player.Data;
using UnityEngine;
using Utils;
using Zenject;

namespace Player
{
	public class HealthSoundsIndicator : MonoBehaviour
	{
		[SerializeField] private float maxAmplitude = 2f;
		[SerializeField] private float maxFrequency = 2f;
		[SerializeField] private AudioSource source;
		[SerializeField] private AudioClip heartSound;
		[SerializeField] private float soundCheckInterval;
		[SerializeField] private CameraShaker cameraShaker;

		private PlayerHealth _health;
		private PlayerConfig _config;

		[Inject]
		private void Construct(PlayerHealth health, PlayerConfig config)
		{
			_health = health;
			_config = config;
		}

		private void Start() => StartCoroutine(StartHealthSoundsCoroutine());

		private void FixedUpdate()
		{
			const float offset = 0.5f;
			if (_health.Health / _config.MaxHealth < offset)
			{
				var relativeValue = _config.MaxHealth - _health.Health / _config.MaxHealth - offset;
				var scaledAmplitude = relativeValue * maxAmplitude * 2;
				var scaledFrequency = relativeValue * maxAmplitude * 2;

				cameraShaker.StartShake(gameObject,
					Mathf.Clamp(scaledAmplitude, 0, maxAmplitude),
					Mathf.Clamp(scaledFrequency, 0, maxFrequency));
			}
			else
				cameraShaker.StopShake(gameObject);
		}

		// ReSharper disable once FunctionRecursiveOnAllPaths
		private IEnumerator StartHealthSoundsCoroutine()
		{
			if (_health.IsDead)
				yield break;

			yield return new WaitForSeconds(Mathf.Clamp(soundCheckInterval * (_health.Health / _config.MaxHealth),
				maxAmplitude,
				soundCheckInterval));

			if (Mathf.Approximately(_health.Health, _config.MaxHealth))
			{
				StartCoroutine(StartHealthSoundsCoroutine());
				yield break;
			}

			source.PlayOneShot(heartSound, 0.35f);
			StartCoroutine(StartHealthSoundsCoroutine());
		}
	}
}