using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Core.Game
{
	public class StartUnFade : MonoBehaviour
	{
		[SerializeField] private Image fader;
		[SerializeField] private float fadeDuration;

		private void Awake() => fader.DOFade(0f, fadeDuration);
	}
}
