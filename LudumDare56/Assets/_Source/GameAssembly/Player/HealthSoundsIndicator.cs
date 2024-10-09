using System;
using System.Collections;
using Player.Data;
using UnityEngine;
using Utils;
using Zenject;

namespace Player
{
	public class HealthSoundsIndicator : MonoBehaviour
	{
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

		// ReSharper disable once FunctionRecursiveOnAllPaths
		private IEnumerator StartHealthSoundsCoroutine()
		{
			if(_health.IsDead)
				yield break;
			
			yield return new WaitForSeconds(Mathf.Clamp(soundCheckInterval * (_health.Health / _config.MaxHealth), 0.6f, soundCheckInterval));

			if(_health.Health / _config.MaxHealth < 0.2f)
				cameraShaker.StartShake(gameObject);
			else
				cameraShaker.StopShake(gameObject);
			
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