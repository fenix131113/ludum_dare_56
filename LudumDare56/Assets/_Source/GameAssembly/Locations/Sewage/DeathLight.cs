using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Locations.Sewage
{
	public class DeathLight : MonoBehaviour
	{
		[SerializeField] private Light2D lightning;
		[SerializeField] private Collider2D deathCollider;

		private float _animTime;
		private PlayerHealth _playerHealth;
		public void Init(float animationTime)
		{
			_animTime = animationTime;
		}

		public void ActivateAnimation()
		{
				deathCollider.enabled = true;
			DOTween.To(() => lightning.intensity, x => lightning.intensity = x, 1f, _animTime)
				.onComplete += DeactivateAnimation;
		}

		private void DeactivateAnimation()
		{
			deathCollider.enabled = false;
			DOTween.To(() => lightning.intensity, x => lightning.intensity = x, 0f, _animTime / 2);
		}
	}
}