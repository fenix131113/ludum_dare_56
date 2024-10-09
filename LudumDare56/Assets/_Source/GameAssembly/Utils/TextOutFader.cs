using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Utils
{
	public class TextOutFader : MonoBehaviour
	{
		[SerializeField] private TMP_Text label;
		[SerializeField] private float animTime;

		private float _startY;

		private void Start()
		{
			FadeOut();
			_startY = label.rectTransform.anchoredPosition.y;
		}

		private void FadeOut()
		{
			label.transform.DOLocalMoveY(0, animTime);
			DOTween.To(() => label.color, x => label.color = x, new Color(255, 255, 255, 1f), animTime);
		}
		
		public void FadeIn()
		{
			label.transform.DOLocalMoveY(-_startY, animTime);
			DOTween.To(() => label.color, x => label.color = x, new Color(255, 255, 255, 0f), animTime)
				.onComplete += () => gameObject.SetActive(false);
		}
	}
}
