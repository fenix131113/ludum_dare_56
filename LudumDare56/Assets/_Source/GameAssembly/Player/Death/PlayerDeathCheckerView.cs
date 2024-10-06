﻿using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player.Death
{
	public class PlayerDeathCheckerView : MonoBehaviour
	{
		[SerializeField] private float textAnimationDuration;
		[SerializeField] private float darkFadeAnimationDuration;
		[SerializeField] private Image deathFader;
		[SerializeField] private TMP_Text deathText;
		[SerializeField] private TMP_Text restartLabel;
		
		private PlayerHealth _playerHealth;
		
		[Inject]
		public void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;

		private void Awake() => Bind();

		private void ShowDeadScreen(string reason)
		{
			deathText.text = reason;
			deathText.gameObject.SetActive(true);

			var seq = DOTween.Sequence();
			
			seq.Append(deathFader.DOFade(1f, darkFadeAnimationDuration));
			seq.Append(deathText.rectTransform.DOLocalMoveY(0, textAnimationDuration));
			seq.Insert(darkFadeAnimationDuration, DOTween.To(() => deathText.color, x => deathText.color = x, new Color(255, 255, 255, 1f), textAnimationDuration));
			seq.Append(DOTween.To(() => restartLabel.color, x => restartLabel.color = x, new Color(255, 255, 255, 1f), textAnimationDuration));
		}

		private void Bind()
		{
			_playerHealth.OnDeath += ShowDeadScreen;
		}

		private void Expose()
		{
			_playerHealth.OnDeath -= ShowDeadScreen;
		}

		private void OnApplicationQuit() => Expose();
	}
}