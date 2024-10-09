using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using DG.Tweening;

namespace Locations.Sewage
{
	public class ExitTeleport : MonoBehaviour
	{
		[SerializeField] private LayerMask playerMask;
		[SerializeField] private Image fader;
		[SerializeField] private float fadeDuration;
		[SerializeField] private int loadSceneIndex;

		private bool _isEntered;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!LayerService.CheckLayersEquality(collision.gameObject.layer, playerMask) || _isEntered)
				return;

			_isEntered = true;
			fader.DOFade(1f, fadeDuration)
				.onComplete += () => SceneManager.LoadScene(loadSceneIndex);
		}
	}
}