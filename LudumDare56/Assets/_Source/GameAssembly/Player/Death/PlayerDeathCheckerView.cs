using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player.Death
{
	public class PlayerDeathCheckerView : MonoBehaviour
	{
		[SerializeField] private float deathAnimationDuration;
		[SerializeField] private Image deathFader;
		[SerializeField] private TMP_Text deathText;
		[SerializeField] private TMP_Text restartLabel;
		
		private PlayerHealth _playerHealth;
		
		[Inject]
		public void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;

		private void Awake() => Bind();

		private void ShowDeadScreen(string reason)
		{
			Time.timeScale = 0; // Not enough time to do normally :(
			deathFader.DOFade(1f, deathAnimationDuration);
			
			deathText.text = reason;
			deathText.gameObject.SetActive(true);

			deathText.rectTransform.DOLocalMoveY(0, deathAnimationDuration);
			DOTween.To(() => deathText.color, x => deathText.color = x, new Color(255, 255, 255, 1f), deathAnimationDuration);
			
			DOTween.To(() => restartLabel.color, x => restartLabel.color = x, new Color(255, 255, 255, 1f), deathAnimationDuration);
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